using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    NoteSpawner noteSpawner = null;
    SoundTimer timer = null;
    NoteReader reader = null;

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();
    NoteInfo[] Downnotes = new NoteInfo[4] { new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo() };

    string panjeong;

    private void Start()
    {
        noteSpawner = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteSpawner>();
        reader = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteReader>();
        timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
    }

    private int GetHit()
    {
        if (Input.GetKeyDown(KeyCode.Z))        return 1;
        if (Input.GetKeyDown(KeyCode.X))        return 2;
        if (Input.GetKeyDown(KeyCode.Period))   return 3;
        if (Input.GetKeyDown(KeyCode.Slash))    return 4;

        return 0;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) reader.ReadLis(ref notes, "Assets/Scripts/Ozi's Scene/example");
        
        while (notes.Count > 0 && Downnotes[notes.First().line - 1].line == -1)
        {
            Downnotes[notes.First().line - 1] = notes.Dequeue();
        }

        int input = GetHit();
        if (input != 0 && timer.NowPos >= Downnotes[input - 1].hitTiming - 192)
        {
            if      (timer.NowPos <= Downnotes[input - 1].hitTiming - 168) panjeong = "BAD";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming - 100) panjeong = "GOOD";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming - 48) panjeong = "GREAT";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 48) panjeong = "PERFECT";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 100) panjeong = "GREAT";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 168) panjeong = "GOOD";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 192) panjeong = "BAD";
            else panjeong = "MISS";

            Debug.Log(panjeong);
            Downnotes[input - 1].line = -1;
        }
        if (input == 0)
        {
            foreach(NoteInfo n in Downnotes)
            {
                if(n.line != -1 && timer.NowPos >= n.hitTiming + 192)
                {
                    panjeong = "MISS";
                    Debug.Log(panjeong);
                    n.line = -1;
                }
            }
        }
    }
}

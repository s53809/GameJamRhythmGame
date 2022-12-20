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
    NoteInfo[] Downnotes = new NoteInfo[6] { new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo() };

    string panjeong;

    private void Start()
    {
        noteSpawner = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteSpawner>();
        reader = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteReader>();
        timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
    }

    private void GetHit()
    {
        if (Input.GetKeyDown(KeyCode.Z))          CheckHit(1);
        if (Input.GetKeyDown(KeyCode.X))          CheckHit(2);
        if (Input.GetKeyDown(KeyCode.Period))     CheckHit(3);
        if (Input.GetKeyDown(KeyCode.Slash))      CheckHit(4);
        if (Input.GetKeyDown(KeyCode.LeftShift))  CheckHit(5);
        if (Input.GetKeyDown(KeyCode.RightShift)) CheckHit(6);

        return;
    }

    private void CheckHit(int input)
    {
        if (input != 0 && timer.NowPos >= Downnotes[input - 1].hitTiming - 192)
        {
            if (timer.NowPos <= Downnotes[input - 1].hitTiming - 168) panjeong = "BAD";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming - 100) panjeong = "GOOD";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming - 48) panjeong = "GREAT";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 48) panjeong = "PERFECT";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 100) panjeong = "GREAT";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 168) panjeong = "GOOD";
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 192) panjeong = "BAD";
            else panjeong = "MISS";

            Debug.Log(input);
            Debug.Log(panjeong);
            Downnotes[input - 1].line = NoteLine.None;
        }

        return;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) reader.ReadLis(ref notes, "Assets/Scripts/Ozi's Scene/example");
        
        while (notes.Count > 0 && Downnotes[(int)notes.First().line - 1].line == NoteLine.None)
        {
            Downnotes[(int)notes.First().line - 1] = notes.Dequeue();
        }

        GetHit();

        foreach (NoteInfo n in Downnotes)
        {
            if (n.line != NoteLine.None && timer.NowPos >= n.hitTiming + 192)
            {
                panjeong = "MISS";
                Debug.Log(panjeong);
                n.line = NoteLine.None;
            }
        }
    }
}

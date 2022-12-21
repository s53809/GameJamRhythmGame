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
    SoundManager SM = null;
    GameManagerEx GM = null;

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();
    NoteInfo[] Downnotes = new NoteInfo[6] { new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo() };

    private int panjeong;

    private void Start()
    {
        noteSpawner = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteSpawner>();
        reader = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteReader>();
        timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
        SM = SoundManager.GetInstance();
        GM = GameManagerEx.GetInstance();
    }

    private void GetHit()
    {
        if (Input.GetKeyDown(KeyCode.Z))          { SM.PlaySFX("hitogg"); CheckHit(1); }
        if (Input.GetKeyDown(KeyCode.X))          { SM.PlaySFX("hitogg"); CheckHit(2); }
        if (Input.GetKeyDown(KeyCode.Period))     { SM.PlaySFX("hitogg"); CheckHit(3); }
        if (Input.GetKeyDown(KeyCode.Slash))      { SM.PlaySFX("hitogg"); CheckHit(4); }
        if (Input.GetKeyDown(KeyCode.LeftShift))  { SM.PlaySFX("hitogg"); CheckHit(5); }
        if (Input.GetKeyDown(KeyCode.RightShift)) { SM.PlaySFX("hitogg"); CheckHit(6); }

        return;
    }

    private void CheckHit(int input)
    {
        if (input != 0 && timer.NowPos >= Downnotes[input - 1].hitTiming - 192)
        {
            if      (timer.NowPos <= Downnotes[input - 1].hitTiming - 168)  panjeong = 20;
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming - 100)  panjeong = 50;
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming - 48)   panjeong = 80;
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 48)   panjeong = 100;
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 100)  panjeong = 80;
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 168)  panjeong = 50;
            else if (timer.NowPos <= Downnotes[input - 1].hitTiming + 192)  panjeong = 20;
            else panjeong = 0;

            Debug.Log(panjeong);
            if (panjeong != 0) GM.AddScore(panjeong);
            Downnotes[input - 1].line = NoteLine.None;
        }

        return;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) reader.ReadLis(ref notes, "Assets/Scripts/Ozi's Scene/example");
        
        while (notes.Count > 0 && Downnotes[(int)notes.First().line - 1].line == NoteLine.None)
        {
            Downnotes[(int)notes.First().line - 1] = notes.Dequeue();
        }

        GetHit();

        foreach (NoteInfo n in Downnotes)
        {
            if (n.line != NoteLine.None && timer.NowPos >= n.hitTiming + 192)
            {
                panjeong = 0;
                Debug.Log(panjeong);
                n.line = NoteLine.None;
            }
        }
    }
}

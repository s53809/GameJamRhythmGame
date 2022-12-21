using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    SoundTimer timer = null;
    NoteReader reader = null;
    SoundManager SM = null;
    GameManagerEx GM = null;

    private float crotchet;

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();
    NoteInfo[] Downnotes = new NoteInfo[6] { new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo() };

    private int panjeong;

    private Tuple<bool, KeyCode>[] checkLongLine
        = new Tuple<bool, KeyCode>[6] 
        { new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None) };
    private IEnumerator CheckLong;

    private void Start()
    {
        reader = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteReader>();
        timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
        SM = SoundManager.GetInstance();
        GM = GameManagerEx.GetInstance();
    }

    private void GetHit()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))  { CheckHit(0); }
        if (Input.GetKeyDown(KeyCode.Z))          { CheckHit(1); }
        if (Input.GetKeyDown(KeyCode.X))          { CheckHit(2); }
        if (Input.GetKeyDown(KeyCode.Period))     { CheckHit(3); }
        if (Input.GetKeyDown(KeyCode.Slash))      { CheckHit(4); }
        if (Input.GetKeyDown(KeyCode.RightShift)) { CheckHit(5); }

        return;
    }

    private void GetLongHit(int line, int pj)
    {
        KeyCode key;

        switch(line)
        {
            case 0: key = KeyCode.LeftShift;    break;
            case 1: key = KeyCode.Z;            break;
            case 2: key = KeyCode.X;            break;
            case 3: key = KeyCode.Period;       break;
            case 4: key = KeyCode.Slash;        break;
            case 5: key = KeyCode.RightShift;   break;

            default: key = KeyCode.None; break;
        }

        CheckLong = CheckLongC(key, line, pj);
        StartCoroutine(CheckLong);
    }

    IEnumerator CheckLongC(KeyCode key, int line, int pj)
    {
        checkLongLine[line] = new Tuple<bool, KeyCode>(true, key);

        while (Input.GetKey(key))
        {
            yield return new WaitForSeconds(crotchet / 1000);
            GM.AddScore(pj);
        }

        yield return null;
    }

    private void CheckHit(int input)
    {
        if (input != -1 && timer.NowPos >= Downnotes[input].hitTiming - 192)
        {
            if      (timer.NowPos <= Downnotes[input].hitTiming - 168)  panjeong = 20;
            else if (timer.NowPos <= Downnotes[input].hitTiming - 100)  panjeong = 50;
            else if (timer.NowPos <= Downnotes[input].hitTiming - 48)   panjeong = 80;
            else if (timer.NowPos <= Downnotes[input].hitTiming + 48)   panjeong = 100;
            else if (timer.NowPos <= Downnotes[input].hitTiming + 100)  panjeong = 80;
            else if (timer.NowPos <= Downnotes[input].hitTiming + 168)  panjeong = 50;
            else if (timer.NowPos <= Downnotes[input].hitTiming + 192)  panjeong = 20;
            else panjeong = 0;

            if (panjeong != 0) { GM.AddScore(panjeong); };
            if (Downnotes[input].noteTrans == NoteTrans.Long) GetLongHit(input, panjeong);
            Downnotes[input].line = NoteLine.None;
        }

        return;
    }
    
    private void Update()
    {
        for(int i = 0; i < checkLongLine.Length; i++)
        {
            if (checkLongLine[i].Item1) 
                if (Input.GetKeyUp(checkLongLine[i].Item2))
                {
                    StopCoroutine(CheckLong);
                    checkLongLine[i] = new Tuple<bool, KeyCode>(false, checkLongLine[i].Item2); //롱노트 종료 후 검사하지 않게 함
                    CheckHit(i);
                }
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        { 
            reader.ReadLis(ref notes, "Assets/Resources/Liss/Snowy/Snowy.lis");
            crotchet = 60000 / reader.bpm;
            Debug.Log(crotchet);
        }

        while (notes.Count > 0 && Downnotes[(int)notes.First().line].line == NoteLine.None)
        {
            Downnotes[(int)notes.First().line] = notes.Dequeue();
        }

        GetHit();

        foreach (NoteInfo n in Downnotes)
        {
            if (n.line != NoteLine.None && timer.NowPos >= n.hitTiming + 192)
            {
                panjeong = 0;
                GM.AddScore(panjeong);
                n.line = NoteLine.None;
            }
        }

    }
}
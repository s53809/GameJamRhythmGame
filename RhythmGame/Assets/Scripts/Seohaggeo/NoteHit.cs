using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteHit : MonoBehaviour
{
    //싱글톤
    private static NoteHit instance = null;
    public static NoteHit GetInstance()
    {
        if (!instance)
        {
            instance = (NoteHit)GameObject.FindObjectOfType(typeof(NoteHit));
            if (!instance)
                Debug.Log("오류");
        }
        return instance;
    }

    SoundTimer timer = null;
    NoteSpawner spawner = null;
    NoteReader reader = null;
    GameManagerEx game = null;
    SnowSpawner snow = null;

    public float crotchet;

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();
    NoteInfo[] Downnotes = new NoteInfo[6] { new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo(), new NoteInfo() };

    private int panjeong;

    private Tuple<bool, KeyCode>[] checkLongLine
        = new Tuple<bool, KeyCode>[6] 
        { new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None), new Tuple<bool, KeyCode>(false, KeyCode.None) };
    private IEnumerator CheckLong;

    private void Start()
    {
        spawner = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteSpawner>();
        reader = GameObject.Find(NoteSpawner.NOTESPAWNER_NAME).GetComponent<NoteReader>();
        timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
        game = GameManagerEx.GetInstance();
        snow = GameObject.Find(SnowSpawner.SNOWSPAWNER_NAME).GetComponent<SnowSpawner>();
    } // 객체를 가져와요

    private void GetHit()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))  { CheckHit(0); }
        if (Input.GetKeyDown(KeyCode.Z))          { CheckHit(1); }
        if (Input.GetKeyDown(KeyCode.X))          { CheckHit(2); }
        if (Input.GetKeyDown(KeyCode.Period))     { CheckHit(3); }
        if (Input.GetKeyDown(KeyCode.Slash))      { CheckHit(4); }
        if (Input.GetKeyDown(KeyCode.RightShift)) { CheckHit(5); }

        return;
    } // 키보드 눌럿나요

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

            game.AddScore(panjeong);
            if (Downnotes[input].noteTrans == NoteTrans.Long) GetLongHit(input, panjeong);

            switch (panjeong)
            {
                case 0: case 20: snow.SnowPop(); break;
                default: if (Downnotes[input].noteType == NoteType.Normal_Snow) snow.SnowHit(); break;
            }
            
            Downnotes[input].line = NoteLine.None;
        }

        return;
    } // 판정검사해요
    
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
    } // 롱노트 치고잇나요

    IEnumerator CheckLongC(KeyCode key, int line, int pj)
    {
        checkLongLine[line] = new Tuple<bool, KeyCode>(true, key);

        while (Input.GetKey(key))
        {
            yield return new WaitForSeconds(crotchet / 1000);
            game.AddScore(pj);
        }

        yield return null;
    } // 롱노트 감지하는 코루틴

    private void Update()
    {
        if (notes.Count == 0 && spawner.notes.Count != 0)
        {
            notes = spawner.notes;
            crotchet = 60000 / reader.bpm;
        }
        while (notes.Count > 0 && Downnotes[(int)notes.First().line].line == NoteLine.None) // 지금 내려오고있는 노트는?
            Downnotes[(int)notes.First().line] = notes.Dequeue();
       
        GetHit();

        for(int i = 0; i < checkLongLine.Length; i++)
        {
            if (checkLongLine[i].Item1) 
                if (Input.GetKeyUp(checkLongLine[i].Item2))
                {
                    StopCoroutine(CheckLong);
                    checkLongLine[i] = new Tuple<bool, KeyCode>(false, checkLongLine[i].Item2); //롱노트 종료 후 검사하지 않게 함
                    if (timer.NowPos >= Downnotes[i].hitTiming - 192) CheckHit(i);
                    else
                    {
                        panjeong = 0;
                        game.AddScore(panjeong);
                        snow.SnowPop();
                        Downnotes[i].line = NoteLine.None;
                    }
                }
        } // 롱노트 누르다가 뗏나요

        foreach (NoteInfo n in Downnotes)
        {
            if (n.line != NoteLine.None && timer.NowPos >= n.hitTiming + 192)
            {
                panjeong = 0;
                game.AddScore(panjeong);
                snow.SnowPop();
                n.line = NoteLine.None;
            }
        } // 노트 안치고 그냥 내려갔는지 알아봐요
    }
}
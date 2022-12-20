using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

public class NoteReader : MonoBehaviour
{
    [Header("Normal Note")]
    public GameObject normalNote;
    public GameObject normalLongNote;

    [Header("Side Note")]
    public GameObject sideNote;
    public GameObject sideLongNote;

    [Header("Snow Note")]
    public GameObject snowNote;
    public GameObject snowLongNote;
    public GameObject snowSideNote;
    public GameObject snowSideLongNote;

    public const float NOTE_DISTANCE = 1.8f;
    public const char INFO_SEPARATOR = ':';
    public const char NOTE_SEPARATOR = ',';
    public const int ERROR_NUM = -1;
    
    [Header("Lis Info")]
    [ReadOnly] public string artistName = "ArtistName";
    [ReadOnly] public string songName = "SongName";
    [ReadOnly] public string songPath = "SongPath";
    [ReadOnly] public float bpm = 0.0f;
    [ReadOnly] public int offset = 0;

    private List<Tuple<NoteInfo, GameObject>> LongStart = new();
    private List<Tuple<NoteInfo, GameObject>> LongEnd = new();

    public Queue<NoteInfo> ReadLis(ref Queue<NoteInfo> notes, string path)
    {
        StreamReader reader = new(path + ".lis");

        // lis 파일 작성 형식 
        /* example.lis
         * ArtistName : Plum
         * SongName   : R
         * SongPath   : None
         * Bpm    : 120.0
         * Offset : 0
         * 
         * [ NOTE ]
         * 1, 1000, 0, 0
         * 2, 2000, 1, 0
         * 3, 3000, 0, 0
         * 4, 4000, 0, 0
         * 
         * 
         */

        /* [ Info Setting ] */ {
            // ArtistName (separator) (String)
            try {
                artistName = reader.ReadLine().Split(INFO_SEPARATOR)[1];
                /* [ 앞 공백 제거 ] */
                while (artistName[0].Equals(' ')) { artistName = artistName.Substring(1); }
            }
            catch (Exception e)
            {
                artistName = ERROR_NUM.ToString();
                Debug.Log("ArtistName : " + e.Message);
            }

            // SongName (separator) (String)
            try {
                songName = reader.ReadLine().Split(INFO_SEPARATOR)[1];
                while (songName[0].Equals(' ')) { songName = songName.Substring(1); }
            }
            catch (Exception e)
            {
                songName = ERROR_NUM.ToString();
                Debug.Log("SongName : " + e.Message);
            }

            // SongPath (separator) (String)
            try {
                songPath = reader.ReadLine().Split(INFO_SEPARATOR)[1];
                while (songPath[0].Equals(' ')) { songPath = songPath.Substring(1); }
            }
            catch (Exception e)
            {
                songPath = ERROR_NUM.ToString();
                Debug.Log("SongPath : " + e.Message);
            }

            // Bpm (separator) (Single)
            try { bpm = Convert.ToSingle(reader.ReadLine().Split(INFO_SEPARATOR)[1]); }
            catch (Exception e)
            {
                bpm = ERROR_NUM;
                Debug.Log("Bpm : " + e.Message);
            }

            // Offset (separator) (Int32)
            try { offset = Convert.ToInt32(reader.ReadLine().Split(INFO_SEPARATOR)[1]); }
            catch (Exception e)
            {
                offset = ERROR_NUM;
                Debug.Log("Offset : " + e.Message);
            }
        }
        /* [ Loading Note ] */ {
            bool isStart = false;
            string[] texts;

            while(!isStart) {
                texts = reader.ReadLine().Split(' ');

                for(int i = 0; i < texts.Length; i++)
                {
                    if (texts[i].ToUpper().Equals("NOTE")) { isStart = true; break; }
                }

                if (reader.EndOfStream) { throw new Exception("\"NOTE\" Not Found In .lis File"); }
            }

            string text;
            float delay = (bpm * NoteDown.SPEED * NoteDown.MULTIPLE) + offset;
            
            while (true) {
                text = reader.ReadLine();
                if(text == null) { break; }

                GameObject note = null;
                
                string[] splitText = text.Split(',');

                // [ Note Visual ]
                try
                {
                    switch ((NoteType)Convert.ToInt32(splitText[2]))
                    {
                        case NoteType.Normal: { note = Instantiate(normalNote); } break;
                        case NoteType.Snow: { note = Instantiate(snowNote); } break;
                        case NoteType.Side: { note = Instantiate(sideNote); } break;
                    }
                }
                catch (Exception e) { Debug.Log("Type Error : " + e.Message); }

                note.transform.parent = transform;

                NoteInfo info = note.AddComponent<NoteInfo>();

                // [ Note Type ]
                try { info.noteType = (NoteType)Convert.ToInt32(splitText[2]); }
                catch (Exception e) { Debug.Log("Type Error : " + e.Message); }

                // [ Line ]
                Vector3 pos = Vector3.zero;
                try
                {
                    if(info.noteType == NoteType.Side)
                    {
                        switch ((NoteLine)Convert.ToInt32(splitText[0]))
                        {
                            case NoteLine.One:
                            case NoteLine.Two:  { pos = new Vector3(NOTE_DISTANCE * -1.0f, delay, 0); info.line = NoteLine.LeftSide; } break;
                            case NoteLine.Three:
                            case NoteLine.Four: { pos = new Vector3(NOTE_DISTANCE * 1.0f, delay, 0); info.line = NoteLine.RightSide; } break;
                            default: { throw new Exception(); }
                        }
                    }
                    else
                    {
                        switch ((NoteLine)Convert.ToInt32(splitText[0]))
                        {
                            case NoteLine.One:      { pos = new Vector3(NOTE_DISTANCE * -1.5f, delay, 0); } break;
                            case NoteLine.Two:      { pos = new Vector3(NOTE_DISTANCE * -0.5f, delay, 0); } break;
                            case NoteLine.Three:    { pos = new Vector3(NOTE_DISTANCE * 0.5f, delay, 0); } break;
                            case NoteLine.Four:     { pos = new Vector3(NOTE_DISTANCE * 1.5f, delay, 0); } break;
                            default: { throw new Exception(); }
                        }
                        info.line = (NoteLine)Convert.ToInt32(splitText[0]);
                    }
                    note.transform.position = pos;
                }
                catch(Exception e) { Debug.Log("Line Error : " + e.Message); }

                // [ Timing ]
                try { info.hitTiming = Convert.ToInt32(splitText[1]); }
                catch (Exception e) { Debug.Log("Timing Error : " + e.Message); }

                // [ Note Trans ]
                try
                {
                    switch ((NoteTrans)Convert.ToInt32(splitText[3]))
                    {
                        case NoteTrans.Long: {
                                GameObject @object = null;

                                switch (info.noteType)
                                {
                                    case NoteType.Normal:   { @object = Instantiate(normalLongNote); } break;
                                    case NoteType.Snow:     { @object = Instantiate(snowLongNote);  } break;
                                    case NoteType.Side:     { @object = Instantiate(sideLongNote);  } break;
                                }

                                if (@object != null) {
                                    @object.transform.parent = note.transform;
                                    @object.transform.position = new Vector3(pos.x, delay, 0);
                                    LongStart.Add(new Tuple<NoteInfo, GameObject>(info, @object));
                                }
                            }
                            break;
                        case NoteTrans.LongEnd: { LongEnd.Add(new Tuple<NoteInfo, GameObject>(info, note)); } break;
                    }

                    info.noteTrans = (NoteTrans)Convert.ToInt32(splitText[3]);
                }
                catch (Exception e) { Debug.Log("Trans Error : " + e.Message); }

                notes.Enqueue(info);
            }
        }
        /* [ Linking Long Note ] */ {
            if(LongStart.Count + LongEnd.Count % 2 == 1) { throw new Exception("롱노트의 시작과 끝이 맞지 않습니다."); }
            bool isChange;
            while(true)
            {
                isChange = false;

                if(LongEnd.Count > 0)
                foreach(var item in LongEnd)
                {
                    if(item.Item1.line == LongStart[0].Item1.line)
                    {
                        Transform trans = LongStart[0].Item2.transform;

                        float hit = (item.Item1.hitTiming - LongStart[0].Item1.hitTiming) * 0.01f;
                        trans.localScale = new Vector3(trans.localScale.x, hit * 2 ,trans.localScale.z);
                        trans.position += new Vector3(0, hit * 0.65f, 0);

                        LongEnd.Remove(item);
                        LongStart.Remove(LongStart[0]);
                        
                        isChange = true;
                            break;
                    }
                }

                if (LongStart.Count + LongEnd.Count <= 0) { break; }
                if (!isChange) { throw new Exception("해당 라인의 맞는 Long End를 찾지 못했습니다."); }
            }
        }

        return notes;
    }

}

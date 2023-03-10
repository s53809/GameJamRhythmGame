 using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
//using static TreeEditor.TreeEditorHelper;

public class NoteReader : MonoBehaviour
{
    [ReadOnly] public bool isRead = false;

    [Header("Normal Note")]
    public GameObject normalNote;
    public GameObject normalLongNote;

    [Header("Snow Note")]
    public GameObject snowNote;
    public GameObject snowLongNote;

    [Header("Side Note")]
    public GameObject normalSideNote;
    public GameObject normalSideLongNote;
    public GameObject snowSideNote;
    public GameObject snowSideLongNote;

    public const float NOTE_DISTANCE = 1.25f;
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

    public void ReadLis(ref Queue<NoteInfo> notes, string path)
    {
        TextAsset reader = Resources.Load<TextAsset>(path);
        string[] reads = reader.text.Split('\n');
        int index = 0;

        // lis File
        /* example.lis
         * ArtistName : (Artist)
         * SongName   : (Song)
         * SongPath   : (Path)
         * Bpm    : (BPM)
         * Offset : (offset)
         * 
         * [ NOTE ]
         * (Line), (Timing), (Type), (Trans)
         */

        /* [ Info Setting ] */ {
            // ArtistName (separator) (string)
            try
            {
                artistName = reads[index].Split(INFO_SEPARATOR)[1];
                /* [ ' ' Remove ] */
                while (artistName[0].Equals(' ')) { artistName = artistName.Substring(1); }
            }
            catch (Exception e)
            {
                artistName = ERROR_NUM.ToString();
                Debug.Log("ArtistName : " + e.Message);
            }
            index++;

            // SongName (separator) (string)
            try
            {
                songName = reads[index].Split(INFO_SEPARATOR)[1];
                while (songName[0].Equals(' ')) { songName = songName.Substring(1); }
            }
            catch (Exception e)
            {
                songName = ERROR_NUM.ToString();
                Debug.Log("SongName : " + e.Message);
            }
            index++;

            // SongPath (separator) (string)
            try
            {
                songPath = reads[index].Split(INFO_SEPARATOR)[1];
                while (songPath[0].Equals(' ')) { songPath = songPath.Substring(1); }
            }
            catch (Exception e)
            {
                songPath = ERROR_NUM.ToString();
                Debug.Log("SongPath : " + e.Message);
            }
            index++;

            // Bpm (separator) (float)
            try { bpm = Convert.ToSingle(reads[index].Split(INFO_SEPARATOR)[1]); }
            catch (Exception e)
            {
                bpm = ERROR_NUM;
                Debug.Log("Bpm : " + e.Message);
            }
            index++;

            // Offset (separator) (int)
            try { offset = Convert.ToInt32(reads[index].Split(INFO_SEPARATOR)[1]); }
            catch (Exception e)
            {
                offset = ERROR_NUM;
                Debug.Log("Offset : " + e.Message);
            }
            index++;
        }
        /* [ Loading Note ] */
        {
            bool isStart = false;

            while (!isStart)
            {
                string[] texts = reads[index].Split(' ');

                for (int i = 0; i < texts.Length; i++) { if (texts[i].ToUpper().Equals("NOTE")) { isStart = true; break; } }

                if (index >= reads.Length) { throw new Exception("\"NOTE\" Not Found In .lis File"); }
                index++;
            }

            float delay = (bpm * NoteDown.SPEED) * NoteDown.MULTIPLE + 10;

            while (true)
            {
                GameObject note = null;

                if (reads[index] == "") { break; }
                string[] splitText = reads[index].Split(',');

                // [ Note Visual ]
                try
                {
                    switch ((NoteType)Convert.ToInt32(splitText[2]))
                    {
                        case NoteType.Normal: { note = Instantiate(normalNote); } break;
                        case NoteType.Normal_Snow: { note = Instantiate(snowNote); } break;
                        case NoteType.Side: { note = Instantiate(normalSideNote); } break;
                        case NoteType.Side_Snow: { note = Instantiate(snowSideNote); } break;
                    }
                }
                catch (Exception e) { new Exception("Type Visual : " + e.Message + " " + splitText[2] != null ? splitText[2] : "null"); }

                note.transform.parent = transform;

                NoteInfo info = note.AddComponent<NoteInfo>();

                // [ Note Type ]
                try { info.noteType = (NoteType)Convert.ToInt32(splitText[2]); }
                catch (Exception e) { new Exception("Type Error : " + e.Message + " " + splitText[2] != null ? splitText[2] : "null"); }

                // [ Line ]
                try
                {
                    Vector3 pos = Vector3.zero;
                    if (info.noteType == NoteType.Side)
                    {
                        switch ((NoteLine)Convert.ToInt32(splitText[0]))
                        {
                            case NoteLine.LeftSide:
                            case NoteLine.One:
                            case NoteLine.Two: { pos = new Vector3(NOTE_DISTANCE * -1.0f, delay, 0); info.line = NoteLine.LeftSide; } break;
                            case NoteLine.RightSide:
                            case NoteLine.Three:
                            case NoteLine.Four: { pos = new Vector3(NOTE_DISTANCE * 1.0f, delay, 0); info.line = NoteLine.RightSide; } break;
                            default: { throw new Exception(splitText[0] != null ? splitText[0] : "null"); }
                        }
                    }
                    else
                    {
                        switch ((NoteLine)Convert.ToInt32(splitText[0]))
                        {
                            case NoteLine.One: { pos = new Vector3(NOTE_DISTANCE * -1.5f, delay, 0); } break;
                            case NoteLine.Two: { pos = new Vector3(NOTE_DISTANCE * -0.5f, delay, 0); } break;
                            case NoteLine.Three: { pos = new Vector3(NOTE_DISTANCE * 0.5f, delay, 0); } break;
                            case NoteLine.Four: { pos = new Vector3(NOTE_DISTANCE * 1.5f, delay, 0); } break;
                            case NoteLine.LeftSide: { pos = new Vector3(NOTE_DISTANCE * -1.0f, delay, 0); } break;
                            case NoteLine.RightSide: { pos = new Vector3(NOTE_DISTANCE * 1.0f, delay, 0); } break;
                            default: { throw new Exception(splitText[0] != null ? splitText[0] : "null"); }
                        }
                        info.line = (NoteLine)Convert.ToInt32(splitText[0]);
                    }
                    note.transform.position = pos;
                }
                catch (Exception e) { new Exception("Line Error : " + e.Message + " " + splitText[0] != null ? splitText[0] : "null"); }

                // [ Timing ]
                try
                {
                    info.hitTiming = Convert.ToInt32(splitText[1]) + offset;
                }
                catch (Exception e) { new Exception("Timing Error : " + e.Message + " " + splitText[1] != null ? splitText[1] : "null"); }

                // [ Note Trans ]
                try
                {
                    switch ((NoteTrans)Convert.ToInt32(splitText[3]))
                    {
                        case NoteTrans.Long:
                            {
                                GameObject @object = null;

                                switch (info.noteType)
                                {
                                    case NoteType.Normal: { @object = Instantiate(normalLongNote); } break;
                                    case NoteType.Normal_Snow: { @object = Instantiate(snowLongNote); } break;
                                    case NoteType.Side: { @object = Instantiate(normalSideLongNote); } break;
                                    case NoteType.Side_Snow: { @object = Instantiate(snowSideLongNote); } break;
                                }

                                if (@object != null)
                                {
                                    @object.transform.parent = note.transform;
                                    @object.transform.position = note.transform.position;
                                    LongStart.Add(new Tuple<NoteInfo, GameObject>(info, @object));
                                }
                            }
                            break;
                        case NoteTrans.LongEnd: {
                                GameObject @object = null;

                                switch (info.noteType)
                                {
                                    case NoteType.Normal: { @object = Instantiate(normalNote); } break;
                                    case NoteType.Normal_Snow: { @object = Instantiate(snowNote); } break;
                                    case NoteType.Side: { @object = Instantiate(normalSideNote); } break;
                                    case NoteType.Side_Snow: { @object = Instantiate(snowSideNote); } break;
                                }

                                if (@object != null)
                                {
                                    @object.transform.parent = note.transform;
                                    @object.transform.position = note.transform.position;
                                    LongEnd.Add(new Tuple<NoteInfo, GameObject>(info, note));
                                }
                                
                            } break;
                        case NoteTrans.Normal: { } break;
                    }

                    info.noteTrans = (NoteTrans)Convert.ToInt32(splitText[3]);
                }
                catch (Exception e) { new Exception("Trans Error : " + e.Message + " " + splitText[3] != null ? splitText[3] : "null"); }

                notes.Enqueue(info);

                index++;
                if (index >= reads.Length) { break; }
            }
        }
        /* [ Linking Long Note ] */ {
            if (LongStart.Count + LongEnd.Count % 2 == 1) { throw new Exception("Long Note Start and End is not Equal."); }
            bool isChange;
            while (true)
            {
                isChange = false;

                if (LongEnd.Count > 0)
                    foreach (var item in LongEnd)
                    {
                        if (item.Item1.line == LongStart[0].Item1.line)
                        {
                            Transform trans = LongStart[0].Item2.transform;

                            trans.localScale = new Vector3(trans.localScale.x, (item.Item1.hitTiming - LongStart[0].Item1.hitTiming) * NoteDown.Get(trans, 5.9f), trans.localScale.z);

                            LongEnd.Remove(item);
                            LongStart.Remove(LongStart[0]);

                            isChange = true;
                            break;
                        }
                    }

                if (LongStart.Count + LongEnd.Count <= 0) { break; }
                if (!isChange) { throw new Exception("Not Found Long Note End in Line. : " + LongStart[0].Item1.line.ToString()); }
            }
        }
        /* [ Delay ] */ {

            int Offset = 0;
            if (notes.Count > 0 && notes.First().hitTiming <= 1000)
            {
                Offset = 1001 - notes.First().hitTiming;
                foreach (var item in notes)
                { item.hitTiming += Offset; }
            }

            offset += Offset;
        }

        isRead = true;
    }
    public void Clear()
    {
        artistName = "ArtistName";
        songName = "SongName";
        songPath = "SongPath";
        bpm = 0.0f;
        offset = 0;
    }
}

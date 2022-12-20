using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static TreeEditor.TreeEditorHelper;

[System.Serializable]
public class NoteReader : MonoBehaviour
{
    public GameObject normalNote;
    public GameObject snowNote;
    public GameObject SideNote;

    public const float NOTE_DISTANCE = 1.0f;
    public const char INFO_SEPARATOR = ':';
    public const char NOTE_SEPARATOR = ',';
    public const int ERROR_NUM = -1;
    
    [Header("Lis Info")]
    [ReadOnly] public string artistName = "ArtistName";
    [ReadOnly] public string songName = "SongName";
    [ReadOnly] public string songPath = "SongPath";
    [ReadOnly] public float bpm = 0.0f;
    [ReadOnly] public int offset = 0;

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

                GameObject note = Instantiate(normalNote);
                note.transform.parent = transform;

                NoteInfo info = note.AddComponent<NoteInfo>();
                string[] splitText = text.Split(',');

                // [ Line ]
                note.transform.position = new Vector3(NOTE_DISTANCE * -1.5f, 0, 0);
                try
                {
                    switch (Convert.ToInt32(splitText[0]))
                    {
                        case 1: { note.transform.position = new Vector3(NOTE_DISTANCE * -1.5f, delay, 0); } break;
                        case 2: { note.transform.position = new Vector3(NOTE_DISTANCE * -0.5f, delay, 0); } break;
                        case 3: { note.transform.position = new Vector3(NOTE_DISTANCE *  0.5f, delay, 0); } break;
                        case 4: { note.transform.position = new Vector3(NOTE_DISTANCE *  1.5f, delay, 0); } break;
                        default: { throw new Exception(); }
                    }
                    info.line = Convert.ToInt32(splitText[0]);
                }
                catch(Exception e) { Debug.Log("Line Error : " + e.Message); }

                // [ Timing ]
                try { info.hitTiming = Convert.ToInt32(splitText[1]); }
                catch (Exception e) { Debug.Log("Timing Error : " + e.Message); }

                // [ Note Type ]
                try { info.noteType = (NoteType)Convert.ToInt32(splitText[2]); }
                catch (Exception e) { Debug.Log("Type Error : " + e.Message); }

                // [ Note Trans ]
                try { info.noteTrans = (NoteTrans)Convert.ToInt32(splitText[3]); }
                catch (Exception e) { Debug.Log("Trans Error : " + e.Message); }

                notes.Enqueue(info);
            }
        }

        return notes;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class NoteReader : MonoBehaviour
{
    public const char infoSeparator = ':';
    public const char noteSeparator = ',';
    public const int errorNum = -1;
    
    [Header("Lis Info")]
    [ReadOnly] public string artistName = "ArtistName";
    [ReadOnly] public string songName = "SongName";
    [ReadOnly] public string songPath = "SongPath";
    [ReadOnly] public float bpm = 0.0f;
    [ReadOnly] public int offset = 0;

    public Queue<NoteInfo> ReadLis(ref Queue<NoteInfo> notes, string path)
    {
        StreamReader reader = new(path + ".lis");
        //

        /* [ 기본적인 채보 Info 세팅 ] */ {
            // ArtistName (separator) (String)
            try {
                artistName = reader.ReadLine().Split(infoSeparator)[1];
                /* [ 앞 공백 제거 ] */
                while (artistName[0].Equals(' ')) { artistName = artistName.Substring(1); }
            }
            catch (Exception e)
            {
                artistName = errorNum.ToString();
                Debug.Log("ArtistName : " + e.Message);
            }

            // SongName (separator) (String)
            try {
                songName = reader.ReadLine().Split(infoSeparator)[1];
                while (songName[0].Equals(' ')) { songName = songName.Substring(1); }
            }
            catch (Exception e)
            {
                songName = errorNum.ToString();
                Debug.Log("SongName : " + e.Message);
            }

            // SongPath (separator) (String)
            try {
                songPath = reader.ReadLine().Split(infoSeparator)[1];
                while (songPath[0].Equals(' ')) { songPath = songPath.Substring(1); }
            }
            catch (Exception e)
            {
                songPath = errorNum.ToString();
                Debug.Log("SongPath : " + e.Message);
            }

            // Bpm (separator) (Single)
            try { bpm = Convert.ToSingle(reader.ReadLine().Split(infoSeparator)[1]); }
            catch (Exception e)
            {
                bpm = errorNum;
                Debug.Log("Bpm : " + e.Message);
            }

            // Offset (separator) (Int32)
            try { offset = Convert.ToInt32(reader.ReadLine().Split(infoSeparator)[1]); }
            catch (Exception e)
            {
                offset = errorNum;
                Debug.Log("Offset : " + e.Message);
            }
        }
        /* [ 노트 불러오기 ] */ {
            string text;
            do { text = reader.ReadLine(); }
            while (text != null && !text.ToUpper().Equals("[ NOTE ]"));

            while(true) {
                text = reader.ReadLine();
                if(text == null) { break; }

                NoteInfo note = new();
                string[] info = text.Split(',');
                
                // [ 라인 ]
                try { note.line = Convert.ToInt32(info[0]); }
                catch(Exception e) {
                    note.line = errorNum;
                    Debug.Log("Line Error : " + e.Message);
                }

                // [ 타이밍 ]
                try { note.spanwnTiming = Convert.ToInt32(info[1]); }
                catch (Exception e)
                {
                    note.spanwnTiming = errorNum;
                    Debug.Log("Line Error : " + e.Message);
                }

                // [ 노트 타입 ]
                try { note.noteType = (NoteType)Convert.ToInt32(info[2]); }
                catch (Exception e)
                {
                    note.noteType = (NoteType)errorNum;
                    Debug.Log("Line Error : " + e.Message);
                }

                // [ 노트 트랜스 ]
                try { note.noteTrans = (NoteTrans)Convert.ToInt32(info[3]); }
                catch (Exception e)
                {
                    note.noteTrans = (NoteTrans)errorNum;
                    Debug.Log("Line Error : " + e.Message);
                }

                notes.Enqueue(note);
            }
        }

        return notes;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class NoteReader : MonoBehaviour
{
    public const Char infoSeparator = ':';
    public const Char noteSeparator = ',';
    public const Int32 errorNum = -1;
    
    [Header("Lis Info")]
    [ReadOnly] public String artistName = "ArtistName";
    [ReadOnly] public String songName = "SongName";
    [ReadOnly] public String songPath = "SongPath";
    [ReadOnly] public Single bpm = 0.0f;
    [ReadOnly] public Int32 offset = 0;

    // 읽는다 파이노
    public Queue<NoteInfo> ReadLis(String path)
    {
        Queue<NoteInfo> notes = new Queue<NoteInfo>();

        StreamReader reader = new StreamReader(path + ".lis");
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
            string isStart = "";
            do { isStart = reader.ReadLine(); }
            while (isStart != null && !isStart.ToUpper().Equals("[ NOTE ]"));

        }

        //
        reader.Close();

        return notes;
    }
}

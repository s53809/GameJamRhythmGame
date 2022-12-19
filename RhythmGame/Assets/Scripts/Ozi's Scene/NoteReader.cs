using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

enum NoteTrans
{
    Normal,
    Long,
    LongEnd
}
enum NoteType
{
    Normal,
    Special,
    Side
}

public class NoteReader : MonoBehaviour
{
    public String songName;
    public String songPath;
    public Single bpm;
    public Int32 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 읽는다 파이노
    public void ReadPaino(String path)
    {
        StreamReader reader= new StreamReader(path);
        //

        songName = reader.ReadLine();
        songPath = reader.ReadLine();
        bpm = Convert.ToSingle(reader.ReadLine());
        offset = Convert.ToInt32(reader.ReadLine());

        //
        reader.Close();
    }
}

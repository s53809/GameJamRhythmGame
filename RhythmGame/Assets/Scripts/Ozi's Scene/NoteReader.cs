using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

enum NoteType
{
    Normal,
    Special,
    Long,
    LongEnd,
    Side,
    SideEnd
}

public class NoteReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �д´� ���̳�
    public void ReadPaino(string path)
    {
        StreamReader reader= new StreamReader(path);
        //

        string songname = reader.ReadLine();
        string songpath = reader.ReadLine();
        float bpm = reader.ReadLine();
        int offset = reader.ReadLine();

        //
        reader.Close();
    }
}

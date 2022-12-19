using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

enum NoteType
{
    Normal,
    Special,
    Long,
    Side
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

    // 읽는다 파이노
    public void ReadPaino(string path)
    {
        StreamReader reader= new StreamReader(path);
        //

        string line = reader.ReadLine();

        //
        reader.Close();
    }
}

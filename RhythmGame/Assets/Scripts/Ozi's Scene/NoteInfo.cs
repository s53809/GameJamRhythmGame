using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteType
{
    Normal,
    Special,
    Side
}
public enum NoteTrans
{
    Normal,
    Long,
    LongEnd
}

public class NoteInfo
{
    public NoteType  noteType  = NoteType .Normal;
    public NoteTrans noteTrans = NoteTrans.Normal;
    public int line         = NoteReader.errorNum;
    public int spanwnTiming = NoteReader.errorNum;

    public bool Spawn()
    {
        

        return true;
    }
}

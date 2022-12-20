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

public class NoteInfo : MonoBehaviour
{
    public NoteType  noteType  = NoteType .Normal;
    public NoteTrans noteTrans = NoteTrans.Normal;
    public int line         = NoteReader.ERROR_NUM;
    public int hitTiming = NoteReader.ERROR_NUM;

    public void Down(float bpm)
    {
        NoteDown down = gameObject.AddComponent<NoteDown>();
        down.bpm = bpm;

        Debug.Log("Note Down!");
    }
}

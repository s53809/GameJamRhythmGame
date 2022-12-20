using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoteLine
{
    None,
    One,
    Two,
    Three,
    Four,
    LeftSide,
    RightSide
}
public enum NoteType
{
    Normal,
    Normal_Snow,
    Side,
    Side_Snow
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
    public NoteLine line       = NoteLine .None;
    public int hitTiming = NoteReader.ERROR_NUM;

    public void Down(float bpm)
    {
        NoteDown down = gameObject.AddComponent<NoteDown>();
        down.bpm = bpm;

        Debug.Log("Note Down!");
    }
}

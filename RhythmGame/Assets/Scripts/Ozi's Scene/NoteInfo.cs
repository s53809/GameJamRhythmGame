using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum NoteType
{
    Normal,
    Special,
    Side
}
enum NoteTrans
{
    Normal,
    Long,
    LongEnd
}

public class NoteInfo
{
    private NoteType  noteType = NoteType.Normal;
    private NoteTrans noteTrans = NoteTrans.Normal;

    NoteInfo(NoteType type, NoteTrans trans) {
        noteType = type;
        noteTrans = trans;
    }

    public bool Spawn()
    {
        noteType = NoteType.Normal;
        noteTrans = NoteTrans.Normal;

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NoteReader))]
[RequireComponent(typeof(SoundTimer))]
public class NoteSpawner : MonoBehaviour
{
    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
    [ReadOnly] public SoundTimer timer;
    [ReadOnly] public int NoteCount = 0;

    [Header("Next Note Info")]
    [ReadOnly] public int nextLine;
    [ReadOnly] public int nextSpawnTiming;
    [ReadOnly] public NoteType  nextNoteType;
    [ReadOnly] public NoteTrans nextNoteTrans;

    private Queue<NoteInfo> notes = new Queue<NoteInfo>();

    void Awake()
    {
        reader = GetComponent<NoteReader>();
        timer = GetComponent<SoundTimer>();
    }

    void Update()
    {
        if(notes.Count > 0)
        {
            nextLine        = notes.First().line;
            nextSpawnTiming = notes.First().spanwnTiming;
            nextNoteType    = notes.First().noteType;
            nextNoteTrans   = notes.First().noteTrans;
        }
    }

    public bool LisRead(string path)
    {
        reader.ReadLis(ref notes, path);

        return true;
    }

    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
    {
        notes.Clear();
    }
}

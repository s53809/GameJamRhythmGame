using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NoteReader))]
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

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();

    void Start()
    {
        reader = GetComponent<NoteReader>();

        timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();

        LisRead("Assets/Scripts/Ozi's Scene/example");
    }

    void Update()
    {
        while (notes.Count > 0 && timer.NowPos >= (notes.First().hitTiming - 1000/*(ms)*/))
        {
            NoteInfo info = notes.First();

            nextLine = info.line;
            nextSpawnTiming = info.hitTiming;
            nextNoteType = info.noteType;
            nextNoteTrans = info.noteTrans;

            notes.First().Down(reader.bpm);
            notes.Dequeue();
        }
    }

    public bool LisRead(string path)
    {
        reader.ReadLis(ref notes, path);
        timer.Play(path, reader.offset);

        return true;
    }

    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
    {
        notes.Clear();
    }
}

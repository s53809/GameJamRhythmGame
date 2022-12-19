using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NoteReader))]
public class NoteSpawner : MonoBehaviour
{
    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
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
        notes = reader.ReadLis("Assets/Scripts/Ozi's Scene/example");
    }

    void Update()
    {
        nextNoteTrans = notes.First().noteTrans;
        nextNoteType = notes.First().noteType;
        nextSpawnTiming = notes.First().spanwnTiming;
        nextLine = notes.First().line;
    }

    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
    {
        notes.Clear();
    }
}

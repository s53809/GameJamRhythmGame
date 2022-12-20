using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Spawner")]
[RequireComponent(typeof(NoteReader))]
public class NoteSpawner : MonoBehaviour
{
    public const string NOTESPAWNER_NAME = "Note Spawner"; 

    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
    [ReadOnly] public SoundTimer timer;
    [ReadOnly] public int NoteCount = 0;

    [Header("Next Note Info")]
    [ReadOnly] public NoteLine nextLine;
    [ReadOnly] public int nextSpawnTiming;
    [ReadOnly] public NoteType  nextNoteType;
    [ReadOnly] public NoteTrans nextNoteTrans;

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();

    private void Awake()
    {
        GameObject @object = GameObject.Find(NOTESPAWNER_NAME);
        if (@object == null)
        {
            @object = new GameObject(NOTESPAWNER_NAME);

            NoteSpawner spanwer = @object.AddComponent<NoteSpawner>();

            spanwer.reader = @object.GetComponent<NoteReader>();
            spanwer.timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
            if(GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>() == null) { Debug.Log("Timer�� null"); }

            // �밡��
            spanwer.reader.normalNote = GetComponent<NoteReader>().normalNote;
            spanwer.reader.snowNote = GetComponent<NoteReader>().snowNote;
            spanwer.reader.sideNote = GetComponent<NoteReader>().sideNote;
            spanwer.reader.normalLongNote = GetComponent<NoteReader>().normalLongNote;
            spanwer.reader.snowLongNote = GetComponent<NoteReader>().snowLongNote;
            spanwer.reader.sideLongNote = GetComponent<NoteReader>().sideLongNote;

            Destroy(this);
            Destroy(GetComponent<NoteReader>());
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) { LisRead("Assets/Scripts/Ozi's Scene/example"); }

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

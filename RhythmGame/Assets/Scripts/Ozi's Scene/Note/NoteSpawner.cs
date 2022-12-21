using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NoteReader))]
public class NoteSpawner : MonoBehaviour
{
    public const string NOTESPAWNER_NAME = "Note Spawner";

    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
    [ReadOnly] public SoundTimer timer;
    [ReadOnly] public int NoteCount = 0;
    [ReadOnly] public int Time = 0;

    [Header("Next Note Info")]
    [ReadOnly] public NoteLine nextLine;
    [ReadOnly] public int nextSpawnTiming;
    [ReadOnly] public NoteType nextNoteType;
    [ReadOnly] public NoteTrans nextNoteTrans;

    public Queue<NoteInfo> notes = new Queue<NoteInfo>();

    private void Awake()
    {
        GameObject @object = GameObject.Find(NOTESPAWNER_NAME);
        if (@object == null)
        {
            @object = new GameObject(NOTESPAWNER_NAME);
            if (GameObject.Find("System") != null) { @object.transform.parent = GameObject.Find("System").transform; }

            NoteSpawner spanwer = @object.AddComponent<NoteSpawner>();

            spanwer.reader = @object.GetComponent<NoteReader>();
            spanwer.timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
            if (GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>() == null) { Debug.Log("Timer is null"); }

            NoteReader reader = GetComponent<NoteReader>();
            spanwer.reader.normalNote =     reader.normalNote;
            spanwer.reader.normalSideNote =       reader.normalSideNote;
            spanwer.reader.normalLongNote = reader.normalLongNote;
            spanwer.reader.normalSideLongNote =   reader.normalSideLongNote;
            spanwer.reader.snowNote =       reader.snowNote;
            spanwer.reader.snowLongNote =   reader.snowLongNote;
            spanwer.reader.snowSideNote =   reader.snowSideNote;
            spanwer.reader.snowSideLongNote =   reader.snowSideLongNote;

            Destroy(this);
            Destroy(GetComponent<NoteReader>());
        }
    }

    void Update()
    {
        if(timer != null) { Time = timer.NowPos; }
        if (Input.GetKeyDown(KeyCode.A)) { LisRead("Assets/Resources/Liss/Snowy/snowy.lis"); }

        while (notes.Count > 0 && timer.NowPos >= (notes.First().hitTiming - 1000/*(ms)*/))
        {
            NoteCount = notes.Count - 1;
            NoteInfo info = notes.First();

            nextLine = info.line;
            nextSpawnTiming = info.hitTiming;
            nextNoteType = info.noteType;
            nextNoteTrans = info.noteTrans;

            notes.First().Down(reader.bpm);
            notes.Dequeue();
        }
    }

    public void LisRead(string path)
    {
        try { reader.ReadLis(ref notes, path); }
        catch (Exception e) { Debug.Log("NoteSpawner.LisRead(string) : " + e.Message); } 
        finally { timer.Play(reader.songPath, reader.offset); }
    }

    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
    {
        notes.Clear();
    }
}

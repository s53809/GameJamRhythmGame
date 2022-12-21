using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine;
using Unity.VisualScripting;

[RequireComponent(typeof(NoteReader))]
public class NoteSpawner : MonoBehaviour
{
    public const string NOTESPAWNER_NAME = "Note Spawner";
    public const string RESULT_SCENE_NAME = "lis result";

    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
    [ReadOnly] public SoundTimer timer;
    [ReadOnly] public NoteHit hit;
    [ReadOnly] public int noteCount = 0;
    [ReadOnly] public int time = 0;

    [Header("Next Note Info")]
    [ReadOnly] public NoteLine nextLine;
    [ReadOnly] public int nextSpawnTiming;
    [ReadOnly] public NoteType nextNoteType;
    [ReadOnly] public NoteTrans nextNoteTrans;

    [Header("System Info")]
    [ReadOnly] public float endTime = 0.0f;

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

            hit = NoteHit.GetInstance();

            Destroy(this);
            Destroy(GetComponent<NoteReader>());
        }
    }

    void Update()
    {
        if(timer != null) { time = timer.NowPos; }

        while (notes.Count > 0 && timer.NowPos >= (notes.First().hitTiming - 1000/*(ms)*/))
        {
            noteCount = notes.Count - 1;
            NoteInfo info = notes.First();

            nextLine = info.line;
            nextSpawnTiming = info.hitTiming;
            nextNoteType = info.noteType;
            nextNoteTrans = info.noteTrans;

            notes.First().Down(reader.bpm);
            notes.Dequeue();
        }

        if(reader.isRead && notes.Count == 0)
        {
            endTime += Time.deltaTime;
            if(endTime > 3.0f) {
                // Reset //
                reader.isRead = false;
                timer.Stop();
                ///////////

                SceneManager.LoadScene(RESULT_SCENE_NAME);
            }
        }
    }

    public void LisRead(string path)
    {
        if(timer != null) { timer.Stop(); }

        try { reader.ReadLis(ref notes, path); }
        catch (Exception e) { Debug.Log("NoteSpawner.LisRead(string) : " + e.Message); } 
        finally { hit.notes = this.notes; hit.crotchet = 60000 / reader.bpm; timer.Play(reader.songPath, reader.offset); }
    }

    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
    {
        notes.Clear();
    }
}

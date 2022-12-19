using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NoteReader))]
public class NoteSpawner : MonoBehaviour
{
    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
    [ReadOnly] public int NoteCount = 0;
    private Queue<NoteInfo> notes = new Queue<NoteInfo>();

    void Awake()
    {
        reader = GetComponent<NoteReader>();
        notes = reader.ReadLis("Assets/Scripts/Ozi's Scene/example");
    }

    void Update()
    {
        NoteCount = notes.Count;
    }

    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
    {
        notes.Clear();
    }
}

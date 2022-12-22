using Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public const string NOTESPAWNER_NAME = "Note Spawner";
    public const string RESULT_SCENE_NAME = "lis result";
    public const string RESULT_SCENE_NAME = "Lis Result";

    [SerializeField] private bool isDebug = false;

    [Header("Note Reader")]
    [ReadOnly] public NoteReader reader;
@ -32,6 +34,11 @@ public class NoteSpawner : MonoBehaviour

    private void Awake()
    {
        if (isDebug)
        {
            if (Input.GetKeyUp(KeyCode.A)) { LisRead("Assets/Resources/Liss/PF.lis"); }
        }

        GameObject @object = GameObject.Find(NOTESPAWNER_NAME);
        if (@object == null)
        {
@ -39,6 + 46,7 @@ public class NoteSpawner : MonoBehaviour
            if (GameObject.Find("System") != null) { @object.transform.parent = GameObject.Find("System").transform; }

NoteSpawner spanwer = @object.AddComponent<NoteSpawner>();
spanwer.isDebug = isDebug;

spanwer.reader = @object.GetComponent<NoteReader>();
spanwer.timer = GameObject.Find(SoundTimer.SOUNDTIMER_NAME).GetComponent<SoundTimer>();
@ -83,10 + 91,7 @@ public class NoteSpawner : MonoBehaviour
{
    endTime += Time.deltaTime;
            if(endTime > 3.0f) {
                // Reset //
                reader.isRead = false;
                timer.Stop();
                ///////////
                SpawnerReset();

    SceneManager.LoadScene(RESULT_SCENE_NAME);
            }
@ -105,6 + 110,13 @@ public class NoteSpawner : MonoBehaviour
    [ContextMenu("Note Queue Clear")]
    private void NoteClear()
{
    SpawnerReset();
    notes.Clear();
}

private void SpawnerReset()
{
    reader.isRead = false;
    timer.Stop();
}
}

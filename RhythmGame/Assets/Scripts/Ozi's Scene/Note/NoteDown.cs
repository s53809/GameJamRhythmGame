using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDown : MonoBehaviour
{
    public const float SPEED = 5.0f;        // Note Fall Speed
    public const float MULTIPLE = 0.02f;    // SPEED Calibration
    public const float DEL_LINE = -10.0f;   // Note Delete Position Y

    [ReadOnly] public float bpm = 0.0f;
    [ReadOnly, SerializeField] private Vector3 beforePos;
    [ReadOnly, SerializeField] private Vector3 afterPos;

    private float now = 0.0f;

    private void Awake()
    {
        beforePos = transform.position;
        afterPos = new Vector3(beforePos.x, DEL_LINE, 0);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(beforePos, afterPos, now);
        if(now >= 1.0f) { Destroy(gameObject); }

        // Fall For 1 Seconds
        now += Time.deltaTime;
    }
}

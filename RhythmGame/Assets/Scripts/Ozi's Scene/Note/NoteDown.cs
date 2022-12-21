using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDown : MonoBehaviour
{
    public const float SPEED = 10.0f;        // Note Fall Speed
    public const float MULTIPLE = 0.01f;    // SPEED Calibration
    public const float JDG_LINE = -10.0f;   // Note JDG Position Y

    [ReadOnly] public float bpm = 0.0f;
    [ReadOnly, SerializeField] private Vector3 beforePos;
    [ReadOnly, SerializeField] private Vector3 afterPos;

    private float now = 0.0f;

    private void Awake()
    {
        beforePos = transform.position;
        afterPos = new Vector3(beforePos.x, JDG_LINE, 0);
    }

    void Update()
    {
        transform.position = BezierCurve(now, beforePos, afterPos);
        if(now >= 20.0f) { Destroy(gameObject); }

        // Fall For 1 Seconds
        now += Time.deltaTime;
    }

    Vector3 BezierCurve(float t, Vector3 start, Vector3 end)
    {
        return ((1 - t) * start) + (t * end);
    }

    public static float Get(Transform pos, float t)
    {
        return (pos.position.y - JDG_LINE) * (t * 0.001f);
    }
}

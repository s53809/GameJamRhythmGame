using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDown : MonoBehaviour
{
    public const float SPEED = 5.0f;        // ��Ʈ�� �������� �ӵ�
    public const float MULTIPLE = 0.02f;    // SPEED ���� �뵵
    public const float DEL_LINE = -10.0f;   // ��Ʈ�� ������� y��ǥ��

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

        // 1�ʰ� ������
        now += Time.deltaTime;
    }
}

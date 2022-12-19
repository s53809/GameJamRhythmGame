using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundTimer : MonoBehaviour
{
    SoundManager soundManager;
    private float StartPos = 0.0f;
    private float offset = 0.0f;
    private float pauseTime = 0.0f;

    public int NowPos => Convert.ToInt32(nowPos * 1000.0f);
    [SerializeField, ReadOnly] private float nowPos = 0.0f;
    
    public bool IsPlaying => isPlaying;
    private bool isPlaying = false;

    private void Start()
    {
        soundManager = SoundManager.GetInstance();
    }

    void Update()
    {
        if(isPlaying) { nowPos = (Time.time - StartPos) - offset; }
    }

    public void Play(string path, int offset)
    {
        if(soundManager != null) { soundManager.PlayBGM(path); }

        isPlaying = true;
        StartPos = Time.time;
        this.offset = (offset * 0.001f);
    }

    [ContextMenu("Note Time Play")]
    public void Play()
    {
        isPlaying = true;
        StartPos += Time.time - pauseTime;
    }

    [ContextMenu("Note Time Pause")]
    public void Pause()
    {
        isPlaying = false;
        pauseTime = Time.time;
    }

    [ContextMenu("Note Time Stop")]
    public void Stop()
    {
        isPlaying = false;
        nowPos = 0.0f;
        StartPos = 0.0f;
        pauseTime = 0.0f;
    }
}

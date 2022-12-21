using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SoundTimer : MonoBehaviour
{
    public const string SOUNDTIMER_NAME= "Sound Timer";

    SoundManager soundManager;
    private double StartPos = 0.0f;
    private double offset = 0.0f;
    private double pauseTime = 0.0f;

    public int NowPos => Convert.ToInt32(nowPos * 1000.0f);
    //                                  [    Trans ms     ]

    [SerializeField, ReadOnly] private double nowPos = 0.0f;
    
    public bool IsPlaying => isPlaying;
    private bool isPlaying = false;


    private void Awake()
    {
        GameObject @object = GameObject.Find(SOUNDTIMER_NAME);
        
        if(@object == null)
        {
            @object = new GameObject(SOUNDTIMER_NAME);
            if (GameObject.Find("System") != null) { @object.transform.parent = GameObject.Find("System").transform; }

            SoundTimer timer = @object.AddComponent<SoundTimer>();
            timer.soundManager = SoundManager.GetInstance();

            Destroy(this);
        }
    }

    void Update()
    {
        if(isPlaying) { nowPos = (AudioSettings.dspTime - StartPos) - offset; }
    }

    public void Play(string path, int offset)
    {
        try { if(soundManager != null) { soundManager.PlayMusic(path); } }
        catch(Exception e) { Debug.Log("SoundTimer.Play(string, int) : " + e.Message + " " + path); }
        finally {
            isPlaying = true;
            StartPos = AudioSettings.dspTime;
            this.offset = (offset * 0.001f);
        }
    }

    [ContextMenu("Note Time Play")]
    public void Play()
    {
        isPlaying = true;
        StartPos += AudioSettings.dspTime - pauseTime;
    }

    [ContextMenu("Note Time Pause")]
    public void Pause()
    {
        isPlaying = false;
        pauseTime = AudioSettings.dspTime;
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

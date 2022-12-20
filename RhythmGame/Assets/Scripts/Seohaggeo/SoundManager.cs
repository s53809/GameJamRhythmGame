using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //싱글톤
    private static SoundManager instance = null;
    public static SoundManager GetInstance()
    {
        if (!instance)
        {
            instance = (SoundManager)GameObject.FindObjectOfType(typeof(SoundManager));
            if (!instance)
                Debug.Log("오류");
        }
        return instance;
    }

    [SerializeField]
    [Header("clips"), Tooltip("오디오 클립")]
    public AudioClip[] BGM; // 배경음들
    public AudioClip[] SFX; // 효과음들

    private AudioSource BGMSource; // 배경음 재생할 오디오소스
    private List<AudioSource> SFXSource = new List<AudioSource>(); // 효과음
    private AudioSource tempSFXSource;

    private void OnEnable()
    {
        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.playOnAwake = false;
        BGMSource.loop = true;
    }

    private void Update()
    {
        for(int i = 0; i < SFXSource.Count; i++)
        {
            if (SFXSource[i] != null)
            {
                if (!SFXSource[i].isPlaying)
                {
                    Destroy(SFXSource[i]);
                    SFXSource.Remove(SFXSource[i]);
                }
            }
        }
    }

    public void PlayBGM(string name) // 이미 저장된 브금들을 이름으로 불러옵니다
    {
        foreach (AudioClip b in BGM)
        {
            if (b.name == name)
            {
                BGMSource.clip = b;
                BGMSource.Play();

                return;
            }
        }

        return;
    }

    public void PlayMusic(string path) // 리듬게임 음악을 경로로 불러옵니다
    {
        AudioClip music = Resources.Load<AudioClip>(path);

        if (music == null) throw new Exception("찾을 수 없음");
        
        BGMSource.clip = music;
        BGMSource.Play();

        return;
    }

    public void PlaySFX(string name)
    {
        foreach(AudioClip s in SFX)
        {
            if (s.name == name)
            {
                SFXSource.Add(gameObject.AddComponent<AudioSource>());
                SFXSource.Last().playOnAwake = false;
                SFXSource.Last().clip = s;
                SFXSource.Last().Play();

                return;
            }
        }

        SFX[SFX.Length] = Resources.Load<AudioClip>("Resources/AudioClips/" + name);
        tempSFXSource = gameObject.AddComponent<AudioSource>();
        tempSFXSource.playOnAwake = false;
        tempSFXSource.clip = SFX[SFX.Length];
        tempSFXSource.Play();
        SFXSource.Add(tempSFXSource);

        return;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //싱글톤
    private static SoundManager instance;
    public static SoundManager GetInstance()
    {
        if (!instance)
        {
            instance = (SoundManager)GameObject.FindObjectOfType(typeof(SoundManager));
            if (!instance)
                Debug.Log("오류");
        }
        Debug.Log("Fdsafdsa");
        return instance;
    }

    [SerializeField]
    [Header("clips"), Tooltip("오디오 클립")]
    public AudioClip[] BGM; // 배경음들
    public AudioClip[] SFX; // 효과음들

    private AudioSource BGMSource; // 배경음 재생할 오디오소스
    private AudioSource SFXSource; // 효과음
    private int sfxCount = 0;   // 재생중인 효과음 개수

    private void OnEnable()
    {
        BGMSource = gameObject.AddComponent<AudioSource>();
        BGMSource.playOnAwake = false;
        BGMSource.loop = true;
    }

    public void PlaySFX(string name)
    {
        for(int i = 0; i < SFX.Length; i++)
        {
            Debug.Log(SFX[i].name);
            if (SFX[i].name == name)
            {
                SFXSource = gameObject.AddComponent<AudioSource>();
                SFXSource.playOnAwake = false;
                SFXSource.clip = SFX[i];
                SFXSource.Play();
                if(!SFXSource.isPlaying) Destroy(SFXSource);
            }
        }
    }
}
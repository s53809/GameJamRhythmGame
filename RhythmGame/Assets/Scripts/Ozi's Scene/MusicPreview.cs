using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPreview : MonoBehaviour
{
    NoteReader reader;
    SoundManager sound;
    // 선택하는 클래스 불러와야함

    void Start()
    {
        reader = new();
        sound = new();
    }

    void Update()
    {
        /*
         if(만약 선택받은 곡이 변했다면) {
            reader.ReadLis(변한 곡 파일 경로(.lis 생략) : string);
            sound.PlayBGM(reader.songPath);
        }
         */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPreview : MonoBehaviour
{
    NoteReader reader;
    SoundManager sound;
    // �����ϴ� Ŭ���� �ҷ��;���

    void Start()
    {
        reader = new();
        sound = new();
    }

    void Update()
    {
        /*
         if(���� ���ù��� ���� ���ߴٸ�) {
            reader.ReadLis(���� �� ���� ���(.lis ����) : string);
            sound.PlayBGM(reader.songPath);
        }
         */
    }
}

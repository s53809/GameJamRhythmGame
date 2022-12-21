using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
    private float[] samples = new float[64];
    [SerializeField]
    private AudioSource audioSur;

    [SerializeField]
    private float leastSize = 0;

    [SerializeField]
    private float maximumSize = 0;

    float temp = 0;
    void Start()
    {
        StartCoroutine(update());
    }

    IEnumerator update()
    {
        while (true)
        {
            audioSur.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
            for (int i = 0; i < 64; i++)
            {
                temp += samples[i] * 100;
            }
            temp /= 64;
            Vector3 size = new Vector3(1, 1, 1) * (samples[32] * 100); //#todo: 로고 크기 커졌다 작아졌다 조절하기
            if (leastSize > size.y) //#todo: 메인화면 눈 내리게 하기
            {
                transform.localScale = new Vector3(leastSize, leastSize, leastSize);
            }
            else if (size.y > maximumSize)
            {
                transform.localScale = new Vector3(maximumSize, maximumSize, maximumSize);
            }
            else
            {
                transform.localScale = size;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}

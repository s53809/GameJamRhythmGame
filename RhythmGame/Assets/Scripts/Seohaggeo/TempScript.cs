using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    int timer = 0;
    SoundManager sm;

    private void Start()
    {
        sm = SoundManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        timer++;

        if(timer > 1000)
        {
            timer = 0;
            sm.PlaySFX("beeeeep");
        }
    }
}

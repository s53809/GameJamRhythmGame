using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManage : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        LoadSceneManager.LoadScene("SongSelectScene");
    }

    public void OnOptionButtonClick()
    {
        //LoadSceneManager.LoadScene("SongSelectScene");
    }

    public void OnCreditButtonClick()
    {
        //LoadSceneManager.LoadScene("SongSelectScene");
    }

}
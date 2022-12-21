using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx : MonoBehaviour
{
    //싱글톤
    private static GameManagerEx instance = null;
    public static GameManagerEx GetInstance()
    {
        if (!instance)
        {
            instance = (GameManagerEx)GameObject.FindObjectOfType(typeof(GameManagerEx));
            if (!instance)
                Debug.Log("오류");
        }
        return instance;
    }

    private const int standard = 100;
    public int scoreP = 0;
    public int noteC = 0;

    public void AddScore(int score)
    {
        Debug.Log(score);
        scoreP += score;
        noteC++;

        // Debug.Log(scoreP / noteC);
    }
}

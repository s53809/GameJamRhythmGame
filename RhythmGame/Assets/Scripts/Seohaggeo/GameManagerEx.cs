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

    SnowSpawner snow = null;

    public int scoreP = 0;
    public int noteC = 0;
    public float Accuracy = 0;
    public int Fever = 0;

    private void Start()
    {
        snow = GameObject.Find(SnowSpawner.SNOWSPAWNER_NAME).GetComponent<SnowSpawner>();
    }

    public void AddScore(int score)
    {
        scoreP += score;
        noteC++;
        Accuracy = scoreP / noteC;
        Fever += score / 10;
        Debug.Log("Score" + score);
    }

    private void Update()
    {
        if (Fever >= 400)
        {
            Debug.Log("Fever!");
            Fever -= 400;
            snow.SnowClear();
        }
    }
}

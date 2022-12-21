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

    [SerializeField]
    GameObject panjeong = null;

    SpriteRenderer showPJ = null;
    SnowSpawner snow = null;

    public int scoreP = 0;
    public int noteC = 0;
    public float Accuracy = 0;
    public int Fever = 0;

    private void Start()
    {
        showPJ = panjeong.GetComponent<SpriteRenderer>();
        snow = GameObject.Find(SnowSpawner.SNOWSPAWNER_NAME).GetComponent<SnowSpawner>();
    }

    public void AddScore(int score)
    {
        scoreP += score;
        noteC++;
        Accuracy = scoreP / noteC;
        Fever += score / 10;

        switch(score)
        {
            case 0: showPJ.color = new Color(0, 0, 0); break;
            case 20: showPJ.color = new Color(255, 0, 0); break;
            case 50: showPJ.color = new Color(0, 255, 0); break;
            case 80: showPJ.color = new Color(0, 0, 255); break;
            case 100: showPJ.color = new Color(255, 255, 255); break;
        }
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

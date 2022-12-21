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

    private const int standard = 100;
    public int scoreP = 0;
    public int noteC = 0;
    public float Accuracy = 0;

    private void Start()
    {
        showPJ = panjeong.GetComponent<SpriteRenderer>();
    }

    public void AddScore(int score)
    {
        Debug.Log(score);
        scoreP += score;
        noteC++;
        Accuracy = scoreP / noteC;

        switch(score)
        {
            case 0: showPJ.color = new Color(0, 0, 0); break;
            case 20: showPJ.color = new Color(255, 0, 0); break;
            case 50: showPJ.color = new Color(0, 255, 0); break;
            case 80: showPJ.color = new Color(0, 0, 255); break;
            case 100: showPJ.color = new Color(255, 255, 255); break;
        }

        // Debug.Log(scoreP / noteC);
    }
}

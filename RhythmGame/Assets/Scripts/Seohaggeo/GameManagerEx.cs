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
    public int Combo = 0;
    public int HighestCombo = 0;
    public int[] panzongs = new int[] { 0, 0, 0, 0, 0 };

    private int plusScore = 0;

    private void Start()
    {
        snow = GameObject.Find(SnowSpawner.SNOWSPAWNER_NAME).GetComponent<SnowSpawner>();
    }

    public void AddScore(int score)
    { // 100,80,50,20,0
        if(score == 100)
        {
            panzongs[0]++;
        }
        if(score == 80)
        {
            panzongs[1]++;
        }
        if (score == 50)
        {
            panzongs[2]++;
        }
        if (score == 20)
        {
            panzongs[3]++;
        }
        if (score == 0)
        {
            panzongs[4]++;
        }
        scoreP += score;
        noteC++;
        Accuracy = scoreP / noteC;
        Fever += score / 10;
        ComboSystem.instance.RefreshFeverGauge(Fever);
        ComboSystem.instance.RefreshRate((int)Accuracy);
        ComboSystem.instance.ShowPanzong(score);
        if (score > 20)
        {
            Combo++;
            ComboSystem.instance.RefreshCombo(Combo);
            if (Combo > HighestCombo) HighestCombo = Combo;
        }
        else Combo = 0;


    }

    private void Update()
    {
        if (Fever >= 400)
        {
            ComboSystem.instance.ShowFeverEffect();
            Fever -= 400;
            plusScore += snow.SnowClear();
        }
    }

    public int GetScore()
    {
        return (int)(Accuracy * 10000) + plusScore;
    }

    public int GetRank()
    {
        if      (Accuracy >= 90) return 5;
        else if (Accuracy >= 80) return 4;
        else if (Accuracy >= 60) return 3;
        else if (Accuracy >= 50) return 2;
        else if (Accuracy >= 30) return 1;

        return 0;
    }
}

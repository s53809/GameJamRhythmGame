using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ClearSceneManage : MonoBehaviour
{
    private SaveData finalScore;
    private int[] finalPanzong = new int[5] { 0, 0, 0, 0, 0 };
    
    private Text Title;
    private Text Artist;
    private Text HighScore;
    private Text Accurary;
    private Text Combo;
    private Image Rank;
    private Text Difficulty;
    private List<SongInfo> songList;

    private GameObject songInformation;

    private void Awake()
    {
        songList = SongManagement.instance.lists;
        songInformation = transform.GetChild(2).gameObject;
        Title = songInformation.transform.GetChild(0).GetComponent<Text>();
        Artist = songInformation.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        HighScore = songInformation.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        Accurary = songInformation.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        Combo = songInformation.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Text>();
        Rank = songInformation.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>();
        Difficulty = songInformation.transform.GetChild(0).GetChild(5).GetComponent<Text>();
    }
    void Start()
    {
        finalScore = SavingDataManagement.instance.lastScore;
        finalPanzong = SavingDataManagement.instance.lastPanzong;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ViewSongInfo(int index)
    {
        Title.DOText(songList[index].songName, 0.5f);
        Artist.DOText(songList[index].artistName, 0.5f);
        Difficulty.DOText(songList[index].difficulty, 0.5f);
        HighScore.DOText("Highscore : " + finalScore.highScore[0].ToString(), 0.5f);
        Accurary.DOText("Accurary : " + finalScore.accurary[0].ToString(), 0.5f);
        Combo.DOText("Best Combo : " + finalScore.combo[0].ToString(), 0.5f);
        //#todo : 랭크 결정하기
    }
}

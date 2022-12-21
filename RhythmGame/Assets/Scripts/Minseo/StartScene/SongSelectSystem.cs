using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class SongSelectSystem : MonoBehaviour
{
    private List<SongInfo> songList;
    
    [SerializeField] private GameObject songSelectPanel;

    [SerializeField] private GameObject songInformation;

    [SerializeField] private AudioClip[] songClips;

    private List<GameObject> songs;

    private int songIndex = 0;

    private float timer = 0;

    [SerializeField] private Sprite[] ranks;

    private Text Title;
    private Text Artist;
    private Text HighScore;
    private Text Accurary;
    private Text Combo;
    private Image Rank;
    private Text Difficulty;

    private void Awake()
    {
        songs = new List<GameObject>();
        songList = SongManagement.instance.lists;
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
        for(int i = 0; i < songList.Count; i++)
        {
            GameObject song = Instantiate(songSelectPanel, transform);
            song.GetComponent<RectTransform>().anchoredPosition = new Vector3(350, -i * 450, 0);
            song.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            song.transform.localRotation = Quaternion.Euler(0, 0, 0);
            song.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = songList[i].songBanner;
            song.transform.GetChild(0).GetComponent<Image>().sprite = songList[i].songCD;
            songs.Add(song);
        }

        songs[songIndex].transform.localScale = new Vector3(1, 1, 1);
        songs[songIndex].transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(200, 1f);
        ViewSongInfo(songIndex);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && songIndex != songs.Count - 1 && timer + 1 < Time.time)
        {
            timer = Time.time;
            for(int i = 0; i < songList.Count; i++)
            {
                songs[i].GetComponent<RectTransform>().DOAnchorPosY(songs[i].GetComponent<RectTransform>().anchoredPosition.y + 450, 1f);
            }
            OnChangeSong(songIndex + 1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && songIndex != 0 && timer + 1 < Time.time)
        {
            timer = Time.time;
            for (int i = 0; i < songList.Count; i++)
            {
                songs[i].GetComponent<RectTransform>().DOAnchorPosY(songs[i].GetComponent<RectTransform>().anchoredPosition.y - 450, 1f);
            }
            OnChangeSong(songIndex - 1);
        }
    }

    private void OnChangeSong(int index)
    {
        songs[songIndex].transform.localScale = new Vector3(0.8f, 0.8f, 1);
        songs[songIndex].transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(0, 1f);
        songIndex = index;
        songs[songIndex].transform.localScale = new Vector3(1, 1, 1);
        songs[songIndex].transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(200, 1f);
        ViewSongInfo(songIndex);
    }

    private void ViewSongInfo(int index)
    {
        Title.DOText(songList[index].songName, 0.5f);
        Artist.DOText(songList[index].artistName, 0.5f);
        Difficulty.DOText(songList[index].difficulty, 0.5f);
        SavingDataManagement.instance.JsonLoad();
        SaveData datas = SavingDataManagement.instance.lists;
        HighScore.DOText("Highscore : " + datas.highScore[index].ToString(), 0.5f);
        Accurary.DOText("Accurary : " + datas.accurary[index].ToString(), 0.5f);
        Combo.DOText("Best Combo : " + datas.combo[index].ToString(), 0.5f);
        Rank.sprite = ranks[datas.rank[index]];
    }
}

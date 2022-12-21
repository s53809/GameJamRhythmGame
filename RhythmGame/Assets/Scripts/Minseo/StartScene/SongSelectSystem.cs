using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SongSelectSystem : MonoBehaviour
{
    private List<SongInfo> songList;
    [SerializeField]
    private GameObject songSelectPanel;

    private List<GameObject> songs;

    private int songIndex = 0;

    private float timer = 0;
    void Start()
    {
        songs = new List<GameObject>();
        songList = SongManagement.instance.lists;
        for(int i = 0; i < songList.Count; i++)
        {
            GameObject song = Instantiate(songSelectPanel, transform);
            song.GetComponent<RectTransform>().anchoredPosition = new Vector3(350, -i * 450, 0);
            song.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            song.transform.localRotation = Quaternion.Euler(0, 0, 0);
            songs.Add(song);
        }

        songs[songIndex].transform.localScale = new Vector3(1, 1, 1);
        songs[songIndex].transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPosX(200, 1f);
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Song Data", menuName = "Scriptable Object/Song Data")]
public class SongInfo : ScriptableObject
{
    [SerializeField] private Sprite songBanner;
    [SerializeField] private Sprite songCD;
    [SerializeField] private string songPath;
    [SerializeField] private string artistName;
    [SerializeField] private string songName;
    [SerializeField] private string difficulty;
}

public class SongManagement : MonoBehaviour
{
    [SerializeField]
    private SongInfo lists;
    private void Start()
    {
        
    }
}

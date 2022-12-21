using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Song Data", menuName = "Scriptable Object/Song Data")]
public class SongInfo : ScriptableObject
{
    [SerializeField] private Image songBanner;
    [SerializeField] private Image songCD;
    [SerializeField] private string songPath;
    [SerializeField] private string artistName;
    [SerializeField] private string songName;
}

public class SongManagement : MonoBehaviour
{
    private void Start()
    {
        
    }
}

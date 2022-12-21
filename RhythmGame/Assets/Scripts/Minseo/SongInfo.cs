using UnityEngine;

[CreateAssetMenu(fileName = "Song Data", menuName = "Scriptable Object/Song Data", order = int.MaxValue)]
public class SongInfo : ScriptableObject
{
    [SerializeField] public Sprite songBanner;
    [SerializeField] public Sprite songCD;
    [SerializeField] public string songPath;
    [SerializeField] public string artistName;
    [SerializeField] public string songName;
    [SerializeField] public string difficulty;
}
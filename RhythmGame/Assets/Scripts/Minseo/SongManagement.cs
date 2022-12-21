using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongManagement : MonoBehaviour
{
    public static SongManagement instance;

    [SerializeField]
    public List<SongInfo> lists;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
    }
}

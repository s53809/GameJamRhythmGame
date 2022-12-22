using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    public string path;
    public int songNum;
    public int rankNum;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

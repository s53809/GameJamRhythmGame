using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    public string path;
    public int songNum;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

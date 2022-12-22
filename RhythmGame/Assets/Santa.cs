using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    public string path;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

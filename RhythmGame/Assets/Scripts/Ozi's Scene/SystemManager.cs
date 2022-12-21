using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public const string SYSTEMMANAGER_NAME = "System";

    void Awake()
    {
        GameObject @object = GameObject.Find(SYSTEMMANAGER_NAME);
        if(@object == null)
        {
            @object = new GameObject(SYSTEMMANAGER_NAME);
            DontDestroyOnLoad(@object);

            Destroy(this);
        }
    }
}

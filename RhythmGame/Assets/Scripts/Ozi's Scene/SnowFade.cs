using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SnowFade : MonoBehaviour
{
    SpriteRenderer sprite;
    public bool isFire = false;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite.color.a <= 0.001f) { Destroy(gameObject); }
        if (isFire) { sprite.DOFade(0.0f, 1.0f); }
    }


}

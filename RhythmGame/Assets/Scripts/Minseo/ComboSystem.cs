using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboSystem : MonoBehaviour
{
    private GameObject combo;

    private void Awake()
    {
        combo = transform.GetChild(0).GetChild(1).gameObject;
    }
    public void RefreshCombo(int N)
    {
        combo.transform.localScale = new Vector3(2, 2, 2);
        combo.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f);
        combo.GetComponent<Text>().text = N.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem instance;

    private GameObject combo;
    private TextMeshProUGUI rate;
    private GameObject fever;
    private Text feverEffect;

    private void Awake()
    {
        instance = this;
        combo = transform.GetChild(0).GetChild(1).gameObject;
        rate = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        fever = transform.GetChild(4).gameObject;
        feverEffect = transform.GetChild(5).GetComponent<Text>();
        feverEffect.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowFeverEffect();
        }
    }
    public void RefreshCombo(int N)
    {
        combo.transform.localScale = new Vector3(4, 4, 4);
        combo.transform.DOScale(new Vector3(2.5f, 2.5f, 2.5f), 0.1f);
        combo.GetComponent<TextMeshProUGUI>().text = N.ToString();
    }

    public void RefreshRate(int N)
    {
        rate.text = "RATE : " + N.ToString() + "%";
    }

    public void RefreshFeverGauge(int N)
    {
        fever.transform.localScale = new Vector3(N / 400f, 1, 1);
    }

    public void ShowFeverEffect()
    {
        feverEffect.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        feverEffect.DOFade(0, 0.4f);
        feverEffect.gameObject.SetActive(true);
        feverEffect.gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
        StartCoroutine(FeverTimer());
    }

    IEnumerator FeverTimer()
    {
        yield return new WaitForSeconds(0.4f);
        feverEffect.gameObject.SetActive(false);
        feverEffect.color = new Color(1, 1, 1, 1);
    }
}

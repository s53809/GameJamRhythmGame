using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using TMPro;
using Shapes;

public class ComboSystem : MonoBehaviour
{
    public static ComboSystem instance;

    [SerializeField] private GameObject line;

    private GameObject combo;
    private TextMeshProUGUI rate;
    private GameObject fever;
    private Text feverEffect;
    private GameObject panzongShow;

    private GameObject[] noteEffect = new GameObject[6];

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            noteEffect[i] = line.transform.GetChild(i).gameObject;
            noteEffect[i].SetActive(false);
        }
        instance = this;
        panzongShow = transform.GetChild(6).gameObject;
        for(int i = 0; i < 5; i++)
        {
            panzongShow.transform.GetChild(i).gameObject.SetActive(false);
        }
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

    public void ShowNoteEffect(int N)
    {
        if(N == 0 || N == 5) noteEffect[N].transform.localScale = new Vector3(1f, 0.1f, 1f);
        else noteEffect[N].transform.localScale = new Vector3(1f, 0.1f, 1f);
        noteEffect[N].SetActive(true);
        if (N == 0 || N == 5) noteEffect[N].transform.DOScale(new Vector3(1f, 1, 1), 0.1f);
        else noteEffect[N].transform.DOScale(new Vector3(1f, 1, 1), 0.1f);
    }
    public void HideNoteEffect(int N)
    {
        if (N == 0 || N == 5) noteEffect[N].transform.localScale = new Vector3(1f, 1f, 1f);
        else noteEffect[N].transform.localScale = new Vector3(1f, 1f, 1f);
        noteEffect[N].SetActive(false);
        if (N == 0 || N == 5) noteEffect[N].transform.DOScale(new Vector3(1f, 0.1f, 1), 0.1f);
        else noteEffect[N].transform.DOScale(new Vector3(1f, 0.1f, 1), 0.1f);
    }

    public void ShowPanzong(float N)
    {//100 80 50 20
        if(N == 100)
        {
            panzongShow.transform.GetChild(0).transform.localScale = new Vector3(2, 2, 2);
            panzongShow.transform.GetChild(0).gameObject.SetActive(true);
            panzongShow.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            StartCoroutine(deletePanzong(0));
        }
        else if (N == 80)
        {
            panzongShow.transform.GetChild(1).transform.localScale = new Vector3(2, 2, 2);
            panzongShow.transform.GetChild(1).gameObject.SetActive(true);
            panzongShow.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            StartCoroutine(deletePanzong(1));
        }
        else if (N == 50)
        {
            panzongShow.transform.GetChild(2).transform.localScale = new Vector3(2, 2, 2);
            panzongShow.transform.GetChild(2).gameObject.SetActive(true);
            panzongShow.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            StartCoroutine(deletePanzong(2));
        }
        else if (N == 20)
        {
            panzongShow.transform.GetChild(3).transform.localScale = new Vector3(2, 2, 2);
            panzongShow.transform.GetChild(3).gameObject.SetActive(true);
            panzongShow.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            StartCoroutine(deletePanzong(3));
        }
        else
        {
            panzongShow.transform.GetChild(4).transform.localScale = new Vector3(2, 2, 2);
            panzongShow.transform.GetChild(4).gameObject.SetActive(true);
            panzongShow.transform.GetChild(4).transform.DOScale(new Vector3(1, 1, 1), 0.1f);
            StartCoroutine(deletePanzong(4));
        }
    }

    IEnumerator deletePanzong(int N)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        panzongShow.transform.GetChild(N).gameObject.SetActive(false);
    }
}

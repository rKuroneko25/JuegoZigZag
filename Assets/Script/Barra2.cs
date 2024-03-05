using UnityEngine;
using UnityEngine.UI;

public class Barra2 : MonoBehaviour
{

    public Image progressBarImageBest;
    public Text progressTextBest;
    private float maxValue;

    void Start()
    {
        maxValue = 304f;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        //Barra pausa
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress2");
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress2") * 100f).ToString("F0") + "%";
    }
}
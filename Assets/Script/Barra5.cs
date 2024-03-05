using UnityEngine;
using UnityEngine.UI;

public class Barra5 : MonoBehaviour
{

    public Image progressBarImageBest;
    public Text progressTextBest;
    private float maxValue;

    void Start()
    {
        maxValue = 222f;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        //Barra pausa
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress5");
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress5") * 100f).ToString("F0") + "%";
    }
}
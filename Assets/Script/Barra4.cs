using UnityEngine;
using UnityEngine.UI;

public class Barra4 : MonoBehaviour
{

    public Image progressBarImageBest;
    public Text progressTextBest;
    private float maxValue;

    void Start()
    {
        maxValue = 224f;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        //Barra pausa
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress4");
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress4") * 100f).ToString("F0") + "%";
    }
}
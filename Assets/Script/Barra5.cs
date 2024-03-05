using UnityEngine;
using UnityEngine.UI;

public class Barra5 : MonoBehaviour
{

    public Image progressBarImageBest;
    public Text progressTextBest;

    void Start()
    {
        
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
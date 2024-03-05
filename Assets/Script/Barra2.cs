using UnityEngine;
using UnityEngine.UI;

public class Barra2 : MonoBehaviour
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
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress2");
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress2") * 100f).ToString("F0") + "%";
    }
}
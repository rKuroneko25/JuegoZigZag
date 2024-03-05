using UnityEngine;
using UnityEngine.UI;

public class Barra3 : MonoBehaviour
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
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress3");
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress3") * 100f).ToString("F0") + "%";
    }
}
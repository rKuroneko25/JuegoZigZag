using UnityEngine;
using UnityEngine.UI;

public class Barra1 : MonoBehaviour
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
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress1");
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress1") * 100f).ToString("F0") + "%";
    }
}
using UnityEngine;
using UnityEngine.UI;

public class BarraProgresoBest : MonoBehaviour
{

    public Image progressBarImageBest;
    public Text progressTextBest;
    private string Nivel;
    private float maxValue;

    void Start()
    {
        Nivel = PlayerPrefs.GetString("LevelSelected");
        switch (Nivel){
            case "1":
                //duration = 87f;
                maxValue = 220f;
                break;
            case "2":
                //duration = 100f;
                maxValue = 305f;
                break;
            case "3":
                //duration = 97f;
                maxValue = 288f;
                break;
            case "4":   
                //duration = 85f;
                maxValue = 224f;
                break;
            case "5":
                //duration = 93f;
                maxValue = 222f;
                break;
        }
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        //Barra pausa
        float bestValue = PlayerPrefs.GetFloat("Progress"+Nivel);
        progressBarImageBest.fillAmount = bestValue;
        progressTextBest.text = (bestValue * 100f).ToString("F0") + "%";
    }
}

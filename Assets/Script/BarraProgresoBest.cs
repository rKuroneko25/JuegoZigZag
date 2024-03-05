using UnityEngine;
using UnityEngine.UI;

public class BarraProgresoBest : MonoBehaviour
{

    public Image progressBarImageBest;
    public Text progressTextBest;
    private int Nivel;
    private float maxValue;

    void Start()
    {
        Nivel = PlayerPrefs.GetInt("LevelSelected");
        switch (Nivel){
            case 1:
                maxValue = 220f;
                break;
            case 2:
                //duration = 100f;
                maxValue = 304f;
                break;
            case 3:
                maxValue = 288f;
                break;
            case 4:    
                maxValue = 224f;
                break;
            case 5:
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
        progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress"+Nivel.ToString());
        progressTextBest.text = (PlayerPrefs.GetFloat("Progress"+Nivel.ToString()) * 100f).ToString("F0") + "%";
    }
}

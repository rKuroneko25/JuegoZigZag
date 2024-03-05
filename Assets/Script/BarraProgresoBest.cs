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
                maxValue = 219f;
                break;
            case 2:
                maxValue = 303f;
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
        if (PlayerPrefs.GetFloat("Progress"+Nivel.ToString()) < 0.000001f)
        {
            progressBarImageBest.fillAmount = 0.000001f;

        } else {
            progressBarImageBest.fillAmount = PlayerPrefs.GetFloat("Progress"+Nivel.ToString());
            progressTextBest.text = (PlayerPrefs.GetFloat("Progress"+Nivel.ToString()) * 100f).ToString("F0") + "%";
        }
    }
}

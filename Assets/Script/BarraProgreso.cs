using UnityEngine;
using UnityEngine.UI;

public class BarraProgreso : MonoBehaviour
{
    //private float duration;
    //private float elapsedTime = 0f;
    public Image progressBarImage;
    public Text progressText;
    public Image progressBarImageBest;
    public Text progressTextBest;
    private int Nivel;
    private float maxValue;

    void Start()
    {
        //Nivel = PlayerPrefs.GetString("LevelSelectedString");
        Nivel = PlayerPrefs.GetInt("LevelSelected");
        switch (Nivel){
            case 1:
                //duration = 87f;
                maxValue = 219f;
                break;
            case 2:
                //duration = 100f;
                maxValue = 303f;
                break;
            case 3:
                //duration = 97f;
                maxValue = 288f;
                break;
            case 4:   
                //duration = 85f;
                maxValue = 224f;
                break;
            case 5:
                //duration = 93f;
                maxValue = 222f;
                break;
        }
    }

    // void Update()
    // {
    //     elapsedTime += Time.deltaTime; // Incrementar el tiempo transcurrido

    //     float progress = Mathf.Clamp01(elapsedTime / duration);
        
    //     if (PlayerPrefs.GetFloat("Progress"+Nivel) < progress) {
    //         PlayerPrefs.SetFloat("Progress"+Nivel, progress);
    //     }

    //     if (progressText != null)
    //     {
    //         progressText.text = (progress * 100f).ToString("F0") + "%";
    //     }


    //     UpdateProgressBar(progress);
    // }

    // private void UpdateProgressBar(float progress)
    // {
    //     if (progressBarImage != null)
    //     {
    //         progressBarImage.fillAmount = progress;
    //     }
    // }

    void Update()
    {
        UpdateProgressBar();
    }

    public void UpdateProgressBar()
    {
        float contador = PlayerPrefs.GetFloat("CuentaPads");
        float newValue = Mathf.Clamp(contador, 0f, maxValue);
        
        float fillAmount = newValue / maxValue;

        if (PlayerPrefs.GetFloat("Progress"+Nivel.ToString()) < fillAmount) {
            PlayerPrefs.SetFloat("Progress"+Nivel.ToString(), fillAmount);
        }
        //Barra actual
        progressBarImage.fillAmount = fillAmount;
        float percentage = fillAmount * 100f;
        progressText.text = percentage.ToString("F0") + "%";
    }
}

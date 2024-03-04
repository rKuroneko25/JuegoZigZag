using UnityEngine;
using UnityEngine.UI;

public class BarraProgreso : MonoBehaviour
{
    private float duration;
    private float elapsedTime = 0f;
    public Image progressBarImage;
    public Text progressText;

    void Start()
    {
        switch (PlayerPrefs.GetString("LevelSelected")){
            case "1":
                duration = 87f;
                break;
            case "2":
                duration = 100f;
                break;
            case "3":
                duration = 97f;
                break;
            case "4":   
                duration = 85f;
                break;
            case "5":
                duration = 93f;
                break;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime; // Incrementar el tiempo transcurrido

        float progress = Mathf.Clamp01(elapsedTime / duration);

        if (progressText != null)
        {
            progressText.text = (progress * 100f).ToString("F0") + "%";
        }

        UpdateProgressBar(progress);
    }

    private void UpdateProgressBar(float progress)
    {
        if (progressBarImage != null)
        {
            progressBarImage.fillAmount = progress;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //detiene la musica del audiomanager
        AudioManager.instance.Stop("Musica");

        Button button = GetComponent<Button>();

        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt("Vidas", 3);
        PlayerPrefs.SetInt("Coin", 0);
    }
}

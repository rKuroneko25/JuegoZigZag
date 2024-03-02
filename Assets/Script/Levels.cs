using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{

    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //detiene la musica del audiomanager
        AudioManager.instance.Play("Menu");

        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("LevelSelector");
    }
}

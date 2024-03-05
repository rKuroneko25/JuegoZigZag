using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonRestartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(RestartLevel);
    }

    void RestartLevel()
    {
        FindObjectOfType<AudioManager>().Stop("Level"+PlayerPrefs.GetString("LevelSelectedString"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonResumeLevel : MonoBehaviour
{
    public GameObject GUI;
    public GameObject PauseInt;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {   
        FindObjectOfType<AudioManager>().UnPause("Level"+PlayerPrefs.GetString("LevelSelected"));
        PlayerPrefs.SetInt("Quieto",0);
        GUI.SetActive(true);
        PauseInt.SetActive(false);
    }
}

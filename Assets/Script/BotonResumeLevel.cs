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
        FindObjectOfType<AudioManager>().UnPause("Level"+PlayerPrefs.GetString("LevelSelectedString"));
        PlayerPrefs.SetInt("Quieto",0);
        PlayerPrefs.SetInt("Clic",1);
        GUI.SetActive(true);
        PauseInt.SetActive(false);
    }
}

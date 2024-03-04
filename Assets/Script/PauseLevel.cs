using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour
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
        FindObjectOfType<AudioManager>().Pause("Level"+PlayerPrefs.GetString("LevelSelectedString"));
        PlayerPrefs.SetInt("Quieto",1);
        GUI.SetActive(false);
        PauseInt.SetActive(true);
    }
}

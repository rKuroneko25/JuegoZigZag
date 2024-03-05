using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgress : MonoBehaviour
{
    public GameObject oscuro;
    public GameObject Salir;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);   
    }

    void TaskOnClick()
    {
        oscuro.SetActive(false);
        Salir.SetActive(false);
        PlayerPrefs.SetFloat("Progress1",0);
        PlayerPrefs.SetFloat("Progress2",0);
        PlayerPrefs.SetFloat("Progress3",0);
        PlayerPrefs.SetFloat("Progress4",0);
        PlayerPrefs.SetFloat("Progress5",0);
        PlayerPrefs.SetInt("Attemps1",0);
        PlayerPrefs.SetInt("Attemps2",0);
        PlayerPrefs.SetInt("Attemps3",0);
        PlayerPrefs.SetInt("Attemps4",0);
        PlayerPrefs.SetInt("Attemps5",0);
        PlayerPrefs.SetInt("Clicks1",0);
        PlayerPrefs.SetInt("Clicks2",0);
        PlayerPrefs.SetInt("Clicks3",0);
        PlayerPrefs.SetInt("Clicks4",0);
        PlayerPrefs.SetInt("Clicks5",0);
        PlayerPrefs.SetInt("HighScore",0);
    }
}

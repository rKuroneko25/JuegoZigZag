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
    }
}

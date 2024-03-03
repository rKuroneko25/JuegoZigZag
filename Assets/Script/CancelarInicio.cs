using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelarInicio : MonoBehaviour
{
    public GameObject oscuro;
    public GameObject Salir;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);   
    }

    void TaskOnClick()
    {
        oscuro.SetActive(false);
        Salir.SetActive(false);
    }
}

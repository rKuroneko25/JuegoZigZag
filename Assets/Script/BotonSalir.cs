using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonSalir : MonoBehaviour
{
    public GameObject oscuro;
    public GameObject Salir;
    private Animator animator;
    void Start()
    {
        animator = Salir.GetComponent<Animator>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);   
    }

    void TaskOnClick()
    {
        oscuro.SetActive(true);
        Salir.SetActive(true);
        animator.SetTrigger("Click");
    }
}

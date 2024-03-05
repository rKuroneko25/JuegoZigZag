using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonInfo : MonoBehaviour
{
    public GameObject oscuro;
    public GameObject Salir;
    public Text texto;
    private int level;
    private Animator animator;
    void Start()
    {
        animator = Salir.GetComponent<Animator>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);   
    }

    void Update(){
        level = PlayerPrefs.GetInt("LevelSelected");
    }

    void TaskOnClick()
    {
        texto.text = "Attempts: "+PlayerPrefs.GetInt("Attemps"+level.ToString())+"\nClicks: "+PlayerPrefs.GetInt("Clicks"+level.ToString());
        oscuro.SetActive(true);
        Salir.SetActive(true);
        animator.SetTrigger("Click");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TIraPaLaIzq : MonoBehaviour
{
    private int level=1;
    private int Direccion;
    public GameObject interfaz;
    public GameObject interfazParent;
    private Animator animator;

    void Start()
    {
        animator = interfaz.GetComponent<Animator>();
        level = PlayerPrefs.GetInt("LevelSelected");
        Direccion = PlayerPrefs.GetInt("Direccion");
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void Update(){
        level = PlayerPrefs.GetInt("LevelSelected");
        Direccion = PlayerPrefs.GetInt("Direccion");
    }

    void TaskOnClick()
    {
        if (level > 1){
            if(Direccion == 0){
                interfazParent.transform.position = new Vector3(interfazParent.transform.position.x + 1100, interfazParent.transform.position.y, interfazParent.transform.position.z);
            }
            Direccion = 0;
            PlayerPrefs.SetInt("Direccion", Direccion);
            level--;
            PlayerPrefs.SetInt("LevelSelected", level);
            animator.SetTrigger("Izquierda");
        }
    }
}

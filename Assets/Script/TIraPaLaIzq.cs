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
    public Camera camara;
    public GameObject Barra1;
    public GameObject Barra2;
    public GameObject Barra3;
    public GameObject Barra4;
    public GameObject Barra5;

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
            PlayerPrefs.SetString("LevelSelectedString", level.ToString());
            animator.SetTrigger("Izquierda");
            StartCoroutine(CambiaColor());
        }
    }

    IEnumerator CambiaColor()
    {
        yield return new WaitForSeconds(0.25f);
        switch (level)
        {
            case 1:
                camara.backgroundColor = new Color(1, 0.5f, 0);
                Barra1.SetActive(true);
                Barra2.SetActive(false);
                break;
            case 2:
                camara.backgroundColor = new Color(1, 0, 0);
                Barra2.SetActive(true);
                Barra3.SetActive(false);
                break;
            case 3:
                camara.backgroundColor = new Color(1, 0, 1);
                Barra3.SetActive(true);
                Barra4.SetActive(false);
                break;
            case 4:
                camara.backgroundColor = new Color(0, 1, 0);
                Barra4.SetActive(true);
                Barra5.SetActive(false);
                break;
            case 5:
                camara.backgroundColor = new Color(1, 1, 0);
                break;
        }
    }
}

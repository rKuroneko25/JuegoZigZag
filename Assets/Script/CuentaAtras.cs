using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CuentaAtras : MonoBehaviour
{
    // PRIMERA FORMA
    public Button boton;
    public Image imagen;
    public Sprite[] numeros;

    void Start()
    {
        // SEGUNDA FORMA
        // boton = GameObject.FindAnyObjectByType<Button>();
        // imagen = GameObject.FindAnyObjectByType<Image>();

        // TERCERA FORMA
        // boton = GameObject.FindWithTag("Boton").GetComponent<Button>();
        // imagen = GameObject.FindWithTag("Imagen").GetComponent<Image>();

        boton.onClick.AddListener(Empezar);

    }

    void Empezar()
    {
        imagen.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);

        StartCoroutine(Contar());
         
        //SceneManager.LoadScene("Nivel1");
    }

    IEnumerator Contar()
    {
        for (int i = 0; i < numeros.Length; i++)
        {
            imagen.sprite = numeros[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }
}

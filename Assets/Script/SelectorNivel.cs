using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNivel : MonoBehaviour
{

    public GameObject fade;
    private Animator animator; 
    private int level;
    // Start is called before the first frame update
    void Start()
    {
        animator = fade.GetComponent<Animator>();
        animator.SetTrigger("FadeOutT");

        StartCoroutine(QuitaFade());

        PlayerPrefs.SetInt("LevelSelected", 1);
        PlayerPrefs.SetInt("Direccion", 0);
    }

    IEnumerator QuitaFade()
    {
        yield return new WaitForSeconds(0.5f);
        fade.SetActive(false);
    }
}

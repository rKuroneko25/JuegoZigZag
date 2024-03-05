using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SalirLevelSelector : MonoBehaviour
{
    public GameObject fade;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = fade.GetComponent<Animator>();
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Salir);
    }

    void Salir()
    {
        fade.SetActive(true);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        animator.SetTrigger("FadeInT");
        FindObjectOfType<AudioManager>().Stop("Level"+PlayerPrefs.GetString("LevelSelectedString"));
        FindObjectOfType<AudioManager>().Play("QuitLevel");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LevelSelector");
    }
}

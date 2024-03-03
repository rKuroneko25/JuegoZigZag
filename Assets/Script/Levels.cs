using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    public GameObject fade;
    private Animator animator;

    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        //detiene la musica del audiomanager
        AudioManager.instance.Play("Menu");
        animator = fade.GetComponent<Animator>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        fade.SetActive(true);
        animator.SetTrigger("FadeInT");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("LevelSelector");
    }
}

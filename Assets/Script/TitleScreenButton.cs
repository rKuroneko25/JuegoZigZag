using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenButton : MonoBehaviour
{
    public GameObject fade;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = fade.GetComponent<Animator>();
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        fade.SetActive(true);
        animator.SetTrigger("FadeInT");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        FindObjectOfType<AudioManager>().Stop("GameOver");
        FindObjectOfType<AudioManager>().Play("Menu");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("PantallaInicio");
    }
}

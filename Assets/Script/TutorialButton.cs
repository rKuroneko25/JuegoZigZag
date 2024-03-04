using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialButton : MonoBehaviour
{
    public GameObject fade;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = fade.GetComponent<Animator>();
        fade.SetActive(true);
        StartCoroutine(FadeOut());
        FindObjectOfType<AudioManager>().Play("Tutorial");
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    IEnumerator FadeOut()
    {
        animator.SetTrigger("FadeOutT");
        yield return new WaitForSeconds(0.5f);
        fade.SetActive(false);
    }

    void TaskOnClick()
    {
        fade.SetActive(true);
        animator.SetTrigger("FadeInT");
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        FindObjectOfType<AudioManager>().Stop("Tutorial");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }

}

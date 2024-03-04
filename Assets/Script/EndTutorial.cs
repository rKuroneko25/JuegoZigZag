using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndTutorial : MonoBehaviour
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
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        FindObjectOfType<AudioManager>().Stop("Tutorial");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level");
    }
}

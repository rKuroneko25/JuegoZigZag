using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTutorial : MonoBehaviour
{
    public GameObject Tut1;
    public GameObject Tut2;
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
        Tut1.SetActive(false);
        Tut2.SetActive(true);
    }

}

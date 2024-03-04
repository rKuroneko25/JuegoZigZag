using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
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
        FindObjectOfType<AudioManager>().Play("StartLevel");
        FindObjectOfType<AudioManager>().Stop("Menu");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        string level = gameObject.name;
        fade.SetActive(true);
        animator.SetTrigger("FadeInT");
        PlayerPrefs.SetInt("Fade", 1);
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetString("LevelSelectedString", level[5].ToString());
        PlayerPrefs.SetInt("ClicksNow",0);
        PlayerPrefs.SetInt("AttemptsNow",1);
        SceneManager.LoadScene("Tutorial"+level[5]);

    }
}

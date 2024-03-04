using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Arcade : MonoBehaviour
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
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        fade.SetActive(true);
        animator.SetTrigger("FadeInT");
        PlayerPrefs.SetInt("Fade", 1);
        FindObjectOfType<AudioManager>().Play("StartLevel");
        FindObjectOfType<AudioManager>().Stop("Menu");
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().Play("Arcade");
        PlayerPrefs.SetString("LevelSelected", "0");
        SceneManager.LoadScene("Arcade");
    }
}

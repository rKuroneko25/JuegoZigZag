using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour
{
    public GameObject GUI;
    public GameObject PauseInt;
    public GameObject fade;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = fade.GetComponent<Animator>();
        StartCoroutine(FadeOut());
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    IEnumerator FadeOut()
    {
        animator.SetTrigger("FadeOutT");
        yield return new WaitForSeconds(0.25f);
        fade.SetActive(false);
    }
    
    void TaskOnClick()
    {
        FindObjectOfType<AudioManager>().Pause("Level"+PlayerPrefs.GetString("LevelSelectedString"));
        PlayerPrefs.SetInt("Quieto",1);
        GUI.SetActive(false);
        PauseInt.SetActive(true);
    }
}

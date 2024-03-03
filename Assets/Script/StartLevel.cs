using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetString("LevelSelected", level[5].ToString());
        PlayerPrefs.SetInt("ClicksNow",0);
        PlayerPrefs.SetInt("AttemptsNow",1);
        SceneManager.LoadScene("Level");

    }
}

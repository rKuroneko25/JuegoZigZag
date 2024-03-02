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
        yield return new WaitForSeconds(1);
        string level = gameObject.name;
        PlayerPrefs.SetString("LevelSelected", level[5].ToString());
        SceneManager.LoadScene(level);
    }
}

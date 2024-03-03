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
        SceneManager.LoadScene(level);
        //realmente no haria falta tener una escena para cada nivel, tan solo una llamada "Level"
        //ya que el nivel en si es un archivo, no se que pilla "level = gameObject.name", por eso
        //no lo he cambiado, pero hay ponerlo bien
        //para el tutorial si que habria que crear 5 distintos
    }
}

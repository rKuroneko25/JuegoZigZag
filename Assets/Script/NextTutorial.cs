using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextTutorial : MonoBehaviour
{
    public GameObject Tut1;
    public GameObject Tut2;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Tut1.SetActive(false);
        Tut2.SetActive(true);
    }

}

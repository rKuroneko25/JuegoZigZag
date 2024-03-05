using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatasosFinal : MonoBehaviour
{
    public Text datasosText;

    void Start()
    {
        
    }

    void Update()
    {
        datasosText.text = "Attempts: " + PlayerPrefs.GetInt("AttemptsNow") + "\n\nClicks: " + PlayerPrefs.GetInt("ClicksNow");
    }
}

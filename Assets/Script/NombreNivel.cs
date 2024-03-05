using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NombreNivel : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        switch(PlayerPrefs.GetString("LevelSelectedString"))
        {
            case "1":
                text.text = "Chooser";
                break;
            case "2":
                text.text = "Speedical Dominator";
                break;
            case "3":
                text.text = "Reverse machine";
                break;
            case "4":
                text.text = "Flip after flip";
                break;
            case "5":
                text.text = "Jumpingeist";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

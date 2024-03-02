using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNivel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("LevelSelected", 1);
        PlayerPrefs.SetInt("Direccion", 0);
    }
}

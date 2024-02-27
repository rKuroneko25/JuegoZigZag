using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(autodelete());
    }

    IEnumerator autodelete()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

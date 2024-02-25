using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject Suelo;
    public float Velocidad = 5;

    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 Direccion;

    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        CrearSueloInical();
        Direccion = Vector3.forward;
    }

    void CrearSueloInical()
    {
        for (int i = 0; i < 3; i++)
        {
            ValZ += 6;
            Instantiate(Suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        }
    }

    void Update()
    {
        camara.transform.position = transform.position + offset;
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CambiarDireccion();
        }
        transform.Translate(Direccion * Velocidad * Time.deltaTime, Space.World);
        
    }

    private void OnCollisionExit(Collision other){
        if(other.gameObject.tag == "Suelo")
        {
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        if (aleatorio < 0.5f)
        {
            ValX += 6.0f;
        }
        else
        {
            ValZ += 6.0f;
        }
        Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        yield return new WaitForSeconds(3);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1);
        Destroy(suelo);
    }

    void CambiarDireccion()
    {
        if(Direccion == Vector3.forward){
            Direccion = Vector3.right;
        }
        else{
            Direccion = Vector3.forward;
        }
    }
}

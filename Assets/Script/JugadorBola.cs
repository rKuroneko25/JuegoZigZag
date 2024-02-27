using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject Suelo;
    public GameObject Flip;
    public float Velocidad = 5;

    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 Direccion;
    private bool flip;
    private bool stopSpawn;

    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        CrearSueloInical();
        Direccion = Vector3.forward;
        flip = false;
        stopSpawn = false;
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
        camara.transform.position = new Vector3(transform.position.x,0,transform.position.z) + offset;
        if(Input.GetKeyUp(KeyCode.Mouse0))
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
        if(other.gameObject.tag == "Flip")
        {
            StartCoroutine(BorrarFlip(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        float typePad = Random.Range(0.0f, 1.0f);
        float y=0;
        if (flip) {y=10;}

        //Posicion generada
        if (aleatorio < 0.5f)
        {
            ValX += 6.0f;
        }
        else
        {
            ValZ += 6.0f;
        }
        
        //Pad generator
        if (!stopSpawn) 
        {
            if (typePad <= 0.7f)
            {
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
            else
            {
                Instantiate(Flip, new Vector3(ValX, y, ValZ), Quaternion.identity);
                stopSpawn = true;
            }
        }
        
        yield return new WaitForSeconds(3);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1);
        Destroy(suelo);
    }

    IEnumerator BorrarFlip(GameObject suelo)
    {
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

    IEnumerator RotarCamara()
    {
        Vector3 angulo;
        if (flip)
        {
            angulo = new Vector3(-25f, 45f, 0f);
        } else {
            angulo = new Vector3(30f, 45f, 0f);
        }
        while (Quaternion.Angle(camara.transform.rotation, Quaternion.Euler(angulo)) > 0.01f)
        {
            camara.transform.rotation = Quaternion.RotateTowards(camara.transform.rotation, Quaternion.Euler(angulo), 30f * Time.deltaTime);
            yield return null;
        }
    }

    void InicioFlip()
    {
        stopSpawn = false;
        Physics.gravity *= -1;
        flip = !flip;
        
        StartCoroutine(RotarCamara());

        float y=0;
        if (flip) 
        {
            if(Direccion == Vector3.forward)
            {
                ValZ += 6;
            }
            else
            {
                ValX += 6;
            }
            y=10;
        } else {
            if(Direccion == Vector3.forward)
            {
                ValZ -= 6;
            }
            else
            {
                ValX -= 6;
            }
        }

        for (int i = 0; i < 4; i++) //forward suma z y right suma x
        {
            if(Direccion == Vector3.forward)
            {
                ValZ += 6;
                
            }
            else
            {
                ValX += 6;
            }
            Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Flip")
        {
            InicioFlip();
        }
    }
}

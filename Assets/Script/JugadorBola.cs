using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject Suelo;
    public GameObject Flip;
    public GameObject Speed;
    public float Velocidad = 15f;

    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ, HelpX, HelpZ;
    private Vector3 Direccion;
    private bool flip;
    private bool stopSpawn;
    private bool sonic;

    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        CrearSueloInical();
        Direccion = Vector3.forward;
        flip = false;
        sonic = false;
        stopSpawn = false;
        Velocidad = 15f;
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
        if(other.gameObject.tag == "Suelo" || other.gameObject.tag == "Flip" || other.gameObject.tag == "Speed")
        {
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Flip")
        {
            InicioFlip();
        }
        if(other.gameObject.tag == "Speed")
        {
            InicioSpeed();
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        float typePad = Random.Range(0.0f, 1.0f);
        float y=0;
        if (flip) {y=10;}

        if (aleatorio < 0.5f)
        {
            ValX += 6.0f;
        }
        else
        {
            ValZ += 6.0f;
        }
        
        if (!stopSpawn) 
        {
            if (typePad <= 0.7f)
            {
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
            else if (typePad <= 0.85f)
            {
                Instantiate(Speed, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
            else
            {
                Instantiate(Flip, new Vector3(ValX, y, ValZ), Quaternion.identity);
                HelpX = ValX;
                HelpZ = ValZ;
                stopSpawn = true;
            }
        }
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
        ValX = HelpX;
        ValZ = HelpZ;
        stopSpawn = false;
        Physics.gravity *= -1;
        flip = !flip;

        StartCoroutine(RotarCamara());

        int i=2;

        float y=0;
        if (flip) {y=10;}

        float plus=0;
        if (sonic) {plus=8; i=3;}

        if (Direccion == Vector3.forward) 
            {ValZ += 10+plus;}
        else 
            {ValX += 10+plus;}

        while(i>0) //forward suma z y right suma x
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
            i--;
        }
    }

    void InicioSpeed() 
    {
        sonic = !sonic;
        if (sonic)
            {Velocidad = 20f;}
        else
            {Velocidad = 15f;}
    }
}

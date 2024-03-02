using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;


public class JugadorBola : MonoBehaviour
{
    //PUBLICAS
    public Camera camara;
    public GameObject Suelo;
    public GameObject Jump;
    public GameObject Bifurcation;
    public GameObject Reverse;
    public GameObject Speed;
    public GameObject Flip;
    public GameObject Borde;
    public float Velocidad = 15f;

    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ, Val2X, Val2Z, HelpX, HelpZ;
    private Vector3 Direccion;
    private bool Saltar = false;
    private bool Saltando = false;
    private int Bifurcacion = 0;
    private bool flip;
    private bool stopSpawn;
    private int orientacion = 1;
    private int izqOrDer = 1;
    private bool reverso = false;
    private bool sonic;
    private GameObject JUMPAD;
    private bool Forward = true;
    private bool speedDelay;
    private string Nivel; //Nivel 0 = Arcade
    private string[] padsNivel;
    private int padActual = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Nivel = PlayerPrefs.GetString("Nivel");
        Nivel = "1";
        offset = camara.transform.position;
        Direccion = Vector3.forward;
        flip = false;
        sonic = false;
        stopSpawn = false;
        Velocidad = 15f;
        speedDelay = false;
        if (Nivel == "0") //Arcade
            CrearSueloInical();
        else
            CargaNivel();
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
        if(transform.position.y < 0.50  || transform.position.y > 9.50)
        {
            if (flip) {Physics.gravity *= -1;}
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        camara.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset;

        if(!Saltando){
            transform.Translate(Direccion * Velocidad * Time.deltaTime, Space.World);
        
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                if(!Saltar){
                    CambiarDireccion();
                    StartCoroutine(girar());     
                }
                else{
                    Saltando = true;
                    StartCoroutine(jumpPad(JUMPAD)); 
                }
            } 
        }
      
    }

    IEnumerator girar()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        transform.Translate(Direccion * Velocidad * Time.deltaTime, Space.World);
    }

    private void OnCollisionExit(Collision other){
        if (Nivel == "0") {
            if(other.gameObject.tag == "Suelo" || 
               other.gameObject.tag == "Reverse" || 
               other.gameObject.tag == "Speed" ||
               other.gameObject.tag == "Flip")
            {
                StartCoroutine(BorrarSuelo(other.gameObject));
            }
            if(other.gameObject.tag == "Bifurcation")
            {
                if(Direccion != Vector3.forward){
                    ValX = Val2X;
                    ValZ = Val2Z;
                }
                StartCoroutine(BorrarSuelo(other.gameObject));
            }
        } else {
            if(other.gameObject.tag == "Bifurcation")
                if(Direccion != Vector3.forward){
                    ValX = Val2X;
                    ValZ = Val2Z;
                }
            StartCoroutine(GeneraPad(other.gameObject));
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Jump")
        {
            Saltar = true;
            JUMPAD = other.gameObject;
        }

        if(other.gameObject.tag == "Flip"){
            InicioFlip();
        }

        if(other.gameObject.tag == "Reverse"){
            izqOrDer *= -1;
        }

        if(other.gameObject.tag == "Speed"){
            InicioSpeed();
        }

        if(other.gameObject.tag == "Borde")
        {
            if (flip) {Physics.gravity *= -1;}
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator jumpPad(GameObject suelo)
    {
        int z = 1;
        if(flip){z = -1;}

        GetComponent<Rigidbody>().AddForce(Direccion * 500);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 300 * z);
        yield return new WaitForSeconds(0.85f);
        GetComponent<Rigidbody>().AddForce(Vector3.down * 150 * z);
        GetComponent<Rigidbody>().AddForce(Direccion * -200);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Saltar = false;
        Saltando = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(suelo);
    }

    IEnumerator BorrarSuelo(GameObject coso)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        float typePad = Random.Range(0.0f, 1.0f);  
        GameObject pad = Suelo;
        float y = 0;
        if(flip){y = 10;}

        if(reverso){
            if(!Forward)
                {
                    Instantiate(Borde, new Vector3(ValX+(3*orientacion*(-1)), y, ValZ), Quaternion.identity);
                }
            ValZ += 6.0f;
        }
        else if(Bifurcacion == 0){
            if (aleatorio < 0.5f) {
                if(Forward)
                {
                    Instantiate(Borde, new Vector3(ValX, y, ValZ+3), Quaternion.Euler(0, 90, 0));
                }
                ValX += 6.0f * orientacion;
                Forward = false;
                }
            else {
                if(!Forward)
                {
                    Instantiate(Borde, new Vector3(ValX+(3*orientacion), y, ValZ), Quaternion.identity);
                }
                ValZ += 6.0f;
                Forward = true;
            } 
        }

        if(!stopSpawn){
            if(reverso){
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                reverso = false;
            }
            else if(Bifurcacion > 0){
                Val2X += 6.0f * orientacion;
                ValZ += 6.0f;
                Instantiate(Suelo, new Vector3(Val2X, y, Val2Z), Quaternion.identity);
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                Bifurcacion -= 1;
            }
            else if(typePad <= 0.9){
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
            else if(typePad > 0.9 && typePad <= 0.92){
                Instantiate(Jump, new Vector3(ValX, y, ValZ), Quaternion.identity);
                if(aleatorio < 0.5f) {ValX += 12.0f * orientacion;}
                else {ValZ += 12.0f;}
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
            else if(typePad > 0.92 && typePad <= 0.94){
                Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.identity);
                Bifurcacion = 3;
                Val2X = ValX;
                Val2Z = ValZ;
            }
            else if(typePad > 0.94 && typePad <= 0.96){
                Instantiate(Flip, new Vector3(ValX, y, ValZ), Quaternion.identity);
                HelpX = ValX;
                HelpZ = ValZ;
                stopSpawn = true;
            }
            else if(typePad > 0.96 && typePad <= 0.98){
                Instantiate(Reverse, new Vector3(ValX, y, ValZ), Quaternion.identity);
                orientacion *= -1;
                reverso = true;
            }
            else if(typePad > 0.98 && typePad <= 1.0){
                if (speedDelay)
                    Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                else
                    Instantiate(Speed, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
        }
            
        yield return new WaitForSeconds(3);
        coso.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        coso.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1);
        Destroy(coso);
    }

    void CambiarDireccion()
    {
        if(Direccion == Vector3.forward){
            Direccion = Vector3.right * izqOrDer;
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
    /*
    IEnumerator RotarAmarac() //No es 100% funcional
    {
        Vector3 angulo;
        if (izqOrDer == -1)
            angulo = new Vector3(30f, -45f, 0f);
        else
            angulo = new Vector3(30f, 45f, 0f);

        while (Quaternion.Angle(camara.transform.rotation, Quaternion.Euler(angulo)) > 0.01f)
        {
            camara.transform.rotation = Quaternion.RotateTowards(camara.transform.rotation, Quaternion.Euler(angulo), 100f * Time.deltaTime);
            yield return null;
        }
    }*/

    void InicioFlip(){
        if (Nivel == "0")
        {
            ValX = HelpX;
            ValZ = HelpZ;
        }
        stopSpawn = false;
        Physics.gravity *= -1;
        flip = !flip;

        StartCoroutine(RotarCamara());

        float y=0;
        if (flip) {y=10;}

        float plus=0;
        if (sonic) {plus=8;}
        if (sonic) {plus=8;}

        if (Direccion == Vector3.forward) 
            {ValZ += 10+plus;}
        else 
            {ValX += (10+plus)*orientacion;}

        for(int i=0 ; i<3 ; i++) //forward suma z y right suma x
        {
            if(Direccion == Vector3.forward)
            {
                ValZ += 6;
            }
            else
            {
                ValX += 6 * orientacion;
            }
            Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
        }
    }

    void InicioSpeed() 
    {
        sonic = !sonic;
        speedDelay = true;
        StartCoroutine(SpeedDelay());
        if (sonic)
            {Velocidad = 20f;}
        else
            {Velocidad = 15f;}
    }

    IEnumerator SpeedDelay()
    {
        yield return new WaitForSeconds(5);
        speedDelay = false;
    }

    public void CargaNivel()
    {
        string fichero = Application.dataPath + "/Niveles/nivel" + Nivel + ".txt";

        string[] lineas = File.ReadAllLines(fichero);

        foreach (string linea in lineas)
            padsNivel = linea.Split(',');

        CrearSueloInical();
    }

    IEnumerator GeneraPad(GameObject coso)
    {
        if (!stopSpawn) {

            float y=0;
            if (flip) {y=10;}

            if(Bifurcacion > 0){
                Val2X += 6.0f * orientacion;
                ValZ += 6.0f;
                Instantiate(Suelo, new Vector3(Val2X, y, Val2Z), Quaternion.identity);
                Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                Bifurcacion -= 1;
            }
            else {
                string pad = padsNivel[padActual];
                padActual += 1;

                if(reverso){
                    if(!Forward)
                    {
                        Instantiate(Borde, new Vector3(ValX+(3*orientacion*(-1)), y, ValZ), Quaternion.identity);
                    }
                    ValZ += 6.0f;
                } else {
                    if (pad[0] == 'R') {
                        if(Forward)
                        {
                            Instantiate(Borde, new Vector3(ValX, y, ValZ+3), Quaternion.Euler(0, 90, 0));
                        }
                        ValX += 6.0f * orientacion;
                        Forward = false;
                        }
                    else {
                        if(!Forward)
                        {
                            Instantiate(Borde, new Vector3(ValX+(3*orientacion), y, ValZ), Quaternion.identity);
                        }
                        ValZ += 6.0f;
                        Forward = true;
                    } 
                }

                switch(pad[1]){
                    case 'N':
                        Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        break;

                    case 'S':
                        Instantiate(Speed, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        break;

                    case 'B':
                        Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        Bifurcacion = 3;
                        Val2X = ValX;
                        Val2Z = ValZ;
                        break;

                    case 'F':
                        Instantiate(Flip, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        stopSpawn = true;
                        break;

                    case 'J':
                        Instantiate(Jump, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        if(pad[0] == 'F') 
                            {ValZ += 12;}
                        else 
                            {ValX += 12*orientacion;}
                        Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        break;

                    case 'R':
                        Instantiate(Reverse, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        orientacion *= -1;
                        reverso = !reverso;
                        break;
                }
            }
        }

        yield return new WaitForSeconds(3);
        coso.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        coso.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(1);
        Destroy(coso);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public float Velocidad = 15f;

    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ, Val2X, Val2Z, HelpX, HelpZ;
    private Vector3 Direccion;
    private bool Saltar = false;
    private int Bifurcacion = 0;
    private bool flip;
    private bool stopSpawn;
    private int orientacion = 1;
    private int izqOrDer = 1;
    private bool reverso = false;
    private bool sonic;
    private bool speedDelay;
    private bool reverseDelay;

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
        speedDelay = false;
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
        if(transform.position.y < 0.50 || transform.position.y > 9.5)
        {
            if (flip)   Physics.gravity *= -1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        camara.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset;

        if(!Saltar){
            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                CambiarDireccion();
                StartCoroutine(girar());     
            }
            else{
                transform.Translate(Direccion * Velocidad * Time.deltaTime, Space.World);
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Jump")
        {
            Saltar = true;
            StartCoroutine(jumpPad(other.gameObject));
        }

        if(other.gameObject.tag == "Flip"){
            InicioFlip();
        }

        if(other.gameObject.tag == "Reverse"){
            InicioReverse();
        }

        if(other.gameObject.tag == "Speed"){
            InicioSpeed();
        }
    }

    IEnumerator jumpPad(GameObject suelo)
    {
        int z = 1;
        if(flip){z = -1;}

        GetComponent<Rigidbody>().AddForce(Direccion * 600);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 350 * z);
        yield return new WaitForSeconds(0.85f);
        GetComponent<Rigidbody>().AddForce(Vector3.down * 150 * z);
        GetComponent<Rigidbody>().AddForce(Direccion * -200);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;      
        Saltar = false;
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

        // Elegir direccion
        if(reverso){
            ValZ += 6.0f;
            reverso = false;
        }
        else if(Bifurcacion == 0){
            if (aleatorio < 0.5f) {ValX += 6.0f * orientacion;}
            else {ValZ += 6.0f;} 
        }

        // Crear Pad
        if(!stopSpawn){
            if(Bifurcacion > 0){
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
                if (reverseDelay)
                    Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                else {
                    Instantiate(Flip, new Vector3(ValX, y, ValZ), Quaternion.identity);
                    HelpX = ValX;
                    HelpZ = ValZ;
                    HelpX = ValX;
                    HelpZ = ValZ;
                    stopSpawn = true;
                }
            }
            else if(typePad > 0.96 && typePad <= 0.98){
                if (reverseDelay)
                    Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                else {
                    Instantiate(Reverse, new Vector3(ValX, y, ValZ), Quaternion.identity);
                    orientacion *= -1;
                    reverso = true;
                }
            }
            else if(typePad > 0.98 && typePad <= 1.0){
                if (speedDelay)
                    Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                else
                    Instantiate(Speed, new Vector3(ValX, y, ValZ), Quaternion.identity);
            }
        }
            
        // Destruir suelo
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
        ValX = HelpX;
        ValZ = HelpZ;
        stopSpawn = false;
        Physics.gravity *= -1;
        flip = !flip;

        StartCoroutine(RotarCamara());

        float y=0;
        if (flip) {y=10;}

        float plus=0;
        if (sonic) {plus=8;}

        if (Direccion == Vector3.forward) 
            {ValZ += 10+plus;}
        else 
            {ValX += (10+plus)*orientacion;}

        for (int i=0; i<3; i++) //forward suma z y right suma x
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

    void InicioReverse()
    {
        reverseDelay = true;
        izqOrDer *= -1;

        StartCoroutine(ReverseDelay());
        /*
        offset = new Vector3(offset.x*-1, offset.y, offset.z);
        
        StartCoroutine(RotarAmarac());
        */        
    }

    IEnumerator SpeedDelay()
    {
        yield return new WaitForSeconds(5);
        speedDelay = false;
    }

    IEnumerator ReverseDelay()
    {
        yield return new WaitForSeconds(2);
        reverseDelay = false;
    }
}

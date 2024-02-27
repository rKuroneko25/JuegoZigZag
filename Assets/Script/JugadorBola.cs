using System.Collections;
using System.Collections.Generic;
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
    public float Velocidad = 5;

    //PRIVADAS
    private Vector3 offset;
    private float ValX, ValZ, Val2X, Val2Z;
    private Vector3 Direccion;
    private bool Saltar = false;
    private int Bifurcacion = 0;
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
        if(transform.position.y < 0.72)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        camara.transform.position = transform.position + offset;
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
        if(other.gameObject.tag == "Suelo")
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
    }

    IEnumerator jumpPad(GameObject suelo)
    {
        //impulsa al jugador hacia delante en un angulo de 45 grados
        GetComponent<Rigidbody>().AddForce(Direccion * 600);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 350);
        yield return new WaitForSeconds(0.85f);
        GetComponent<Rigidbody>().AddForce(Vector3.down * 150);
        GetComponent<Rigidbody>().AddForce(Direccion * -300);
        yield return new WaitForSeconds(0.5f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);        
        Saltar = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(suelo);
    }

    IEnumerator BorrarSuelo(GameObject coso)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        float typePad = Random.Range(0.0f, 1.0f);  
        GameObject pad = Suelo;

        // Elegir direccion
        if(Bifurcacion == 0){
            if (aleatorio < 0.5f) {ValX += 6.0f;}
            else {ValZ += 6.0f;} 
        }

        // Crear Pad
        if(Bifurcacion > 0){
            Val2X += 6.0f;
            ValZ += 6.0f;
            Instantiate(Suelo, new Vector3(Val2X, 0, Val2Z), Quaternion.identity);
            Instantiate(Suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            Bifurcacion -= 1;
        }
        else if(typePad <= 0.9){
            Instantiate(Suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        }
        else if(typePad > 0.9 && typePad <= 0.92){
            Instantiate(Jump, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            if(aleatorio < 0.5f) {ValX += 12.0f;}
            else {ValZ += 12.0f;}
            Instantiate(Suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        }
        else if(typePad > 0.92 && typePad <= 0.94){
            Instantiate(Bifurcation, new Vector3(ValX, 0, ValZ), Quaternion.identity);
            Bifurcacion = 3;
            Val2X = ValX;
            Val2Z = ValZ;
        }
        else if(typePad > 0.94 && typePad <= 0.96){
            //Flip
        }
        else if(typePad > 0.96 && typePad <= 0.98){
            //Reverse
        }
        else if(typePad > 0.98 && typePad <= 1.0){
            //Speed
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

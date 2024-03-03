using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.UI;


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
    public GameObject Guia;
    public float Velocidad = 15f;
    public Text timerText;
    public Text scoreText;

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
    private bool DirBifurc; 
    private int Clicks;
    private int Attempts;
    private int score;
    private float timer;
    private bool quietomanin;

    // Start is called before the first frame update
    void Start()
    {
        Nivel = PlayerPrefs.GetString("LevelSelected");
        Clicks = PlayerPrefs.GetInt("ClicksNow");
        Attempts = PlayerPrefs.GetInt("AttemptsNow");
        offset = camara.transform.position;
        Direccion = Vector3.forward;
        flip = false;
        sonic = false;
        stopSpawn = false;
        Velocidad = 15f;
        speedDelay = false;
        Bifurcacion = 0;
        quietomanin = false;
        if (Nivel == "0") { //Arcade
            CrearSueloInical();
            score = 0;
            timer = 0;
        }
        else{
            FindObjectOfType<AudioManager>().Play("Level" + Nivel);
            CargaNivel();
        }
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
        if (Nivel == "0") {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
            scoreText.text = "Score: " + score;
        }
        if(transform.position.y < 0.50  || transform.position.y > 9.50)
        {
            Muerte();
        }

        camara.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset;
        if (!quietomanin) {
            if(!Saltando){
                transform.Translate(Direccion * Velocidad * Time.deltaTime, Space.World);
            
                if(Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Clicks += 1;
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
               other.gameObject.tag == "Flip" ||
               other.gameObject.tag == "Bifurcation")
            {
                StartCoroutine(BorrarSuelo(other.gameObject));
            }
            // if(other.gameObject.tag == "Bifurcation")
            // {
            //     if(Direccion != Vector3.forward){
            //         ValX = Val2X;
            //         ValZ = Val2Z;
            //     }
            //     StartCoroutine(BorrarSuelo(other.gameObject));
            // }
        } else {
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
            Muerte();
        }
        if(other.gameObject.tag == "Guia")
        {
            Meta(other.gameObject);
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
        float BifurcationRandom = Random.Range(0.0f, 1.0f);
        float typePad = Random.Range(0.0f, 1.0f);  
        GameObject pad = Suelo;
        float y = 0;
        if(flip){y = 10;}

        Puntos(coso);

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
                if (Bifurcacion == 0)
                {
                    if(!DirBifurc)
                    {
                        ValX = Val2X;
                        ValZ = Val2Z;
                    }
                }
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
                if(BifurcationRandom < 0.5f){ // Forward
                    Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.Euler(0, 270, 0));
                    DirBifurc = true;
                }
                else{ // Side
                    if(orientacion == 1){
                        Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.Euler(0, 0, 0));
                    }
                    else{
                        Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.Euler(0, 180, 0));
                    }
                    DirBifurc = false;
                }
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
                    speedDelay = true;
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

        string megalinea = string.Join("",lineas);

        padsNivel = megalinea.Split(',');

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
                if (Bifurcacion == 0)
                {
                    if(!DirBifurc)
                    {
                        ValX = Val2X;
                        ValZ = Val2Z;
                    }else{ //sin esto no va xD
                        Val2X = ValX;
                        Val2Z = ValZ;
                    }
                }
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
                    reverso = false;
                } else {
                    if (pad[0] == 'S') {
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
                    case 'G':
                        Instantiate(Guia, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        break;

                    case 'N':
                        Instantiate(Suelo, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        break;

                    case 'S':
                        Instantiate(Speed, new Vector3(ValX, y, ValZ), Quaternion.identity);
                        break;

                    case 'B':
                        if(pad[2] == 'F'){ // Forward
                            Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.Euler(0, 270, 0));
                            DirBifurc = true;
                        }
                        else{ // Side
                            if(orientacion == 1)
                            {
                                Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.Euler(0, 0, 0));
                            }
                            else
                            {
                                Instantiate(Bifurcation, new Vector3(ValX, y, ValZ), Quaternion.Euler(0, 180, 0));
                            }
                            DirBifurc = false;
                        }
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
                        reverso = true;
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

    void Muerte()
    {
        PlayerPrefs.SetInt("ClicksNow", Clicks);
        PlayerPrefs.SetInt("AttemptsNow", Attempts+1);
        GameObject.Find("Jugador").SetActive(false);
        FindObjectOfType<AudioManager>().Play("Death");
        FindObjectOfType<AudioManager>().Stop("Level"+Nivel);
        if (flip) {Physics.gravity *= -1;}
        Invoke("SceneLoad", 1f);
    }

    void SceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    void Meta(GameObject coso)
    {
        quietomanin = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }

        Vector3 centro = coso.GetComponent<Renderer>().bounds.center;
        StartCoroutine(DesplazarHaciaCentro(centro));
        
        //Guardar la informacion (para la "i" de Level Selector)
        switch (Nivel)
        {
            case "1":
                PlayerPrefs.SetInt("Clicks1", Clicks + PlayerPrefs.GetInt("Clicks1"));
                PlayerPrefs.SetInt("Attemps1", Attempts + PlayerPrefs.GetInt("Attemps1"));
                break;

            case "2":
                PlayerPrefs.SetInt("Clicks2", Clicks + PlayerPrefs.GetInt("Clicks2"));
                PlayerPrefs.SetInt("Attemps2", Attempts + PlayerPrefs.GetInt("Attemps2"));
                break;

            case "3":
                PlayerPrefs.SetInt("Clicks3", Clicks + PlayerPrefs.GetInt("Clicks3"));
                PlayerPrefs.SetInt("Attemps3", Attempts + PlayerPrefs.GetInt("Attemps3"));
                break;

            case "4":
                PlayerPrefs.SetInt("Clicks4", Clicks + PlayerPrefs.GetInt("Clicks4"));
                PlayerPrefs.SetInt("Attemps4", Attempts + PlayerPrefs.GetInt("Attemps4"));
                break;

            case "5":
                PlayerPrefs.SetInt("Clicks5", Clicks + PlayerPrefs.GetInt("Clicks5"));
                PlayerPrefs.SetInt("Attemps5", Attempts + PlayerPrefs.GetInt("Attemps5"));
                break;

            default:
                break;
        }
        //Parar la bola en el centro de la meta
        //Poppear LevelComplete y la musica Win
        //Poppear resultados -> Level Complete / Clicks: / Attempts: 
    }

    IEnumerator DesplazarHaciaCentro(Vector3 centro)
    {
        while (Vector3.Distance(transform.position, centro) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, centro, Time.deltaTime * 2);
            yield return null;
        }

        transform.position = centro;
    }

    void Puntos(GameObject coso)
    {
        int fm=0, rm=0, sm=0;
        if (flip) {fm = 1;}
        if (orientacion == -1) {rm = 1;}
        if (sonic) {sm = 1;}

        if (coso.tag == "Suelo") {score += 1 * (System.Math.Max(fm*3,1)) * (System.Math.Max(rm*3,1)) * (System.Math.Max(sm*2,1));}
        if (coso.tag == "Bifurcation") {score += 10 * (System.Math.Max(fm*3,1)) * (System.Math.Max(rm*3,1)) * (System.Math.Max(sm*2,1));}
        if (coso.tag == "Jump") {score += 30 * (System.Math.Max(fm*3,1)) * (System.Math.Max(rm*3,1)) * (System.Math.Max(sm*2,1));}
    }
    
}
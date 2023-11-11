using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorDeCarreteras : MonoBehaviour
{
    public GameObject contenedorCallesGo;
    public GameObject[] contenedorCallesArray;

    public float velocidad;
    public bool inicioJuego;
    public bool juegoFinalizado;

    int contadorCalles = 0;
    int numeroSelectorDeCalles;

    public GameObject calleNueva;
    public GameObject calleAnterior;

    public float tamañoCalle;

    public Vector3 medidaLimitePantalla;
    public bool salioDePantalla;
    public GameObject mCamGo;
    public Camera mCamComp;

    public GameObject cocheGO;
    public GameObject audioFXGO;
    public AudioFX audioFXScript;
    public GameObject bgFinalGO;

    void Start()
    {
        InicioJuego();
    }
    void InicioJuego()
    {
        contenedorCallesGo = GameObject.Find("ContenedorCalles");
        mCamGo = GameObject.Find("MainCamera");
        mCamComp = mCamGo.GetComponent<Camera>();

        bgFinalGO = GameObject.Find("PanelGameOver");
        bgFinalGO.SetActive(false);

        audioFXGO = GameObject.Find("AudioFx");
        audioFXScript = audioFXGO.GetComponent<AudioFX>();

        cocheGO = GameObject.FindObjectOfType<Coche>().gameObject;

        VelocidadMotorCarretera();
        MedirPantalla();
        BuscadorDeCalles();
    }

    public void JuegoTerminadoEstados()
    {
        cocheGO.GetComponent<AudioSource>().Stop();
        audioFXScript.FXMusic();
        bgFinalGO.SetActive(true);
    }

    void VelocidadMotorCarretera()
    {
        velocidad = 18;
    }
    void BuscadorDeCalles()
    {
        contenedorCallesArray = GameObject.FindGameObjectsWithTag("Calle");
        for (int i = 0; i < contenedorCallesArray.Length; i++)
        {
            contenedorCallesArray[i].gameObject.transform.parent = contenedorCallesGo.transform;
            contenedorCallesArray[i].gameObject.SetActive(false);
            contenedorCallesArray[i].gameObject.name = "CalleOFF_" + i;
        }
        CrearCalles();
    }
    void CrearCalles()
    {
        contadorCalles++;
        numeroSelectorDeCalles = Random.Range(0, contenedorCallesArray.Length);
        GameObject Calle = Instantiate(contenedorCallesArray[numeroSelectorDeCalles]);
        Calle.SetActive(true);
        Calle.name = "Calle" + contadorCalles;
        Calle.transform.parent = gameObject.transform;
        PosicionarCalles();
    }
    void PosicionarCalles()
    {
        //Llena la variable calle anterior con la Calla que lleva el contador -1 para que sea la calle anterior a la nueva
        calleAnterior = GameObject.Find("Calle" + (contadorCalles - 1));
        calleNueva = GameObject.Find("Calle" + contadorCalles);

        //Posicionarlas

        //1 primero identifico cuanto mide cada calle 
        MidoCalle();
        calleNueva.transform.position = new Vector3(calleAnterior.transform.position.x,calleAnterior.transform.position.y-1 + tamañoCalle, 0);
        salioDePantalla = false;
    }
    void MidoCalle()
    {
        for (int i = 0; i < calleAnterior.transform.childCount; i++)
        {
            if (calleAnterior.transform.GetChild(i).gameObject.GetComponent<Pieza>() != null)
            {
                float tamañoPieza = calleAnterior.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
                tamañoCalle = tamañoCalle + tamañoPieza;
            }

        }
    }
    void MedirPantalla()
    {
        medidaLimitePantalla = new Vector3(0, mCamComp.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f, 0);
    }
    void Update()
    {
        if (inicioJuego == true && juegoFinalizado == false)
        {
            transform.Translate(Vector3.down * velocidad * Time.deltaTime);
            if (calleAnterior.transform.position.y + tamañoCalle < medidaLimitePantalla.y && salioDePantalla == false)
            {
                salioDePantalla = true;
                DestruyoCalles();
            }
        }
    }
    void DestruyoCalles()
    {
        Destroy(calleAnterior);
        tamañoCalle = 0;
        calleAnterior = null;
        CrearCalles();
    }
}

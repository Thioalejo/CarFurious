using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocheOstaculo : MonoBehaviour
{
    public GameObject cronometroGo;
    public Cronometro cronometroScript;

    public GameObject audioFXGo;
    public AudioFX audioFXScript;

    private void Start()
    {
        cronometroGo = GameObject.FindObjectOfType<Cronometro>().gameObject;
        cronometroScript = cronometroGo.GetComponent<Cronometro>();

        audioFXGo = GameObject.FindObjectOfType<AudioFX>().gameObject;
        audioFXScript = audioFXGo.GetComponent<AudioFX>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Coche>()!=null)
        {
            audioFXScript.FXSonidoChoque();
            cronometroScript.tiempo = cronometroScript.tiempo - 20;
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    public GameObject motorDeCarreterasGo;
    public MotorDeCarreteras motorDeCarreterasScript;

    public float tiempo;
    public float distancia;
    public Text txtTiempo;
    public Text txtDistancia;
    public Text txtDistanciaFinal;
    // Start is called before the first frame update
    void Start()
    {
        motorDeCarreterasGo = GameObject.Find("MotorDeCarreteras");
        motorDeCarreterasScript = motorDeCarreterasGo.GetComponent<MotorDeCarreteras>();

        txtTiempo.text = "0:10";
        txtDistancia.text = "0";

        tiempo = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(motorDeCarreterasScript.inicioJuego == true && motorDeCarreterasScript.juegoFinalizado == false)
        {
            CalculoTiempoDistancia();
        }

        if(tiempo <= 0 && motorDeCarreterasScript.juegoFinalizado == false)
        {
            motorDeCarreterasScript.juegoFinalizado = true;
            motorDeCarreterasScript.JuegoTerminadoEstados();
            txtDistanciaFinal.text = ((int)distancia).ToString()+ " mts";
        }
    }

    void CalculoTiempoDistancia()
    {
        distancia += Time.deltaTime * motorDeCarreterasScript.velocidad;

        txtDistancia.text = ((int)distancia).ToString();

        tiempo -= Time.deltaTime;
        int minutos = (int)tiempo / 60;
        int segundos = (int)tiempo % 60;
        txtTiempo.text = minutos.ToString() + ":" + segundos.ToString().PadLeft(2, '0');
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bola : MonoBehaviour
{
    public float velocidad = 30.0f;


    //Audio Source
    AudioSource fuenteDeAudio;
    //Clips de audio
    public AudioClip Gol, Raqueta, Rebote, fin;
    //Contadores de goles
    public int golesIzquierda = 0;
    public int golesDerecha = 0;
    //Cajas de texto de los contadores
    public Text contadorIzquierda;
    public Text contadorDerecha;
    public Text resultado;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;

        fuenteDeAudio = GetComponent<AudioSource>();
        //Pongo los contadores a 0
        contadorIzquierda.text = golesIzquierda.ToString();
        contadorDerecha.text = golesDerecha.ToString();
        resultado.enabled = false;
        Time.timeScale = 1;
    }
    void OnCollisionEnter2D(Collision2D micolision)
    {
        
        //Si choca con la raqueta izquierda
        if (micolision.gameObject.name == "RaquetaIzquierda")
        {
            //Valor de x
            int x = 1;
            //Valor de y
            int y = direccionY(transform.position,
            micolision.transform.position);
            //Calculo dirección
            Vector2 direccion = new Vector2(x, y);
            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

            fuenteDeAudio.clip = Raqueta;
            fuenteDeAudio.Play();
        }
        //Si choca con la raqueta derecha
        if (micolision.gameObject.name == "RaquetaDerecha")
        {
            //Valor de x
            int x = -1;
            //Valor de y
            int y = direccionY(transform.position,
            micolision.transform.position);
            //Calculo dirección (normalizada para que de 1 o -1)
            Vector2 direccion = new Vector2(x, y);
            //Aplico velocidad
            GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
            fuenteDeAudio.clip = Raqueta;
            fuenteDeAudio.Play();
        }
        //Para el sonido del rebote
        if (micolision.gameObject.name == "Arriba" ||
        micolision.gameObject.name == "Abajo")
        {
            //Reproduzco el sonido del rebote
            fuenteDeAudio.clip = Rebote;
            fuenteDeAudio.Play();
        }
    }
    //Direccion Y
    int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
    {
        if (posicionBola.y > posicionRaqueta.y)
        {
            return 1;
        }
        else if (posicionBola.y < posicionRaqueta.y)
        {
            return -1;
        }
        else
        {
            return 0;
        }

    }
    
    //Reinicio la posición de la bola
    public void reiniciarBola(string direccion)
    {
        //Posición 0 de la bola
        transform.position = Vector2.zero;
        //Vector2.zero es lo mismo que new Vector2(0,0);
        //Velocidad inicial de la bola
        
        //Velocidad y dirección
        
            if (direccion == "Derecha")
            {
                //Incremento goles al de la derecha
                golesDerecha++;

                //Lo escribo en el marcador
                contadorDerecha.text = golesDerecha.ToString();
            velocidad = velocidad + 5f;
            if (!comprobarFinal())
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
                     

            }
           

            
        }
            else if (direccion == "Izquierda")
            {
                //Incremento goles al de la izquierda
                golesIzquierda++;

                //Lo escribo en el marcador
                contadorIzquierda.text = golesIzquierda.ToString();
                velocidad = velocidad + 5f;
                if (!comprobarFinal())
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
                    


            } 
                
            }
        
        //Reproduzco el sonido del gol
        fuenteDeAudio.clip = Gol;
        fuenteDeAudio.Play();
    }
        
   
    bool comprobarFinal()
    {
        if (golesIzquierda == 5)
        {
            
            resultado.text = "EL Jugador de la Izquierda Gana!\n Pulsa P para volver a jugar \n Pulsa T para salir";
            
            resultado.enabled = true;
            Time.timeScale = 0;
            return true;
        }
        else if (golesDerecha == 5)
        {
            
            resultado.text = "El Jugador de la Derecha Gana!\n Pulsa P para volver a jugar \n Pulsa T para salir";
            
            resultado.enabled = true;
            Time.timeScale = 0;
            
            return true;
            
        }
        else
        {
            return false;
        }
        
    }

}

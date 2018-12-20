using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarameloScript : MonoBehaviour {

    int puntuacion = 5;
    Text txtCaramelo;
    public static int numeroCaramelos;

    private void Start() {
        txtCaramelo = GameObject.Find("TextoCaramelo").GetComponent<Text>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 

        if (collision.gameObject.gameObject.name == "PlayerCalabaza")
        {
          
            // INCREMENTAMOS LOS PUNTOS AL COGER EL CARAMELO 5 PUNTOS
            collision.gameObject.GetComponent<PlayerScript>().IncrementarPuntuacion(puntuacion);
            
            numeroCaramelos = int.Parse(txtCaramelo.text);
           // print("colisionantes antes de sumar " + numeroCaramelos);

            numeroCaramelos = numeroCaramelos + 1;
            // LO PONEMOS EN EL TEXTO PARA QUE SE MUESTRE
            txtCaramelo.text = "" + numeroCaramelos ;

            // VAMOS HACER QUE SALGAN PARTICULAS DE CARAMELOS
            gameObject.GetComponent<ParticleSystem>().Play();

            // DESTUIR EL CARAMELO
            Destroy(gameObject,1);            
        }
    }
    
}

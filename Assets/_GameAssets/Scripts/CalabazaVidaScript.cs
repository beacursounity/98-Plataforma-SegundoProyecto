using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalabazaVidaScript : MonoBehaviour {
    int salud = 40;
   

    private void OnTriggerEnter2D(Collider2D collision) {
 
        if ( collision.gameObject.name == "PlayerCalabaza") {
            // LLAMAMOS A RECIBIR SALUD
            collision.gameObject.GetComponent<PlayerScript>().RecibirSalud(salud);

            if (collision.gameObject.GetComponent<PlayerScript>().destruirVidaCalabaza) {
                gameObject.GetComponent<ParticleSystem>().Play();
                
                // SE PODRIA HACER QUE SI ESTA A TOPE DE SALUD QUE NO HAGA NADA LO VERMOS MAS ADELANTE ///
                // PARA QUE SE DESTRUYA A LOS 2 SEGUNDOS
                Destroy(gameObject,2);
            }
        }
    }

    
}

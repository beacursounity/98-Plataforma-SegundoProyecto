using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigoEstatico : MonoBehaviour {
	
	private void OnCollisionEnter2D(Collision2D other) { 

       //  SI CHOCA CON EL PLAYERCALABAZA
        if (other.gameObject.name == "PlayerCalabaza") {
			// me quita 1 vidas cuando colisiono con la Bala del Enemigo Estatico
            other.gameObject.GetComponent<PlayerScript>().QuitarVidas();

            // CAMBIAR LA POSICION HACIA ATRAS CUANDO NOS DEN 
            other.gameObject.GetComponent<PlayerScript>().CambiarPosicionPlayer();
		}

        // DESTRUIR BALA 
        Destroy(gameObject,1);
    }
}

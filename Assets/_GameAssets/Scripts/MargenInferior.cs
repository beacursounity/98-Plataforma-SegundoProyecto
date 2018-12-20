using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MargenInferior : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "PlayerCalabaza") {
            // ME QUITA 1 VIDA CUANDO CAIGO AL VACIO
            collision.gameObject.GetComponent<PlayerScript>().QuitarVidas();

            // SI TIENE TODAVIA VIDAS LLAMO A LA FUNCION PARA QUE POSICIONE AL PLAYER 
            // DE NUEVO AL PRINCIPIO.
            if (collision.gameObject.GetComponent<PlayerScript>().GetVidas() > 0) {
                // SALTO AL VACIO
                collision.gameObject.GetComponent<PlayerScript>().SaltoAlVacio();
            }
        }
    }
}

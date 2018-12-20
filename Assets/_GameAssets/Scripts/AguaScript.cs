using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AguaScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.gameObject.name == "PlayerCalabaza")
        {
            // me quita 1 vidas cuando colisiono con el agua
            collision.gameObject.GetComponent<PlayerScript>().QuitarVidas();

            // CAMBIAR LA POSICION HACIA ATRAS CUANDO NOS DEN 
            collision.gameObject.GetComponent<PlayerScript>().CambiarPosicionPlayer();
        }
    }
}

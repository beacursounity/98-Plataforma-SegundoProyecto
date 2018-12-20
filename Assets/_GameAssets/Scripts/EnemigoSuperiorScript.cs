using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoSuperiorScript : MonoBehaviour {

    int puntosEnemigo = 10;

    private void OnCollisionEnter2D(Collision2D collision) {
      
        if (collision.gameObject.name == "PlayerCalabaza" ){
            // INCREMENTAMOS LOS PUNTOS AL MATAR AL ENEMIGO POR ENCIMA 5 PUNTOS
            collision.gameObject.GetComponent<PlayerScript>().IncrementarPuntuacion(puntosEnemigo);
            // DESTRUIR ENEMIGO
            Destroy(transform.parent.gameObject,2);
        }
    }
}

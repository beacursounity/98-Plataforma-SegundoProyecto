using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaCalabaza : MonoBehaviour {

	int puntosBala = 10;
	[SerializeField] PlayerScript player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Malo")
        {
            print("colision Bala con Malo ");
            print("incrementar puntos "+puntosBala);
            // INCREMENTAMOS LOS PUNTOS AL MATAR AL ENEMIGO POR ENCIMA 5 PUNTOS
            player.IncrementarPuntuacion(puntosBala);

            // DESTRUIR ENEMIGO
            Destroy(collision.gameObject);

            // DESTRUYO LA BALA
            Destroy(gameObject);
        } 
        // SI NO COLISIONO CON EL MALO DESTRUYO LA BALA
        else {
            Destroy(gameObject);
        }

    }
}

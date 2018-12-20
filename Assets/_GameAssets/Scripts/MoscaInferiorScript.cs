using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoscaInferiorScript : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "PlayerCalabaza") {
            //print("colision inferior");
            // LE HACEMOS EL DAÑO DE 20 EN LA SALUD
            collision.gameObject.GetComponent<PlayerScript>().RecibirDanyo(20);
        }
    }
}

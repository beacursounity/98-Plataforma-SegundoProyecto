using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour {

    int danyo = 20;

    private void OnCollisionEnter2D(Collision2D collision) {
       
        if (collision.gameObject.name == "PlayerCalabaza") {
            // LE HACEMOS EL DAÑO DE 20 EN LA SALUD
            collision.gameObject.GetComponent<PlayerScript>().RecibirDanyo(danyo);
        }
    }
}

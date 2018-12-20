using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
     
        // Quitamos vidas y tenemos que volver hacia atras un poco
        // poner particulas.....
        //QuitarVidas(1);

        if (collision.gameObject.gameObject.name == "PlayerCalabaza") {
           //collision.gameObject.GetComponent<PlayerScript>().QuitarVidas();
        }
    }
}

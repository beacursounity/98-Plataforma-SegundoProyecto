using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other) {
       
		if (other.gameObject.gameObject.name == "PlayerCalabaza")
        {
           
            // VAMOS HACER QUE SALGAN PARTICULAS DE CARAMELOS
           gameObject.GetComponent<AudioSource>().Play();

            // HA LLEGADO A SU DESTINO
            Application.Quit();        
        }
	}
}

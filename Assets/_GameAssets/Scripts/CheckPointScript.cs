using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPointScript : MonoBehaviour {

    //[SerializeField] GameObject player;
    
    Text txtCaramelo;

    private void OnTriggerEnter2D(Collider2D collision) {

        if ( collision.gameObject.name == "PlayerCalabaza") {
            // PLAYERPREFS ES COMO UN DICIONARIO
            PlayerPrefs.SetFloat("X",this.transform.position.x);
            PlayerPrefs.SetFloat("Y",this.transform.position.y);

            // RECOGER LA PUNTUACION
            //int puntuacion =  player.GetComponent<PlayerScript>().puntos;
            int puntuacion = collision.gameObject.GetComponent<PlayerScript>().GetPuntos();

          //  print("puntos cuando colisiona con player GUARDADOS "+puntuacion);

            // RECOGER LOS CARAMELOS
            txtCaramelo = GameObject.Find("TextoCaramelo").GetComponent<Text>();
          
            // LO SALVAMOS
            PlayerPrefs.Save();

            // LLAMAMOS AL GAMECONTROLER
            GameControler.StorePosicion(collision.gameObject.transform.position);
            GameControler.StorePuntos(puntuacion);
            GameControler.StoreCaramelos(txtCaramelo.text);

            // SONIDO CUANDO LE DA EL PLAYERCALABAZA
            gameObject.GetComponent<AudioSource>().Play();
            // DESTRUIMOS EL OBJETO
            Destroy(this.gameObject);
        }
            
    }
}
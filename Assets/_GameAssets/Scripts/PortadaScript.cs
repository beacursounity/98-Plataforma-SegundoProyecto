using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortadaScript: MonoBehaviour {

    [SerializeField] RectTransform[] rt;
    [SerializeField] float speed = 30f;

	// ENTRAMOS EN LA PRIMERA ESCENA
	public void StartGame () {
        SceneManager.LoadScene(1);
	}

    // SALIMOS DEL JUEGO
    public void ExitGame () {
        Application.Quit();
	}

    public void DeleteGame () {
       PlayerPrefs.DeleteAll();
	}

    /*private void Update() {
       
        for (int i = 0; i < rt.Length; ++i) {
            // POSICION
            float xPos = -1 * Time.deltaTime * speed;
            // COGEMOS LA POSICION DE LA NUBE + ANCHO DE LA NUBE PARA QUE NO
            // SE VEA LA NUBE Y QUE VUELVA A EMPEZAR DESDE LA IZQUIERDA
            if ((rt[i].position.x + rt[i].rect.width) < 0) {
                xPos = 1100;
            }

            rt[i].Translate(xPos, 0, 0);
        }
        */ 
    

}
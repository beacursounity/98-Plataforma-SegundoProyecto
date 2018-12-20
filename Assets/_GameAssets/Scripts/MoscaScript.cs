using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoscaScript : MonoBehaviour {

    public Transform limiteDerecho;
    public Transform limiteIzquierdo;

    // DIRECCION
    bool haciaDerecha = false;

    // PODEMOS HACER DISTINTAS MOSCAS CON DISTINTOS DAÑOS
    [SerializeField] int danyo = 20;

    // 
    Slider slider;

    private void Start() {
        transform.position = limiteDerecho.position;
        // RECOGEMOS EL HIJO
        slider = GetComponentInChildren<Slider>();
        print(slider.gameObject.name);

        // TENDRIAMOS QUE LE DISPARASEN O PARA PODER ACTIVAR LA VIDA
        QuitarVida();
    }

    // Update is called once per frame
    void Update () {
	    if (haciaDerecha == true) {
            transform.Translate(Vector2.right * Time.deltaTime);
            if (transform.position.x > limiteDerecho.position.x) {
                haciaDerecha = false;
                CambiarOrientacion();
            }
        } else {
            transform.Translate(Vector2.left * Time.deltaTime);
            if (transform.position.x < limiteIzquierdo.position.x) {
                haciaDerecha = true;
                CambiarOrientacion();
            }
        }
	}


    void CambiarOrientacion() {

        // ESCALARLO EN EL EJE DE LAS Y
        if (haciaDerecha) {
            transform.localScale = new Vector2(-1, 1);
        } else {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void QuitarVida() {
        slider.value = slider.value - 50;
    }
    
}

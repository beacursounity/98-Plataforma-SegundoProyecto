using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    // PARA CARGAR TODAS LAS VIDAS
    //Image[] imagenesVida = new Image[5];
    public Image[] imagenesVida;

    public Image prefabImagenVida;
    // TENEMOS QUE RECOGER EL PANEL DE VIDAS PARA RECOGER LA REFERENCIA
    public GameObject panelVidas;

    public PlayerScript playerCalabaza;
    int numeroVidas;

	// Use this for initialization
	void Start () {

        numeroVidas = playerCalabaza.GetVidas();
        imagenesVida = new Image[numeroVidas];

		for (int i = 0; i < imagenesVida.Length; i++) {
            // INSTANCIAMOS EL PREFAB Y LO METEMOS DENTRO DEL PANELVIDAS
            imagenesVida[i] = Instantiate(prefabImagenVida, panelVidas.transform);
        }
	}
	
	// Update is called once per frame
	//void Update () {

    public void RestarVida() {

            // RECOGEMOS LAS VIDAS QUE NOS QUEDAN
            /*      numeroVidas = playerCalabaza.GetVidas();
                  for ( int i = numeroVidas; i < imagenesVida.Length - 1; i++) {
                      // SE DESTRUIRA CUANDO NO LO HALLA DESTRUIDO ANTES
                      if (imagenesVida[i].gameObject != null) {
                          Destroy(imagenesVida[i].gameObject); // ES MAS COSTOSO EL DESTROY
                      }
                   }
          */
      
        // LE CAMBIAMOS EL COLOR
        numeroVidas = playerCalabaza.GetVidas();
        for (int i = numeroVidas; i < imagenesVida.Length; i++) {
            // SE DESTRUIRA CUANDO NO LO HALLA DESTRUIDO ANTES
            if (imagenesVida[i].gameObject != null) {
                imagenesVida[i].color = new Color32(160, 160 , 160 , 128); //(,,,nIVEL ALPHA)
            }
        }

    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEstaticoScript : MonoBehaviour {

	// VARIABLES PARA QUE INVOKEN AL METODO LANZARBALA
	// CADA CIERTO TIEMPO
	float comienzo = 2.0f;
	float cadacuanto = 4.0f;

	// LE PASAMOS EL TRANSFORM DEL PLAYER PARA QUE VAYA A ESA POSICION LA BALA
	[SerializeField] GameObject prefabBala;
	// POTENCIA DE DISPARO
	int potenciaDisparo = 50;

	// REFERENCIA DEL PLAYER PARA SABER DONDE ESTOY
	// Y EL DISPARO SE DIRIGIRA HACIA EL
	 [SerializeField] Transform player;

	// Use this for initialization
	void Start () {
		InvokeRepeating("LanzarBala", comienzo, cadacuanto);
	}
	

	// Update is called once per frame"
	void LanzarBala() {
		// INSTANCIAMOS LA BALA
		GameObject bala = Instantiate(prefabBala,transform.position,transform.rotation);
		Vector2 posicionPlayer = player.transform.position - transform.position ;

		 // PARA HACER LA FUERZA CON RESPECTO AL MUNDO
        bala.GetComponent<Rigidbody2D>().AddRelativeForce( posicionPlayer * potenciaDisparo );
		bala.GetComponent<AudioSource>().Play();

		// DESTRUIR BALA 
		Destroy(bala,0.5f);

	}
}

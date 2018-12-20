using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasarelaScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InvokeRepeating("ActivarDesactivar",2.0f,5.0f);
	}
	
	// Update is called once per frame
	void ActivarDesactivar () {
		// SI ESTA ACTIVO SE DESACTIVA Y AL REVES
		if (gameObject.active == true  ){
			gameObject.SetActive(false);
		}
		else{
			gameObject.SetActive(true);
		}

	}
}

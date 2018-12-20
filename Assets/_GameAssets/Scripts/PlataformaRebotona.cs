using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaRebotona : MonoBehaviour {

    bool mover = false;

    Vector2 posicionPlataforma;
    // NO ESTA DESACTIVADA MOVER PLATAFORMA
    bool desactivarMoverPlataforma = false;

    [SerializeField] GameObject puntoGeneracion;
    [SerializeField] GameObject prefabCaramelo;
    // PARA CUANDO GOLPEE LA PLATAFORMAREBOTONA EL PLAYER
    AudioSource audio;

    // PARA INSTANCIAR EL CARAMELO
    GameObject caramelo;

    // PARA QUE SI LE DAMOS MAS VECES A LA PLATAFORMAREBOTONA
    // QUE NO INSTANCIE OTRO CARAMELO
    bool crearSoloUnaVezCaramelo = true;

    // Use this for initialization
    void Start () {
        //posicionPlataforma = transform.position;
        audio = GetComponent<AudioSource>();
    }     


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (crearSoloUnaVezCaramelo && collision.gameObject.gameObject.name == "PlayerCalabaza")
        {
            gameObject.GetComponent<Animator>().enabled = true;
            audio.Play();
            // CREA EL CARAMELO
            Invoke("CrearCaramelo",0.8f);
            // PARA ANIMACION
            Invoke("PararAnimacion",1);    
        
        }        
    }

    void CrearCaramelo(){
        // PARA QUE SOLO SE GENERE OTRA VEZ CUANDO SE DESTRUYA
        // EL CARAMELO
        crearSoloUnaVezCaramelo = false;
        // APARECE EL CARAMELO
        caramelo = Instantiate(prefabCaramelo,
                puntoGeneracion.transform.position,
                Quaternion.identity);

        // ESPERARA UN POCO A DESTRUIR EL CARAMELO
         StartCoroutine(DestruirCaramelo());
}
    // PARA ANIMACION DE LA CAJA
    void PararAnimacion(){
        gameObject.GetComponent<Animator>().enabled = false;
    }
   
   
    IEnumerator DestruirCaramelo()
    {  
        yield return new WaitForSeconds(10.0f);
        //print("DESTRUIR CARAMELO");
        Destroy(caramelo);
        crearSoloUnaVezCaramelo = true;
    }

}

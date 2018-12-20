using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour {
    Rigidbody2D rb2D;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 8;

    [Header("PUNTOS....")]
    public static int puntos = 0;
    public int caramelosPlayer = 0;
    public static int tiempo = 10;

    // LO RECOGEMOS LUEGO EN EL START PARA RELLENAR LOS TEXTOS DEL CANVAS
    private Text txtPuntuacion;
    private Text txtCaramelo;
    private Text txtTiempo;
    Image gameOver;

    // VARIABLE PARA PARAR JUEGO CUANDO TERMINE EL TIEMPO
    public static bool jugar = true;

    [Header("POSICION SALTO")]
    [SerializeField] float radioOverLap = 0.03f;
    [SerializeField] Transform posicionPies;

    // CAPA PARA LA COLISIONES
    [SerializeField] LayerMask floorLayer;

    [Header("DISPARO")]
    // GENERAMOS LA BALA CUANDO LE DEMOS A LA TECLA Z
    [SerializeField] GameObject puntoGeneracionBala;
    [SerializeField] GameObject prefabBala;
    [SerializeField] int potenciaDisparo = 500;

    // PARA LA BARRA DE ESTADO
    float vida = 1;

    // REFERNACIA AL PLAYERANIMATOR
    Animator playerAnimator;

    // ESTRUCTURAS
    enum EstadoPlayer { Pausa, AndandoDerecha, AndandoIzquierda, Saltando, Sufriendo};
    EstadoPlayer estado = EstadoPlayer.Pausa;

    // VIDAS
    int vidasMaximas = 5;
    [SerializeField] int vidas;
    int saludMaxima = 100;
    [SerializeField] int salud;
    // BARRA SALUD
    [SerializeField] Image barraDeVida;

    // IMPULSOS PARA CUANDO VAMOS HACIA ATRAS Y HENMOS DADO ALGUN ENEMIGO
    public int fuerzaImpactoX = 4;
    public int fuerzaImpactoY = 5;

    // RECOGEMOS EL SCRIPT DE UISCRIPT PARA PODER LLAMAR AL METODO

    [SerializeField] UIScript uiScript;
    public bool destruirVidaCalabaza = false;

    [SerializeField] GameObject explosion;

    int puntuacionrecogida;
    string caramelosrecogidos;

    private void Awake() {
        // INICIALIZAMOS LAS VARIABLES ESTATICAS PARA QUE CUANDO VOLVAMOS A JUGAR
        // ESTEN TODAS LAS VARIABLES 
        puntos = 0;
        tiempo = 60;
        jugar = true;
        // LO PONEMOS EN LA AWAKE
        vidas = vidasMaximas;
        salud = saludMaxima;
    }

    // Use this for initialization
    void Start () {
    
        rb2D = GetComponent<Rigidbody2D>();
        // 
        txtPuntuacion = GameObject.Find("Puntuacion").GetComponent<Text>();
        txtCaramelo = GameObject.Find("TextoCaramelo").GetComponent<Text>();
        txtTiempo = GameObject.Find("TextoTiempo").GetComponent<Text>();
        gameOver = GameObject.Find("GameOver").GetComponent<Image>();

        // OBTENEMOS LA PUNTUACION Y LOS CARAMELOS
        txtPuntuacion.text = "Score: " + puntos;
        txtCaramelo.text = "0";
        txtTiempo.text = "Time: "+tiempo+ " sg";

        // HACEMOS UN INVOKEREPEATING A CADA SEGUNDO PARA QUE ME VAYA CAMBIANDO EL TXTTIEMPO DEL CANVAS
        // Y CUANDO LLEGUE A 0 SE PARARA TODO EL JUEGO
        InvokeRepeating("ContarSegundos",1, 1);
        
        // RECOGEMOS LA REFERENCIA DE LA ANIMACION
        playerAnimator = GetComponent<Animator>();  

        // LEEMOS EL FICHERO QUE HEMOS GENERADO
        // COMPROBAMOS SI EXISTEN ANTES DE COGERLA PARA QUE NO CASQUE
        // CON QUE SOLO HAGAMOS UNA VALDRIA YA QUE 
        if (PlayerPrefs.HasKey("X") &&
            PlayerPrefs.HasKey("Y")) {
            float x = PlayerPrefs.GetFloat("X");
            float y = PlayerPrefs.GetFloat("Y");
            
            // Y PONEMOS ESAS POSICIONES AL PLAYER
            this.transform.position = new Vector2(x, y);
            
        }
        
        // RECUPERAMOS LA POSICION
        Vector2 position = GameControler.GetPosition();
        // Y SI ES DISTINTO DE ZERO ES QUE TIENE DATOS Y ME PONE LA POSICION
        if (position != Vector2.zero) {
            this.transform.position = position;

            // Y RECUPERAMOS LOS PUNTOS
            puntuacionrecogida = GameControler.GetPuntos();
           // print("puntosrecogidos en Player al principio "+ puntuacionrecogida);

            // Y SI ES DISTINTO DE ZERO ES QUE TIENE DATOS Y ME PONE LA POSICION
            if (puntuacionrecogida > 0) {
                txtPuntuacion.text = "Score: " + puntuacionrecogida;
                puntos = puntuacionrecogida;
            }

            // RECUPERAMOS LOS CARAMELOS SOLO UNA VEZ NO HACERLO EN EL START DEL 
            // CARAMELO YA QUE TENDRE MUCHOS Y SE EJECUTARIAN TODOS SUS START
            // TAMBIEN SE PODRIA HACER CON UN GO VACIO PARA RECOGER TODAS LAS COSAS
            caramelosrecogidos = GameControler.GetCaramelos();

            //print("caramelosrecogidos en Player al principio "+caramelosrecogidos);

            // Y SI ES DISTINTO DE ZERO ES QUE TIENE DATOS Y ME PONE LA POSICION
            if (caramelosrecogidos != null) {
                txtCaramelo.text = caramelosrecogidos;
            }

        } 
        // SI ES LA PRIMERA VEZ QUE COMIENZA EL JUEGO RECOGEMOS LA POSICION
        // DEL PLAYER POR SI SE TIRA AL VACIO VUELVA A COMENZAR DONDE ESTABA
        else {
            // RELLENAMOS ESTOS VALORES PARA QUE COMIENZE AHI Y CARGAMOS
            // LAS POSICIONES EN SU SITIO
            PlayerPrefs.SetFloat("X", gameObject.transform.position.x);
            PlayerPrefs.SetFloat("Y", gameObject.transform.position.y);

            GameControler.StorePosicion(gameObject.transform.position);
        }

       
        
    }

    private void Update() {

        // SI JUGAR ES FALSE SE ACABO EL JUEGO
        if (!jugar) {
            CancelInvoke("ContarSegundos");

            // MOSTRAMOS UNA IMAGEN DE GAMEOVER
            gameOver.GetComponent<Image>().enabled = true;
            // PARAMOS LAS ANIMACIONES
            GameControler.PararAnimaciones();

            // FINALIZAMOS EL JUEGO
            Invoke("FinalizarJuego", 2);
        }
        
        // SI ESTA SALTANDO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            estado = EstadoPlayer.Saltando;
        }

        if (Input.GetKeyDown(KeyCode.Z) && estado != EstadoPlayer.Pausa)
        {

            // APARECE EL CARAMELO
            GameObject bala = Instantiate(prefabBala,
                    puntoGeneracionBala.transform.position,
                    puntoGeneracionBala.transform.rotation);
            // SI EL PLAYER ESTA MIRANDO HACIA LA DERECHA LA BALA SALDRA
            // A LA DERECHA O A LA IZQUIERDA SI ESTA MIRANDO A LA IZQUIERDA
            if ( estado == EstadoPlayer.AndandoDerecha)
            {
                // PARA HACER LA FUERZA CON RESPECTO AL MUNDO
                bala.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * potenciaDisparo);
                bala.GetComponent<AudioSource>().Play();
            }
            else if ( estado == EstadoPlayer.AndandoIzquierda)
            {
                // PARA HACER LA FUERZA CON RESPECTO AL MUNDO
                bala.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * potenciaDisparo);
                bala.GetComponent<AudioSource>().Play();
            }
            else{
               // print(estado);
            }
        }
        // SI ESTA SUFRIENDO Y ESTA EN EL SUELO LO DEJAMOS EN PAUSA
        if (estado == EstadoPlayer.Sufriendo && EstaEnElSuelo()) {
            // DEJAMOS EL ESTADO A PAUSA PARA QUE VUELVA ACTIVAR LAS FUERZAS
            estado = EstadoPlayer.Pausa;
        }

    }
    // Update is called once per frame
    void FixedUpdate () {
        
        float xPos = Input.GetAxis("Horizontal");

        //float yPos = Input.GetAxis("Vertical");

        // LA VELOCIDAD EN EL EJE Y ES CONSTANTE Y LE APLICA LA FUERZA DE LA GRAVEDAD
        // SI NO LO PONEMOS VA MUY DESPACIO
        //float ySpeed = rb2D.velocity.y; 

        // VAMOS A PONER QUE LA VELOCIDAD EN X E Y SEA PROPORCIONAL
        //float ySpeed = rb2D.velocity.y;

        // GUARDAR LA VELOCIDAD ACTUAL EN EL EJE DE LA Y
        // VELOCIDAD QUE TENIA ANTERIOR
        float ySpeedActual = rb2D.velocity.y;


        // PARA SALIRSE DE LA FUNCION PARA QUE NO HAGA LOS VELOCITY CUANDO NOS 
        // ESTA QUITANDO VIDAS
        if (estado == EstadoPlayer.Sufriendo) {
            return;
        }

        // PARA SABER LA DIRECCION QUE TIENE EL PLAYER
        // PARA LOS DISPAROS
       // float valorAbsoluto = Mathf.Abs(xPos);

        // CAMBIAR POSICION Y ANIMACION DEL PLAYER
        if ( Mathf.Abs(xPos) > 0.01f) {
            // ANDA
            // MODIFICAMOS EL PARAMETRO DEL ANIMATOR CUIDADITO
            playerAnimator.SetBool("Andando", true);
        }
        else 
        {   // SE PARA
            // MODIFICAMOS EL PARAMETRO DEL ANIMATOR
            playerAnimator.SetBool("Andando", false);  
        }



        // SI LA POSICION DE y > 0 
        //if (yPos > 0) {
        // SI LA ESTOY PULSANDO SE MOVERA 
        // LO DE PREGUNTAR XPOS = 0 ES MAS ARRIESGADO (DIRECCION PLAYER)
        // (valorAbsoluto > 0.01f) { // NO SALTA CUANDO ESTOY PARADO Y ESTA MAL HAY QUE MODIFICARLA
        // PORQUE QUEREMOS QUE SALTE AUNQUE ESTE PARADO
        //if (saltando)

   
       // print(estado);
        // Añadimos el Enumerador
        if (estado == EstadoPlayer.Saltando) { // SI ESTAS SALTANDO ES UN ESTADO MOMENTANEO
            estado = EstadoPlayer.Pausa; // LO PONEMOS AQUI PARA QUE SALTE SOLO UNA VEZ
            // ESTA EN EL SUELO
            if (EstaEnElSuelo()) {
                // LE PONEMOS SOLO LA FUERZA DE SALTO
                rb2D.velocity = new Vector2(xPos * speed, jumpForce);
            }
            // CUANDO NO ESTES EN EL SUELO PARA QUE NO VAYA LATERALMENTE
            else
            { //PARA IR HACIA LA DERECHA E IZQUIERDA
                rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            } 

        } else if (xPos > 0.01f) // HACIA LA DERECHA
          { 
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
            if (estado == EstadoPlayer.AndandoIzquierda) {
                // PARA QUE ROTE
                transform.localScale = new Vector2(1, 1);
            }
            estado = EstadoPlayer.AndandoDerecha;
          } 
        else if (xPos < -0.01f) // HACIA LA IZQUIERDA
          {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
           if (estado == EstadoPlayer.AndandoDerecha || estado == EstadoPlayer.Pausa) {
                transform.localScale = new Vector2(-1, 1);
            }
            estado = EstadoPlayer.AndandoIzquierda;
        }

        /* // CUANDO SUELTE
        else if (valorAbsoluto > 0.01f)  
        {
            rb2D.velocity = new Vector2(xPos * speed, ySpeedActual);
        }*/

            //rb2D.velocity = new Vector3(xPos * speed, ySpeedActual);
        

    }

    // CONTAMOS LOS SEGUNDOS Y SE MOSTRARAN
    public void ContarSegundos() {
        tiempo = tiempo - 1;
        txtTiempo.text = "Time: " + tiempo + " sg" ;
        if (tiempo == 0) {
            jugar = false;
        }
    }
  
    public void QuitarVidas() {
       

        // SE PUEDE PONER UN SONIDO CUANDO MUERA
        vidas = vidas - 1;

        // HACEMOS QUE SUENE EL AUDIOSOURCE
        gameObject.GetComponent<AudioSource>().Play();

        // QUITAMOS LOS CORAZONES DE LA ESCENA
        if (vidas > 0)
        {
            // RECOGEMOS LOS HIJOS DEL PANEL
            uiScript.RestarVida();

            // VOLVEMOS A DEJAR EL ESTADO DE LA BARRA ENTERA
            vida = 1;
            barraDeVida.fillAmount = vida;

        }
        // SI YA NO TENGO VIDAS SE DESTRUYE
        else if ( vidas == 0) {
            // INSTANCIAMOS LA EXPLOSION DL PLAYERCALABAZA
            Instantiate(explosion, transform.position, Quaternion.identity);
            // ESPERAMOS UN POCO PARA QUE DE TIEMPO A MOSTRAR LA EXPLOSION
            Invoke("FinalizarJuego", 2);
         
        }

    }

    private void FinalizarJuego() {
        // CARGAMOS LA PORTADA
        SceneManager.LoadScene(0);
        //Destroy(gameObject);
    }

    public void IncrementarPuntuacion(int puntosAIncrementar) {
      //  print("INCREMENTARPUNTUACION. PUNTOS ANTES DE INCREMENTAR " +puntos);
      

        puntos = puntos + puntosAIncrementar;
        txtPuntuacion = GameObject.Find("Puntuacion").GetComponent<Text>();

        txtPuntuacion.text = "Score: " + puntos;

        print("INCREMENTARPUNTUACION. PUNTOS ACTUALES " + puntos);
    }


    // VAMOS A SABER CON QUE COLLIDER DE UNA CAPA DETERMINADA
    public bool EstaEnElSuelo() {
        bool enSuelo = false;

        // DECLARAMOS UN COLLIDER2D
        Collider2D col = Physics2D.OverlapCircle(posicionPies.position, radioOverLap, floorLayer);

        // LO HEMOS ENCONTRADO EL COLLIDER
        if ( col!= null) {
            enSuelo = true;
        }

        return enSuelo;
    }

    // EL DAÑO QUE LE PRODUCIRA LA MOSCA Y LE TENDRA QUE IMPULSAR
    public void RecibirSalud(int incrementoSalud) {
      
        salud = salud + incrementoSalud;

        // Esto es lo mismo que el IF de abajo
        //salud = Mathf.Min(salud, saludMaxima);

        if (salud > saludMaxima) {
            salud = saludMaxima;
            destruirVidaCalabaza = false;
        } 
        else {
            destruirVidaCalabaza = true;
            // PROBARLO 40 incremento salud / 100 (total salud)
            //decimal valor = Mathf. incrementoSalud / 100;
            // CHAPUZA
            /*
            if (incrementoSalud == 40){
                 vida += 0.4f;
            }
            */
            // FUNCIONA PERFECTAMENTE
            vida = vida + (incrementoSalud / 100f); 
            
        }

        /* Es lo mismo de arriba OPERADOR TERNARIO
         * salud = (salud > saludMaxima) ? saludMaxima : salud;*/

        //vida += 0.2f + incrementoSalud;


        barraDeVida.fillAmount = vida;

    }


    // EL DAÑO QUE LE PRODUCIRA LA MOSCA Y LE TENDRA QUE IMPULSAR
    public void RecibirDanyo(int danyo) {

        vida -= 0.2f;
        barraDeVida.fillAmount = vida;

        salud = salud - danyo;
   
        if (salud <= 0) {
            // QUITAMOS LOS CORAZONES 
            QuitarVidas();
            salud = saludMaxima;
        }

        // CAMBIAR LA POSICION HACIA ATRAS CUANDO NOS DEN 
        CambiarPosicionPlayer();
     
        // falta cuando estoy quieto y me da la mosca y ver hacia donde me da la mosca y poner el contrario

    }

    public void CambiarPosicionPlayer(){
        // FUERZA EN DIAGONAL HACIA ARRIBA HACIA LA IZQUIERDA
        // HACIA LA IZQUIERDA
        if (estado == EstadoPlayer.AndandoDerecha) {
            GetComponent<Rigidbody2D>().AddRelativeForce(
                new Vector3(-fuerzaImpactoX, fuerzaImpactoY), ForceMode2D.Impulse);
             estado = EstadoPlayer.Sufriendo;

        } else if (estado == EstadoPlayer.AndandoIzquierda) {
    
            GetComponent<Rigidbody2D>().AddRelativeForce(
                new Vector3(fuerzaImpactoX, fuerzaImpactoY), ForceMode2D.Impulse);
             estado = EstadoPlayer.Sufriendo;
        }

    }


    // RECOGER 
    public int GetVidas() {
        return this.vidas; 
    }

    // RECOGER 
    public int GetPuntos() {
        return puntos;
    }

    public void SaltoAlVacio() {
        // OBTENEMOS LA POSICION DEL PLAYER
        Vector2 posicion = GameControler.GetPosition();
        // PONEMOS LA POSICION QUE TENIA ANTERIORMENTE EN EL JUEGO
        gameObject.transform.position = posicion;
    }

        /*void CambiarOrientacion() {

            // ESCALARLO EN EL EJE DE LAS Y
            if (derechaPlayer) {
                transform.localScale = new Vector2(1, 1);
            } else {
                transform.localScale = new Vector2(-1, 1);
            }
        }*/

        // VERSION BASADA EN TAG
        /* public bool EstaEnElSuelo() {

             // ESTA EN EL SUELO Y NO PUEDE SALTAR 
             bool tocandoSuelo = false;

             // COMPROBAMOS EL OVERLAPSHERE 
             // NOS DA TODOS LOS COLLIDER CON RESPECTO AL RADIO
             Collider2D[] cols = Physics2D.OverlapCircleAll(posicionPies.position,radioOverLap);

             // QUE NO SEA NULO Y QUE CON EL QUE COLISIONE SEA DISTINTO DEL PLAYER
             for (int i = 0; i < cols.Length; i++) {
                 if (cols[i].gameObject.tag == "Suelo") {

                     print(cols[i].gameObject.name);
                     tocandoSuelo = true;
                     break; // PARA SALTARSE EL RESTO DE LAS ITERACIONES    
                 }
             }

             return tocandoSuelo;

         }*/

    }

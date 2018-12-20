using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : MonoBehaviour {

    // LO QUE LE DICE AL SISTEMA OPERATIVO NO VA A OCUPAR MAS DE 4BIT
    // PRIVATE PARA QUE NO SE VEA DESDE FUERA Y CONSTANTE PARA QUE NO SE PUEDA CAMBIAR EN 
    // TIEMPO DE EJECUCION

    private const string XPOS = "xPos";
    private const string YPOS = "yPos";
    private const string  PUNTOS = "puntos";
    private const string  CARAMELOS = "caramelos";

    // EL STATIC YO PUEDO LLAMAR A LA CLASE SIN HACER UNA INSTACIA DE LA CLASE
    public static void StorePosicion (Vector2 posicion) {
        // print ("ha pasado");
        // HARDCODE ES PONER ENTRECOMILLAS LA ETIQUETA
        PlayerPrefs.SetFloat(XPOS, posicion.x);
        PlayerPrefs.SetFloat(YPOS, posicion.y);
        PlayerPrefs.Save();

    }

    public static Vector2 GetPosition() {

        Vector2 position = new Vector2();
        if (PlayerPrefs.HasKey(XPOS) &&
            PlayerPrefs.HasKey(YPOS)) {
            float x = PlayerPrefs.GetFloat(XPOS);
            float y = PlayerPrefs.GetFloat(YPOS);
            //print(x);
            //print(y);
            // Y PONEMOS ESAS POSICIONES AL PLAYER
            //this.transform.position = new Vector2(x, y);
            position = new Vector2(x, y);
        } 
        // SI NO LO ENCUENTRA DEVUELVE UN VECTOR2 A 0
        else {
            position = Vector2.zero;
        }

        return position;
    }

   // CARGAMOS LOS PUNTOS
   public static void StorePuntos (int puntuacion) {
        //print ("Cargado punto en PlayerPrefabs "+puntuacion);
        PlayerPrefs.SetInt(PUNTOS, puntuacion);
        PlayerPrefs.Save();

    }

    // RECOGEMOS LOS PUNTOS
    public static int GetPuntos() {
        int puntuacion = 0;
        //print("ESTOY EN GETPUNTOS " +PUNTOS);

        if (PlayerPrefs.HasKey(PUNTOS)) {
          //  print("HA ENCONTRADO LA CLAVE PUNTOS");
            puntuacion =  PlayerPrefs.GetInt(PUNTOS);
            //print("OBTENIDO PUNTOS "+puntuacion);
        } 

        return puntuacion;
    }


     // CARGAMOS LOS CARAMELOS
   public static void StoreCaramelos (string caramelos) {
        //print ("Cargado Caramelos en PlayerPrefabs "+caramelos);
        PlayerPrefs.SetString(CARAMELOS, caramelos);
        PlayerPrefs.Save();

    }

    // RECOGEMOS LOS CARAMELOS
    public static string GetCaramelos() {
        string caramelos = "0" ;
       
        if (PlayerPrefs.HasKey(CARAMELOS)) { 
            caramelos =  PlayerPrefs.GetString(CARAMELOS);
        } 

        return caramelos;
    }


    // VAMOS A COGER TODAS LAS ANIMACIONES PARA PARARLAS CUANDO HA TERMINADO
    // EL TIEMPO DE JUEGO

    public static void PararAnimaciones() {
        GameObject[] animaciones = GameObject.FindGameObjectsWithTag("Animaciones");
        
        for (int i = 0; i < animaciones.Length; i++) {
            animaciones[i].SetActive(false);
        }


    }

}

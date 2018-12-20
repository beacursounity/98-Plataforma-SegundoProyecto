using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : MonoBehaviour {

    LineRenderer lr;
    public Transform spiderTransform;
    // Use this for initialization
    void Start() {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update() {
        // POSICION FINAL DE LA LINEA. LA Z NOS DA IGUAL
        // Y SIEMPRE CON RESPECTO DEL MUNDO
        lr.SetPosition(1, spiderTransform.position);
    }
}

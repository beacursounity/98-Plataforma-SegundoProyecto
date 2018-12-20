using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloScript : MonoBehaviour {

    private void OnCollis2ionEnter2D(Collision2D collision) {
        Vector2 position = GameControler.GetPosition();
        collision.gameObject.transform.position = position;

    }
}

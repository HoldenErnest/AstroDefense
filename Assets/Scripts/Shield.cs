using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (gameObject.transform.position.x >= Player.wallPos) DestroyImmediate(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag.Equals("playerGun")) {
            Destroy(coll.gameObject, 0.01f);
        }
    }
}
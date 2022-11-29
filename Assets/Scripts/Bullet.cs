using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int speed = 1;
    public int damage = 1;
    public GameObject boom;

    void Start() {
        
    }

    void Update() {
        if (transform.position.x < Player.wallPos+0.5f) {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        } else {
            Player.loseHealth(damage);
            Instantiate(boom, this.transform.position, Quaternion.identity);
            DestroyImmediate(this.gameObject);
        }
    }
}

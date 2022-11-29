using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public GameObject[] layers;
    SpriteRenderer sr;

    void Start() {
        sr = layers[0].GetComponent<SpriteRenderer>();
    }

    void Update() {
        for (float i = 0; i < layers.Length; i++) {
            layers[(int)i].transform.Translate(-(i+1)/(layers.Length*300), 0, 0);
            sr.size = new Vector2(sr.size.x + ((i+1) / (layers.Length * 300)), sr.size.y);//< verry ineficient
        }
    }
}

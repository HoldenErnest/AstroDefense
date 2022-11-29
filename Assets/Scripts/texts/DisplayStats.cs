using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStats : MonoBehaviour {

    Text txt;

    void Start() {
        txt = this.GetComponent<Text>();
    }

    void Update() {
        txt.text = "Name" + "[" + Player.level + "] " + ":" + Player.title +":";
    }
}

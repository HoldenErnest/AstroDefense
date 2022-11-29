using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderValues : MonoBehaviour {

    private Slider sldr;
    public int sliderID = 0;

    void Start() {
        sldr = this.GetComponent<Slider>();
    }

    void Update() {
        if (sliderID == 0) {
            sldr.maxValue = Player.xpDiff;
            sldr.value = Player.xp - Player.oldXp;
        } else if (sliderID == 1) {
            sldr.maxValue = Player.maxHealth;
            sldr.value = Player.getHealth();
        }

    }
}

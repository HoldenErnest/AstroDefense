using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour {

    public int delay = 3;
    private Text txt;

    void Start() {
        txt = this.GetComponent<Text>();
        txt.text = Spawning.getLvlTitle() + " (" + Spawning.getLvlWaves() + " waves)\nby:" + Spawning.getLvlCreator();
        StartCoroutine(Remove());
    }

    void Update() {
        
    }
    IEnumerator Remove() {//recharge time for ranged attackers
        yield return new WaitForSeconds(delay);
        DestroyImmediate(this.gameObject);
    }

}

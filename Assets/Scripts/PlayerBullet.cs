using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public int pierce = 1;
    public float speed = 1.0f;
    private int pierced = 0;
    private static int bIn = 0;
    

    void Start() {
        Destroy(this.gameObject, 4f);
        if (Kits.kitID == 5) {//shotgun!
            if (bIn == 0) {//if no bullets have spawned
                transform.Rotate(0, 0, Player.getZ() - Mathf.Rad2Deg * 0.15f);
                bIn++;
            } else if (bIn == 1) {//if 1 other bullet spawned
                transform.Rotate(0, 0, Player.getZ() + Mathf.Rad2Deg * 0.15f);
                bIn++;
            } else {//otherwise(2 bullets have spawned already) spawn the forward bullet
                transform.Rotate(0, 0, Player.getZ());
                bIn = 0;
            }

        } else transform.Rotate(0, 0, Player.getZ());
    }

    void Update() {
        transform.Translate(-(speed*Time.deltaTime),0,0); 
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag.Equals("bot")) {
            pierced++;
            if (pierced >= pierce) Destroy(this.gameObject, 0.01f);//BULLET DEATH
        }
    }


}

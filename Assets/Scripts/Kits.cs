using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kits : MonoBehaviour {

        /// <kits>
        /// sniper - bSpeed, bPierce
        /// turret guy
        /// multiBullet
        /// </kits>
    

    //private int teir = 1; //<tier of the kit 1-3
    public static int kitID = 0;

    private int addHeal = 5;
    private bool waiting = false;

    public GameObject[] objects;


    public void SetKitHeal() {
        kitID = 1;
        Player.maxHealth = 130;
        Player.damage = 1f;
        Player.addHealth(30);
    }
    public void SetKitDamage() {
        kitID = 2;
        Player.addHealth(10);
        Player.setProjectile(objects[1]);
        Player.damage = 1f;
        Player.setReload(0.75f);
    }
    public void SetKitSnipe() {
        kitID = 3;
        Player.addHealth(10);
        Player.setProjectile(objects[2]);
        Player.damage = 1.5f;
        Player.setReload(1f);
        
    }
    public void SetKitGunner() {
        kitID = 4;
        Player.addHealth(10);
        Player.setReload(0.25f);
        Player.setProjectile(objects[3]);
        Player.damage = 0.5f;
        Player.autoFire = true;
    }
    public void SetKitShot() {
        kitID = 5;
        Player.addHealth(10);
        Player.setReload(1f);
        Player.damage = 0.9f;
    }

    void Start() {
        Player.setProjectile(objects[0]);//set projectile to default bullet
        SetKitGunner();
    }
    void Update() {
        if (kitID != 0) {
            if (!waiting) {
                if (kitID == 1) { StartCoroutine(wait()); waiting = true; }
            }
        }
    }
    public IEnumerator wait() {
        yield return new WaitForSeconds(30);
        Player.addHealth(addHeal);
        waiting = false;
    }

}

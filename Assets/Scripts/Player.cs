using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static int health = 100;
    public static float damage = 1f;
    public static float wallPos = 7.6f;
    public float rotSpeed = 1.0f;
    private static float zRot;
    public static bool autoFire = false;

    private static float minReloadTime = 0.5f;
    private bool canFire = true;

    public static int maxHealth = 100;
    public static int level = 0;
    public static int xp = 0;
    public static int xpDiff = 10;
    public static int oldXp = 0;
    public static string title = "Basic";
    public static int lostIntel = 0;

    //cool idea: add stat tracker for end of game, ex: bulletsFired/bulletsHit, botsDestroyed, ect.
    public GameObject[] objects;
    public static GameObject projectile; //< changes based on class

    void Start() {
        
    }

    void Update() {
        checkLevel();
        //Debug.Log("HP:" + health + " || FPS:" + 1 / Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!PauseMenu.isDefeated())
                PauseMenu.changeIsPaused();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
            rotSpeed /= 4;
        else if (Input.GetKeyUp(KeyCode.Mouse1))
            rotSpeed *= 4;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {//rotate gun
            transform.Rotate(0, 0, rotSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.Rotate(0, 0, -rotSpeed*Time.deltaTime);
        }

        if (!autoFire) {//if autofire is not enabled dont otherwise fire auto
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && canFire) {//Shoot Bullet
                zRot = transform.rotation.eulerAngles.z;
                Instantiate(projectile, this.transform.position, new Quaternion(0, 0, 0, 0));
                if (Kits.kitID == 5) {
                    Instantiate(projectile, this.transform.position, new Quaternion(0, 0, 0, 0));
                    Instantiate(projectile, this.transform.position, new Quaternion(0, 0, 0, 0));
                }
                canFire = false;
                StartCoroutine(reload());
            }
        } else {
            if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && canFire) {//Shoot Bullet
                zRot = transform.rotation.eulerAngles.z;
                Instantiate(projectile, this.transform.position, new Quaternion(0, 0, 0, 0));
                canFire = false;
                StartCoroutine(reload());
            }
        }

        if (Kits.kitID == 3) objects[0].SetActive(true);//if sniper show the lazer sight
    }

    IEnumerator reload() {
        yield return new WaitForSeconds(minReloadTime);
        canFire = true;
    }

    public static void getTitle() {
        if (level > 500) title = "Champion V";
        else if (level > 400) title = "Champion I";
        else if (level > 250) title = "Master II";
        else if (level > 200) title = "Master I";
        else if (level > 175) title = "Lord II";
        else if (level > 130) title = "Lord I";
        else if (level > 100) title = "Imperial I";
        else if (level > 80) title = "Advanced III";
        else if (level > 50) title = "Advanced II";
        else if (level > 30) title = "Advanced I";
        else if (level > 20) title = "Starter III";
        else if (level > 10) title = "Starter II";
        else title = "Starter I";
    }
    public void checkLevel() {
        if (xp >= oldXp + xpDiff) {
            addLevel();
            oldXp = xp;
            xpDiff += 5;
        }
    }

    public void addLevel() {
        level++;
    }

    //setters and getters
    public static float getZ() {
        return zRot;
    }
    public static int getHealth() {
        return health;
    }
    public static void loseHealth(int hp) {
        if (health - hp > 0) health -= hp;
        else health = 0;
    }
    public static void addHealth(int hp) {
        if (hp + health <= maxHealth) health += hp;
        else health = maxHealth;
    }
    public static void setHealth(int hp) {
        if (!(hp > maxHealth)) health = hp;
        else health = maxHealth;
    }
    public static void setProjectile(GameObject p) {
        projectile = p;
    }
    public static void setReload(float t) {
        minReloadTime = t;
    }
}

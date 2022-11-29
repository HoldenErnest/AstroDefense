using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour {

    public float speed = 1.0f;//speed of bot
    public int damage = 1;//damage per attackTime
    public float HP = 1f;//hits till the bot is destoryed
    public float range = 0;//distance to attack
    public float attackTime = 1;//time to recharge attack
    public int xpDrop = 10;
    public float delaySpawn = 5;

    public GameObject projectile;//object fired has its own speed ect.
    public GameObject sBoom;//small explosion for bullet hits
    public GameObject bBoom;//big explosion for death explosion
    public GameObject thisBot;

    private bool canAttack = false;
    private float currX;
    private bool reloading = false;
    private bool spawned = false;
    private bool died = false;
    void Start() {
        //modifiers by difficulty
        if (Spawning.getDifficulty() < 0) speed *= 0.75f;
        else speed += (1.1f * Spawning.getDifficulty());
        xpDrop += Mathf.Abs(Spawning.getDifficulty()) * 2;
        attackTime -= 0.01f * Spawning.getDifficulty();

        //randomness modifiers
        attackTime += Random.Range(-attackTime/3, attackTime / 3);
        xpDrop += Random.Range(-3, 3);
        speed += Random.Range(-0.5f, 0.5f);
        delaySpawn += Random.Range(-(delaySpawn/2), (delaySpawn/2));
        if (range > 0)
            range += Random.Range(-0.5f, 0.5f);
        transform.position = new Vector3(-10, Random.Range(-4.5f, 4.5f), 0);
        if (this.gameObject.name.Contains("Wall")) {
            var wall = Instantiate(projectile, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
            wall.transform.parent = gameObject.transform;
        }
        StartCoroutine(spawn());
    }
    void Update() {
        if (spawned) {//wait until the bot spawned
            if (HP <= 0) {

                if (!died) {
                    Player.xp += xpDrop;
                    Instantiate(thisBot, new Vector3(-9, 0, 0), Quaternion.identity);
                    Destroy(this.gameObject, 0.01f);//BOT DEATH-----
                }
                died = true;
            }

            if (transform.position.x >= Player.wallPos - range) canAttack = true; //within range to attack

            if (!canAttack) {//wait until the bots in range then start unique attack sequence
                transform.Translate(speed * Time.deltaTime, 0, 0);
                currX = transform.position.x;
            } else if (range == 0) Attack();
            else FireAttack();
        }
    }

    void Attack() {//Basic back and forth attack from normal melee units
        if (transform.position.x > currX - 1) {
            transform.Translate(-(speed * Time.deltaTime) / attackTime, 0, 0);
        } else if (this.gameObject.name.Contains("Minion")) {
            Player.lostIntel += damage;
        } else {
            Player.loseHealth(damage);
            canAttack = false;
        }
        
    }
    void FireAttack() {//basic sequence of a ranged attack
        if (!reloading) {
            reloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload() {//recharge time for ranged attackers
        yield return new WaitForSeconds(attackTime);
        Instantiate(projectile, this.transform.position, this.transform.rotation);
        reloading = false;
    }
    IEnumerator spawn() {//recharge time for ranged attackers
        yield return new WaitForSeconds(delaySpawn);
        spawned = true;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (spawned) {
            if (coll.gameObject.tag.Equals("playerGun") && coll.IsTouching(this.gameObject.GetComponent<BoxCollider2D>())) {
                Instantiate(sBoom, coll.gameObject.transform.position, Quaternion.identity);
                HP -= Player.damage;
                Player.xp += xpDrop / 3;
            }
        }
    }

}

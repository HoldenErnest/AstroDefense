using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static GameObject gm;
    private static bool isPaused = false;
    public GameObject continueButton;
    public GameObject loseText;
    public GameObject winText;
    private static bool defeated = false;

    
    void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
        continueButton.SetActive(true);
        Player.setHealth(Player.maxHealth);
        Time.timeScale = 1;
        gm = this.gameObject;
        gm.SetActive(false);

        
    }

    public static void changeIsPaused() {
        if (!isPaused) {
            gm.SetActive(true);
            Time.timeScale = 0;
        } else {
            gm.SetActive(false);
            Time.timeScale = 1;
        }

        isPaused = !isPaused;

    }
    public static bool menuUp() {
        return isPaused;
    }
    public static bool isDefeated() {
        return defeated;
    }

    public void winScreen() {
        //display specific children for winscreen
        winText.SetActive(true);
        loseText.SetActive(false);
        continueButton.SetActive(true);
    }

    public void loseScreen() {
        //display specific children for lossscreen
        winText.SetActive(false);
        loseText.SetActive(true);
        continueButton.SetActive(false);
        defeated = true;
    }
}

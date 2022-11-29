using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawning : MonoBehaviour {

    public GameObject[] bots;//list of all copyable bots
    /* 
     * basic 0
     * Speed 1
     * gunner 2
     * tank 3
     * wall 4
     * stealth 5
     * sniper 6
     */
    public GameObject boss;
    public GameObject pauseScreen;

    private int[][] spawnOrder; // [numTotalWaves][Most bots in any wave]
    private int currentWave = 0; // lvl 5 gets you to wave 2, lvl 10 gets you to wave 3
    
    
    static string levelFile;
    string[][] spawnOrderString;//SpawnOrder before its parsed

    private string fileExtras; // extra bits of a level file not part of the spawning pattern
    private static string lvlTitle;
    private static string lvlCreator;
    private static int difficulty = 1;
    private int levelsForWave = 5; // Levels needed to clear each wave
    private static int totalWaves;
    private bool wavesClear = false;

    void Start() {
        string path = "Assets/levels/" + PlayerPrefs.GetString("loadLevel") + ".lvl";
        ReadToString(path); // initialize spawnOrder from a file

        SendToArray();
        EditExtras();
    }

    static void ReadToString(string p) { // Read the text directly from the file
        StreamReader reader = new StreamReader(p);
        levelFile = reader.ReadToEnd();
        reader.Close();
    }
    void SendToArray() {
        string[] lines = (levelFile.Split('\n')); // "0 1", "0 2", "1 2 0"
        fileExtras = lines[0];
        totalWaves = lines.Length - 1;
        string[] waves = new string[totalWaves];
        for (int i = 1; i < lines.Length; i++) {//distinguish waves from lines
            waves[i - 1] = lines[i];
        }

        spawnOrderString = new string[totalWaves][];
        for (int i = 0; i < waves.Length; i++) {
            spawnOrderString[i] = waves[i].Split(' '); // ["0","1"],["0","2"],["1","2","0"]
        }
        spawnOrder = new int[spawnOrderString.Length][];

        //turn the string array of waves into ints
        for (int i = 0; i < spawnOrderString.Length; i++) {
            spawnOrder[i] = System.Array.ConvertAll<string, int>(spawnOrderString[i], int.Parse);
        }
    }
    void EditExtras() {
        string[] parts = (fileExtras.Split(';'));
        lvlTitle = parts[0];
        lvlCreator = parts[1];
        levelsForWave = int.Parse(parts[2]);
        difficulty = int.Parse(parts[3]);
        if (difficulty > 50) difficulty = 50;
        if (difficulty < -1) difficulty = -1;
    }

    public static string getLvlTitle() {
        return lvlTitle;
    }
    public static string getLvlCreator() {
        return lvlCreator;
    }
    public static int getLvlWaves() {
        return totalWaves;
    }
    public static int getDifficulty() {
        return difficulty;
    }


    void Update() {
        if (Player.level / levelsForWave == currentWave) { // if new wave spawn bots
            if (currentWave != totalWaves)
                spawnBots();
            else if (!wavesClear) {
                //continue untill loss or return to menu
                //get stuff for beating level like unlock next level
                Debug.Log("you beat all rounds with bots");
                pauseScreen.GetComponent<PauseMenu>().winScreen();
                PauseMenu.changeIsPaused();
                wavesClear = true;
            }
        }
        if (Player.lostIntel >= 30 - (difficulty / 5)) {//if too much intel is lost spawn boss
            //Instantiate(boss);
            Player.lostIntel = 0;
            Debug.Log("Prefab to initiate boss fight. Boss has a unique script, maybe just multiple of somthing is spawned. maybe boss just moves up and down to dodge your bullets");
        }
        if (Player.getHealth() <= 0) {
            if (!PauseMenu.menuUp()) {
                Debug.Log("you lost");
                PauseMenu.changeIsPaused();
                if (!wavesClear)
                    pauseScreen.GetComponent<PauseMenu>().loseScreen();
            }
        }
    }

    void spawnBots() {

        foreach (int botIndex in spawnOrder[currentWave]) { // loop through all botIndex in the current wave
            if (botIndex != -1) {
                Instantiate(bots[botIndex]);
            } else {
                Debug.Log("Empty wave");
            }
        }
        currentWave++;
    }

    /*
     * basic understanding of mechanics:
     * files with extention .lvl are selected before playing a game, it will determine how the level plays
     * -----------------------------------------------------------------------------------
     * //levelName;creator;levels to advance wave;difficulty
     * level 1;SolarRanks;5         //<< this will give info other than spawning method on a level
     * 0 0 0
     * 0 1          //each line represents a different wave summoned every waveSpeed levels gotten. each number represents bot indecies
     * 2 2 3        //example: line 1 spawns 3 bots of index 0(basic bot)
     * 1 1 1
     */

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShowLevels : MonoBehaviour
{
    public static string path = "Assets/Levels/";
    private int unknownFiles;//counts how many files arent .lvl
    private int totalFiles;

    Text txt;
    void Start() {
        txt = this.GetComponent<Text>();

        foreach (string file in System.IO.Directory.GetFiles(path)) {
            totalFiles++;
            if (file.EndsWith(".lvl")) {
                txt.text += file.Substring(path.Length) + "\n";
            } else {
                unknownFiles++;
            }
        }
        txt.text += unknownFiles + " other files found...";
    }
    public static bool isFile(string fileName) {
        foreach (string file in System.IO.Directory.GetFiles(path)) {
            if (file.Equals(path + fileName + ".lvl")) {
                return true;
            }
        }
        return false;
    }
}

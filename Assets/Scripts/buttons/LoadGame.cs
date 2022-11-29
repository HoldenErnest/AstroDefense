using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
	//public Sprite SpriteOver;
	//public Sprite SpriteExit;
	//SpriteRenderer SR = GetComponent<SpriteRenderer>();

	public string sceneToLoad;
	public bool button = true;
	public string levelName;

	public InputField txt;
	void Start() {

	}

	void OnMouseUp() {
		if (button) {
			if (levelName != null) {
				PlayerPrefs.SetString("loadLevel", levelName);
			}
			SceneManager.LoadScene(sceneToLoad);
		}
	}

	void Update() {
		if (!button) {
			if (txt.isFocused && Input.GetKey(KeyCode.Return)) {
				levelName = txt.text;
				if (levelName != null) {
					if (levelName.EndsWith(".lvl")) {
						levelName = levelName.Remove(levelName.Length-4);
					}
					if (ShowLevels.isFile(levelName)) {
						PlayerPrefs.SetString("loadLevel", levelName);
						SceneManager.LoadScene(sceneToLoad);
					} else {
						txt.text = "not found...";
					}
				}
			}
		}
	}

	/*
	void OnMouseEnter() {
		SR.sprite = SpriteOver;
	}
	void OnMouseExit() {
		SR.sprite = SpriteExit;
	}
	*/
}

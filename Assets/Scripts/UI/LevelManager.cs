using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private bool canContinue = true;
	
	void Start () {
		string checkLevelName = PlayerPrefs.GetString("savedLevel");
		if(checkLevelName == null || checkLevelName == ""){
			canContinue = false;
		}
	}
	
	void newGame () {
		PlayerPrefs.DeleteAll();
		Application.LoadLevel("Castle");
	}
	
	void continueGame () {
		if(canContinue){
			string levelName = PlayerPrefs.GetString("savedLevel");
			Application.LoadLevel(levelName);
		}
	}
}

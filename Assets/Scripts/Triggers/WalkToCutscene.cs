using UnityEngine;
using System.Collections;

public class WalkToCutscene : MonoBehaviour {

	public bool EnableTrigger = true;

	GameObject Player;
	PlayerAnimator PAnimator;
	PlayerController PController;
	public GameObject Blackbox;
	BlackBoxScript BBScript;

	void Awake(){

	}

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		PAnimator = Player.GetComponent<PlayerAnimator> ();
		PController = Player.GetComponent < PlayerController> ();


		BBScript = Blackbox.GetComponent<BlackBoxScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (EnableTrigger == true) {

	//Put Move to Camera and Event script into one GameObject
		if (Player.transform.position.x >= gameObject.transform.position.x) {
				EnableTrigger = false;
			PlayerController.canControl = false;
			PController.ClearAllLists();
			//PAnimator.RunningInstance(true);
			PAnimator.IdleInstance();
			GameManager.CutsceneEnabled = true;
			CameraAnimator.WalkToCutscene = true;
				StartCoroutine(PauseAndOpenTextBox());
		}

			 


	}
	}

	IEnumerator PauseAndOpenTextBox(){
		yield return new WaitForSeconds (1.0f);
	//	BBScript.ChangeText ("Oh ho, so you've arrived!");
		GameManager.Scene = "Castle 1";
	//	BlackBoxScript.SpeedAble = true;
		BBScript.SwitchBoxDirection ();
		BBScript.Slide ("Oh ho, so you've arrived!");
		BlackBoxScript.FinishedText = false;


	}
}

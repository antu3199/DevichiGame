using UnityEngine;
using System.Collections;

public class EventTriggerScript : MonoBehaviour
{

	public bool DisableOnTrigger;
	public int TriggerNumber;
	public bool CameraEventTrigger;
	public bool TextEventTrigger;
	GameObject CameraSetTrigger;
	CameraSetTrigger CameraScript;
	bool isActive = true;
	GameObject Player;
	PlayerAnimator AnimatorScript;
	PlayerController ControllerScript;
	playerHealth HealthScript;
	GameObject BBox;
	BlackBoxScript BBoxScript;
	public bool OnGroundOnly;

	public string DisableOnEvent = "";

	GameManager ManagerScript;

	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find ("Player");
		AnimatorScript = Player.GetComponent<PlayerAnimator> ();
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
		ManagerScript = GameObject.Find ("GameManager").GetComponent<GameManager> ();


		BBox = GameObject.FindGameObjectWithTag ("BlackBox");
		BBoxScript = BBox.GetComponent<BlackBoxScript> ();


		if (CameraEventTrigger) {
			CameraSetTrigger = gameObject.transform.parent.gameObject;
			CameraScript = CameraSetTrigger.GetComponent<CameraSetTrigger> ();
		} else if (TextEventTrigger) {

		}

	}
	
	// Update is called once per frame
	void Update ()
	{

		//Debug.Log (GameManager.EnableTriggers);
	}

	void OnTriggerEnter (Collider other)
	{

		if (GameManager.EnableTriggers == true && ControllerScript.getTimeControl () == false) {


			if (CameraEventTrigger) {
				if (other.tag == "Player") {
					if (GameManager.Location == "Castle") {
						GameManager.CutsceneEnabled = true;
						PlayerController.canControl = false;


						CameraScript.setEventTrigger (true);
						isActive = false;



						if (GameManager.Scene == "Castle 1") {

							DisableForCutScene ();
							AnimatorScript.IdleInstance ();
							HealthScript.HideHearts ();


							StartCoroutine (PauseAndOpenTextBox ("Oh ho, so you've arrived!", false, "The Masked Man"));
							GameManager.Scene = "Castle 1";
							//	CameraAnimator.WalkToCutscene = true;
						}
					}

					if (GameManager.Location == "Field") {
						if (TriggerNumber == 3) {

							GameManager.CutsceneEnabled = true;
							GameManager.Scene = "Field Leafree";
							PlayerController.canControl = false;
							AnimatorScript.IdleInstance ();
							DisableForCutScene ();
							
							CameraScript.setEventTrigger (true);
							HealthScript.HideHearts ();

							isActive = false;
							GameManager.EventNumber = 1;
							StartCoroutine (PauseAndOpenTextBox ("Hey Devichi!", false, "Leafree"));

						}


					}

				}


			} else if (TextEventTrigger) {
				if (other.tag == "Player") {
					if (isActive == true) {


					
						if (GameManager.Location == "Field") {
							//Step on Devilchu's Head
							if (TriggerNumber == 1) {


								//Debug.Log("TEST2");
								//if (BBoxScript.getisOpen() == false){
								//	Debug.Log("TEST");
								GameManager.Scene = "Field Extra 1";
								GameManager.EventNumber = 0;
								BBoxScript.setLeftPictureName ("Devilchu");
								BBoxScript.Slide ("AHHHH!! Why are you jumping on my head???");
								BBoxScript.ActivateEmoticon ("Devilchu", "Angry", false);
								//}
							}

							if (TriggerNumber == 2) {

								//GameManager.CutsceneEnabled = true;
								PlayerController.canControl = false;
								//	PlayerController.vel.x = 0.0f;
								AnimatorScript.RunningInstance (true);
								GameManager.Scene = "Field 2";
								GameManager.EventNumber = 1;



							
							}


						}
						if (GameManager.Location == "CaveCutscene") {
							if (TriggerNumber == 2){
								PlayerController.vel.x = 0.0f;
								GameManager.Scene = "OntoCaveCutscene 4";
								StartCoroutine(DelayFadeIn());
							//	Debug.Log ("Bslkdjfklsf");

							}
						}

					

						if (OnGroundOnly == false) {
							if (DisableOnTrigger) {
								Destroy (gameObject);
								
							}
						}

					

					

					}
				
				}

			}
		


		}
	}

	void OnTriggerStay (Collider other)
	{
		if (GameManager.Location == "CaveCutscene") {
			
			if (other.tag == "Player") {
				if (isActive == true) {
					if (TriggerNumber == 1 && Player.GetComponent<CharacterController> ().isGrounded == true) {
						PlayerController.canControl = false;
				
								AnimatorScript.RunningInstance(true);
						//	GameManager.Scene = "CaveCutscene 4";
						//	GameManager.EventNumber = 1;
						GameManager.EventNumber = 0;
						GameManager.TextNumber = 0;
					//	BBoxScript.PlayToCutscene ();
					}
					if (OnGroundOnly == true) {
						if (Player.GetComponent<CharacterController> ().isGrounded == true) {
							if (DisableOnTrigger) {
								Destroy (gameObject);
						
							}
						}
				
					}
				}
			}
			
		}

	}

	void OnTriggerExit (Collider other)
	{
		if (GameManager.EnableTriggers == true) {
			if (other.tag == "Player") {


				if (CameraEventTrigger) {
				
				} else if (TextEventTrigger) {
				
				}

			}
		}
	}

	void DisableForCutScene ()
	{
		GameManager.CutsceneEnabled = true;
		ControllerScript.ClearAllLists ();
		PlayerController.canControl = false;
	}

	IEnumerator DelayFadeIn (){

		yield return new WaitForSeconds (1.5f);
		ManagerScript.CutsceneFadeIn("Black");

		if (DisableOnEvent == "Fade") {
			Destroy (gameObject);

		}

	}

	IEnumerator PauseAndOpenTextBox (string text, bool left, string portrait)
	{
		yield return new WaitForSeconds (1.0f);


		//	BBScript.ChangeText ("Oh ho, so you've arrived!");

//		BlackBoxScript.SpeedAble = true;
		//	BBoxScript.SwitchBoxDirection ();
		BBoxScript.Slide (text);

		if (left == true) {
			BBoxScript.setLeftPictureName (portrait);
		} else {
			BBoxScript.setRightPictureName (portrait);
			BBoxScript.SwitchBoxDirection ();
			//Debug.Log (left);
		}



		
		
	}


}

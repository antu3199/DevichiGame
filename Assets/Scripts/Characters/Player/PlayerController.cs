using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{


	public static bool canControl = true;
	bool TimeControl = false;
	public static bool DisableTimeControl = false;
	//Used only as an exception for levels with Disabled Time control
	public static bool AutomatedTimeControl = false;
	public static Vector2 vel;


	//Returns if wall climbing
	public static bool isWallClimbing = false;
	public static Vector2 ExternalForce = Vector2.zero;
	public static bool isFlinching = false;

	public float WallJumpStrength;
	float WallJumpCounter = 0.0f;
	public float ttime = 1.0f;
	public float jumpHeight = 8.0f;
	public float walkSpeed;
	AudioSource audioer;
	public AudioSource testere;
	public GameObject VisionPrefab;
	//GameObject VisionGameObject;
	GameObject Vision;
	public AudioClip Vision1;
	public AudioClip Vision2;
	public AudioClip Vision3;
	public AudioClip Vision4;
	List<Vector3> myList = new List<Vector3> ();
	List<bool> isFlinchingList = new List<bool> ();
	List<Vector2> VelocityList = new List<Vector2> ();

	List<bool> IsGroundedList = new List<bool> ();

	List<Vector2> ExternalForceList = new List<Vector2> ();


	private CharacterController controller;
	private float TimeSpeed = 1.0f;
	private float jumpCounter = 0.0f;
	float initTime = 0.0f;
	bool DifferentQuote = false;
	private bool TimeControlGrounded = false;
	public static	Vector2 SpawnPoint = Vector2.zero;
	GameObject BlackBox;
	BlackBoxScript BBScript;
	float test = 0.0f;
	// Use this for initialization
	float j = 0.0f;
	int k = 0;
	bool Attack2Bounce = false;

	bool AutomatedTimer = false;

	GameObject Emoticon;

	void Awake ()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;

		Screen.SetResolution (640, 480, true);
		Screen.SetResolution (1024, 768, true);

		float xFactor = Screen.width / 1024f;
		float yFactor = Screen.height / 768;
		
		ExternalForce = Vector2.zero;
		Camera.main.rect = new Rect (0, 0, 1, xFactor / yFactor);

		isWallClimbing = false;

		canControl = true;
		TimeControl = false;
		DisableTimeControl = false;
		vel = Vector3.zero;
		SpawnPoint = Vector2.zero;
		isFlinching = false;

	}

	public bool getTimeControl ()
	{
		return TimeControl;
	}

	void Start ()
	{

		controller = GetComponent<CharacterController> ();
		PlayerPrefs.SetString ("savedLevel", Application.loadedLevelName);
		//	if (DisableTimeControl == false) {
		Vision = Instantiate (VisionPrefab, new Vector3 (0.5f, 0.5f, 0.0f), Quaternion.identity) as GameObject; 
		//Vision
		Vision.transform.SetParent (GameObject.Find ("Main Camera").transform);


		Vision.GetComponent<SpriteRenderer> ().enabled = false;
		Transform[] children = Vision.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		//	}

	
		BlackBox = GameObject.FindGameObjectWithTag ("BlackBox");
		BBScript = BlackBox.GetComponent<BlackBoxScript> ();

		audioer = GetComponent<AudioSource> ();
		controller.detectCollisions = false;

		j = Time.time;
		Emoticon = gameObject.transform.FindChild ("Emoticon").gameObject;
		Emoticon.GetComponent<SpriteRenderer> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Vision.transform.localPosition = new Vector3 (0.0f, 0.0f, 10.0f);
		//	Debug.Log (vel.y);
	
		//Debug.Log (isWallSlide);
//		Debug.Log (Time.timeScale);
		if (TimeControl == false) {
			Time.timeScale = ttime;
		}

		//Debug.Log (1.0f / Time.deltaTime);
		//	Debug.Log (myList.Count);
		test += 0.01f;
//		Debug.Log (test);
		//Same as controller.isGrounded
		if (playerHealth.Dead) {
			canControl = false;
		}

	
		if (GameManager.RealCutscene == false){
		//Gravity
		if (!controller.isGrounded && TimeControl == false ) {
			
			vel.y -= Time.deltaTime * 80;
			if (isWallClimbing == true) {
				vel.y += Time.deltaTime * 70;
			}

			jumpCounter += Time.deltaTime;

			if (Attack2Bounce == true && jumpCounter >= 0.2f) {
				Attack2Bounce = false;
			}

		} else {
			//if (playerHealth.Dead == false) {
				//Ensures that player is grounded
				if (isFlinching == false){

				vel.y = -1;
				}
		//	} else {
				//If player dead, put vel.y = 0
			//	vel.y = 0.0f;
			//}
			jumpCounter = 0.0f;

			//Stops attackbounce from attack2
			Attack2Bounce = false;
		}


		}
		if (GameManager.CutsceneEnabled == false) {
		

			//TIME CONTROL KEYSTROKES------------------
			if (DisableTimeControl == false && AutomatedTimeControl == false) {

		
				//A Vision! Key Start
				if (Input.GetKeyDown (KeyCode.Space) && TimeControl == false && myList.Count > 0 && DisableTimeControl == false) {
					initTime = Time.time;
//			Debug.Log (initTime);
					/*
			//Note: 0 is inclusive, 4 is exclusive
			int RandomSound = Random.Range (2,4);
			if (RandomSound == 0){
				audioer.PlayOneShot(Vision1, 0.5f);
			}
			else if (RandomSound == 1){
				audioer.PlayOneShot(Vision2, 0.5f);
			}
			else if (RandomSound == 2){
				audioer.PlayOneShot(Vision3, 0.5f);
			}
			else if (RandomSound == 3){
				audioer.PlayOneShot(Vision4, 0.5f);
			}
			*/

					if (DifferentQuote == false) {
						DifferentQuote = true;
						audioer.PlayOneShot (Vision4, 0.5f);
					} else if (DifferentQuote == true) {
						DifferentQuote = false;
						audioer.PlayOneShot (Vision3, 0.5f);
					}
		
			
				}


				//TimeControl Keys
				//OnKeyHold
				if (Input.GetKey (KeyCode.Space) && TimeControl == false && myList.Count > 20 && DisableTimeControl == false) {
					//Time.timeScale = -1.0f;
					canControl = false;
					TimeControl = true;
					Vision.GetComponent<SpriteRenderer> ().enabled = true;

					Transform[] children = Vision.GetComponentsInChildren<Transform> ();
					for (int i = 0; i < children.Length; ++i) {
						
						//if (transform.GetChild (i).childCount > 0){
						if (children [i].gameObject.GetComponent<SpriteRenderer> () != null && children [i].gameObject.name != "GodlyTextbox") {
							children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
							//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
						}
					}

					Time.timeScale = 1.0f;
					TimeSpeed = 0.3f;

				}
		
				//OnKeyUp
				if (Input.GetKeyUp (KeyCode.Space) && TimeControl == true && DisableTimeControl == false) {
					//Time.timeScale = -1.0f;
					TimeControl = false;
					canControl = true;
					Vision.GetComponent<SpriteRenderer> ().enabled = false;

					/*
					Transform[] children = Vision.GetComponentsInChildren<Transform> ();
					for (int i = 0; i < children.Length; ++i) {
						
						//if (transform.GetChild (i).childCount > 0){
						if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
							children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
							//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
						}
					}
*/
					TimeSpeed = 1.0f;
					Time.timeScale = 1.0f;
				}
				if (Input.GetKey (KeyCode.K)){
					TimeControlTalk ();

				}
				if (Input.GetKey (KeyCode.L)){

					GameManager.TimeControlTalk = false;
					Vision.GetComponent<SpriteRenderer>().enabled = false;
				}
		
			}
			//END OF TIME CONTROL KEYSTROKES --------------------------------------
		
//			Debug.Log (ExternalForce.x);
			
		
			//Functions if CanControl
			if (canControl && isFlinching == false) {



				
			
				if (WallJumpCounter > 0) {
					WallJumpCounter ++;
				}



				//INPUT - 
				#if UNITY_STANDALONE || UNITY_WEBPLAYER
				//Directional Movement (Note that attacks are in PlayerAnimator for convenience)
			if(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("left") || Input.GetKey("right")){
				if(Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)){
					
						//if (vel.x != -walkSpeed){
						if (transform.localScale.x == 1.0f){
							transform.localScale = new Vector3 (-1.0f,transform.localScale.y,transform.localScale.z);
						}
						//Move forward more after wall jumping
						if ( ExternalForce.x < 0.0f ){
							vel.x = -walkSpeed + (ExternalForce.x);
							ExternalForce.x -= 12.0f * Time.deltaTime;

						}
						//Move normally
						    else if (ExternalForce.x == 0.0f ){
							vel.x = -walkSpeed;
						}
						else{
							//Cannot move other direction due to momentum
							vel.x = 0.0f;
						}

						//Wall Climbing ACTIVATE!
						if (WallClimbScript.isTouching == true && WallClimbScript.ClimbTouchDirection == -1 && ExternalForce.x <= 0){

							//Called only once for wall climbing
							if (isWallClimbing == false){
								//Resets able to jump again
								vel.y = 0.0f;
								jumpCounter = 0.0f;

							}
							ExternalForce.x = 0.0f;
							isWallClimbing = true;
			

						}
						else{
							isWallClimbing = false;
						}

				}
				if(Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)){

						if (transform.localScale.x == -1.0f){
							transform.localScale = new Vector3 (1.0f,transform.localScale.y,transform.localScale.z);
						}

						//Move forward more after wall jumping
						if ( ExternalForce.x > 0){

							vel.x = walkSpeed + (ExternalForce.x);
							ExternalForce.x += 12.0f * Time.deltaTime;
						}

					//MOve normally
						    else if (ExternalForce.x == 0 ){
						vel.x = walkSpeed; //+ ExternalForce.x;
						}
						//Cannot move due to momentum
						else{ 
							vel.x = 0.0f;
						}
					

						//Wall Climbing ACTIVATE
						if (WallClimbScript.isTouching == true && WallClimbScript.ClimbTouchDirection == 1 && ExternalForce.x >= 0){

							//Initial Wall Climbing thing
							if (isWallClimbing == false){
								vel.y = 0.0f;
								jumpCounter = 0.0f;
							}
							ExternalForce.x = 0.0f;

						//	WallJumpCounter = 2.0f;
							isWallClimbing = true;
					
						}
						else{
							isWallClimbing = false;
						}
				
				}
			}else{
				vel.x = 0;
			}
			


				//Disable Wall climbing on Key up
				if(Input.GetKeyUp("a") || Input.GetKeyUp(KeyCode.LeftArrow)){
					if (isWallClimbing == true && WallClimbScript.isTouching && WallClimbScript.ClimbTouchDirection == -1){
						isWallClimbing = false;
					}
				}
				
				if(Input.GetKeyUp("d") || Input.GetKeyUp(KeyCode.RightArrow)){
					if (isWallClimbing == true && WallClimbScript.isTouching && WallClimbScript.ClimbTouchDirection == 1){
						isWallClimbing = false;
					}


				}


				// Jump
				//Normal jump
				if(Input.GetKey("w") && isWallClimbing == false && controller.isGrounded || Input.GetKey(KeyCode.UpArrow) && isWallClimbing == false && controller.isGrounded){
				if(jumpCounter < 0.125f){
					vel.y = jumpHeight;
					jumpCounter = 0.125f;
					//GetComponent<AudioSource>().PlayOneShot(jumpSound);
				}
			}

				//Jump while wall climning
				if(Input.GetKeyDown("w") && isWallClimbing == true || Input.GetKeyDown(KeyCode.UpArrow) && isWallClimbing == true){
		
						vel.y = jumpHeight;
						jumpCounter = 0.125f;
						//GetComponent<AudioSource>().PlayOneShot(jumpSound);

						if (WallClimbScript.ClimbTouchDirection == 1){
							//Go left
		
							ExternalForce.x = -WallJumpStrength;
						}
						else{
						//Go Right
						ExternalForce.x = WallJumpStrength;
						}

					//}
				}
				#endif
			
				//Stop moving on Attack1
				if (PlayerAnimator.isAttack1ing == true) {
					vel = Vector2.zero;
				}
			}
		}


		if (TimeControl != true) {
			//vel.x = 8.0f;
			//Controller Movement
			if (playerHealth.Dead == false){
			vel.x += ExternalForce.x;
			vel.y += ExternalForce.y;
			
				if (ExternalForce.x > 0.0f) {
					
					if (!controller.isGrounded) {
						if (ExternalForce.x - 13.0f * Time.deltaTime > 0.0f) {
							ExternalForce.x -= 13.0f * Time.deltaTime;	
						} else {
							ExternalForce.x = 0.0f;
						}
					} else {
						
						ExternalForce.x = 0.0f;
						
					}
					
				} else if (ExternalForce.x < 0.0f) {
					if (!controller.isGrounded) {
						if (ExternalForce.x + 13.0f * Time.deltaTime < 0.0f) {
							ExternalForce.x += 13.0f * Time.deltaTime;
						} else {
							ExternalForce.x = 0.0f;
						}
					} else {
						
						ExternalForce.x = 0.0f;
						
					}
				}

			if (ExternalForce.y > 0.0f) {
				
				//if (!controller.isGrounded) {
					if (ExternalForce.y - 13.0f * Time.deltaTime > 0.0f) {
						ExternalForce.y -= 13.0f * Time.deltaTime;	
					} else {
						ExternalForce.y = 0.0f;
					}
			//	} else {
					
				//	ExternalForce.y = 0.0f;
					
			//	}
				
			}
			}
			controller.Move (vel * Time.deltaTime);
			if (isFlinching == true) {
				if (isWallClimbing == true ){
					isWallClimbing = false;
				}
				//Debug.Log (vel + " " + ExternalForce);
				//vel = Vector2.zero;
				if (controller.isGrounded == true){



					vel = Vector2.zero;
					ExternalForce = Vector2.zero;
					isFlinching = false;
				}
			}

		}
	
	}

	void FixedUpdate ()
	{
		if (Time.time > j + 1) {
			j = Time.time;

			k = 0;
			
		} else {
			k ++;
		}


		if (GameManager.CutsceneEnabled == false) {

			isGroundedTimeControl (IsGroundedList);
			ExternalForceTimeControl(ExternalForceList);
			FlinchTimeControl(isFlinchingList);
			VelocityTimeControl (VelocityList);
			//Position and central time control list
			if (TimeControl == false) {
				myList.Add (transform.position);
			
			}
		
			if (TimeControl == true) {

				if (AutomatedTimeControl == true && TimeControlGrounded == true && AutomatedTimer == false) { //&& transform.position.x <= SpawnPoint.x){
					AutomatedTimeControl = false;
					vel.x = 0.0f;
					TimeControl = false;
					canControl = true;
					Vision.GetComponent<SpriteRenderer> ().enabled = false;
					TimeSpeed = 1.0f;
					Time.timeScale = 1.0f;
					
					if (GameManager.Scene == "Field 2" && GameManager.EventNumber == 1 && AutomatedTimer == false) {
						canControl = false;
						GameManager.CutsceneEnabled = true;
						BBScript.setLeftPictureName ("Devichi");
						GameManager.EventNumber ++;
						BBScript.Slide ("...!!! I thought I was a goner... What was that?");
						
					}

				}


				transform.position = myList [myList.Count - 1];
				myList.RemoveAt (myList.Count - 1);
				if (AutomatedTimeControl == false) {
					if (TimeSpeed < 3.0f) {

			
						if (Time.time > initTime + 3.0f) {
							TimeSpeed += 0.02f;
						}
						if (TimeSpeed < 1.0f) {
							TimeSpeed += 0.01f;
						}

					}
					else{
						TimeSpeed += 0.001f;
					}
					Time.timeScale = TimeSpeed;
				}
				if (myList.Count == 0) {
					TimeControl = false;
					canControl = true;
					Vision.GetComponent<SpriteRenderer> ().enabled = false;
					Time.timeScale = 1.0f;
					TimeSpeed = 1.0f;
				}

			}

			//Velocity Time control Lists
		//	
		}

	}






	//FUNCTIONS TO BE CALLED FROM OTHER CLASSSES - - -

	//CLEAR ALL LISTS
	public void ClearAllLists ()
	{
		for (int i = 0; i < myList.Count; i++) {
			myList.RemoveAt (i);
		}
		for (int i = 0; i < VelocityList.Count; i++) {
			VelocityList.RemoveAt (i);
		}
		GetComponent<PlayerAnimator> ().ClearAnimatorLists ();
	}
	
	//Automatic Animation functions
	public void Idle ()
	{
		vel = new Vector2 (0.0f, vel.y);
	}

	public void Walk (bool right)
	{

		if (right == true) {
			vel.x = walkSpeed;
		} else {
			vel.x = -walkSpeed;
		}
	}

	public void EnableAutomatedTimeControl ( bool timer)
	{
		AutomatedTimeControl = true;
		canControl = false;
		TimeControl = true;
		Vision.GetComponent<SpriteRenderer> ().enabled = true;
		Transform[] children = Vision.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null && children [i].gameObject.name != "GodlyTextbox") {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}


		Time.timeScale = 1.0f;
		TimeSpeed = 1.0f;

		if (DifferentQuote == false) {
			DifferentQuote = true;
			audioer.PlayOneShot (Vision4, 0.5f);
		} else if (DifferentQuote == true) {
			DifferentQuote = false;
			audioer.PlayOneShot (Vision3, 0.5f);
		}

		if (timer == false) {
			AutomatedTimer = false;

		} else {
			AutomatedTimer = true;
			StartCoroutine(EndAutoMatedTimeControlByTime());
		}


	}
	IEnumerator EndAutoMatedTimeControlByTime(){
		yield return new WaitForSeconds (3.0f);

		AutomatedTimer = false;
		/*
		AutomatedTimeControl = false;
		vel.x = 0.0f;
		TimeControl = false;
		canControl = true;
		Vision.GetComponent<GUITexture> ().enabled = false;
		TimeSpeed = 1.0f;
		Time.timeScale = 1.0f;
		if (BBScript.getisOpen () == false) { 
			BBScript.setLeftPictureName ("Devichi");
			GameManager.EventNumber = 1;
			GameManager.Scene = "Field Died";
			BBScript.Slide ("...!?!?!? It happens during combat too...");
		}
		*/
	}

	public void Jump ()
	{
		vel.y = jumpHeight;
		jumpCounter = 0.125f;
		if (PlayerAnimator.isAttack2ing == true) {
			Attack2Bounce = true;
		}
	}

	public bool getAttack2Bounce ()
	{
		return Attack2Bounce;

	}

	//this will happen if a trigger collider hits it. we check the tag though so only objects tagged with pickup will make this happen. it upgrades the weapons on pickup.
	void OnTriggerEnter (Collider other)
	{

	}

	//ded
	void died ()
	{
		canControl = false;
		vel.x = 0;
	}
	
	//TIME CONTROL  FUNCTIONS ----
	void VelocityTimeControl (List<Vector2> list)
	{
		if (TimeControl == false) {
			list.Add (vel);
			
		} else if (TimeControl == true) {
			if (list.Count > 0) {
				if (isFlinching ){
					vel = list [list.Count - 1];
				}

				list.RemoveAt (list.Count - 1);
		
			}
			
		}
	}
	void FlinchTimeControl (List<bool> list)
	{
		if (TimeControl == false) {
			list.Add (isFlinching);
			
		} else if (TimeControl == true) {
			if (list.Count > 0) {
				isFlinching = list [list.Count - 1];
				list.RemoveAt (list.Count - 1);
				
			}
			
		}
	}
	void isGroundedTimeControl (List<bool> list)
	{
		if (TimeControl == false) {
			list.Add (controller.isGrounded);
			
		} else if (TimeControl == true) {
			if (list.Count > 0) {
				TimeControlGrounded = list [list.Count - 1];
				list.RemoveAt (list.Count - 1);
				
			}
			
		}
	}

	void ExternalForceTimeControl (List<Vector2> list)
	{
		if (TimeControl == false) {
			list.Add (ExternalForce);
			
		} else if (TimeControl == true) {
			if (list.Count > 0) {
				ExternalForce = list [list.Count - 1];
				list.RemoveAt (list.Count - 1);
				
			}
			
		}
	}

	public void TimeControlTalk (){
		GameManager.TimeControlTalk = true;
		BlueScript.AddEventNumber = true;
		if (DifferentQuote == false) {
			DifferentQuote = true;
			audioer.PlayOneShot (Vision4, 0.5f);
		} else if (DifferentQuote == true) {
			DifferentQuote = false;
			audioer.PlayOneShot (Vision3, 0.5f);
		}

		Vision.GetComponent<SpriteRenderer> ().enabled = true;
		
		Transform[] children = Vision.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}

	}

	
}

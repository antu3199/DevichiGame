using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//Time Animator class used to organize position, animation and frame. (Mostly for convenience. Not really necessary) 
public class TimeAnimator{
	public float direction;
	public float frame;
	public string animation = "idle";

	public TimeAnimator (float _direction, float _frame, string _animation){
		direction = _direction;
		frame = _frame;
		animation = _animation; 
	}




	public float getFrame(){
		return frame;
	}
	
	public void setFrame( int _frame){
		frame = _frame;
	}
	public string getAnimation ()
	{
		return animation;
	}

	public void setAnimation ( string _animation)
	{
		animation = _animation;
	}

}


public class PlayerAnimator : MonoBehaviour {
	//here we have all of the player's textures to animate him running and being idle. jumping just uses the idle texture.
	/*
	 *Sprite method
	public Sprite[] idle;
	public float idleFrameRate = 8.0f;
	public Sprite[] run;
	public float runFrameRate = 8.0f;
	public Sprite[] jump;
	public float jumpFrameRate = 8.0f;
	public float frameRate = 8;
*/
	public static bool PerformRegularAnimations = true;

	public float IdleSpeed = 1.0f;
	public float RunningSpeed = 1.0f;
	public float JumpingSpeed = 1.0f;
	public float Attack1ingSpeed = 1.0f;
	public float Attack2ingSpeed = 1.0f;

	bool isSitting = false;
	bool isSitToStand = false;
	bool isReleasingWeapon = false;

	//Lists
	List<TimeAnimator> myList = new List<TimeAnimator> ();
	List<int> Attack2JumpCounterList = new List<int> ();
	//here are some private variables we use to help animate the character.
	private CharacterController controller;
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	private bool Jumping = false;
	private bool isJumping = false;
	private float origX;

	string direction;
	string animationf;

	Animator MyAnimation;

	bool isIdle = false;
	bool isRunning = false;
	//bool isRunning = false;
	bool right = true;

	float AnimationFrame = 0;

	public AnimationState Test;
	float testerman = 0.0f;
	float j = 0.0f;
	float StartingTime = 0.0f;

	//Attack bools
	public static bool isAttacking = false;
	public static bool isAttack1ing = false;
	public static bool isAttack2ing = false;
	public static int  Attack2JumpCount = 0; 

	GameObject SlashWave;
	playerHealth PlayerHealthScript;
	//public GameObject Wave;
	//Transparency Trans;
	float normalTime = 0.0f;
//	public Animator tester;
	// Use this for initialization
	GameObject Player;
	PlayerController ControllerScript;



	void Awake(){
		isAttacking = false;
		isAttack1ing = false;
		isAttack2ing = false;
		Attack2JumpCount = 0;

		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
		MyAnimation = gameObject.transform.FindChild ("Animation").GetComponent<Animator> ();
	}

	void Start () {
		SlashWave = gameObject.transform.Find("Animation/Body/LArm/Weapon/SlashWave").gameObject;
		SlashWave.GetComponent<SpriteRenderer>().enabled = true;

		origX = 1;
		if (transform.localScale.x == 1) {
			direction = "right";
		} else {
			direction = "left";
		}
		//RunningInstance (true);

		//SittingInstance ();
		PlayerHealthScript = GetComponent<playerHealth> ();

		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();

	}
	//Counter is basically the time in frames
	//i is which frame (sprite) showing

	// Update is called once per frame
	void Update () {
	//	AnimatorStateInfo currentState = MyAnimation.GetCurrentAnimatorStateInfo(animationLayer);
	//	PerformRegularAnimations = false;
		//Debug.Log (direction);

		if (ControllerScript.getTimeControl() == false) {

			if (PlayerController.canControl == true && PlayerController.isFlinching == false){


				//Attacks ----------
			
				if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.C)){
					//Ground attack
					if (isAttacking == false && controller.isGrounded == true && GameManager.Scene != "CaveCutscene 3" ){

					animationf = "attack1";  
					
					if (isAttacking == false){
						StartingTime = Time.time;
					}
					
					
					StopAllExcept (isAttacking);
					
					isAttacking = true;
					isAttack1ing = true;
					
					MyAnimation.SetBool ("isAttacking", isAttacking);
					MyAnimation.SetBool ("isAttack1ing", isAttack1ing);


						MyAnimation.Play ("PlayerAttack1",0,0.0f);
						
					

					}
					SlashWave.GetComponent<SpriteRenderer>().enabled = true;
				//	Trans.resetTrans();
				//	Trans.setStartTime();
					SlashWave.GetComponent<BoxCollider> ().enabled = true;

				}
				if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C)  ){

					//Air attack
					if (isAttacking == false && controller.isGrounded == false && PlayerController.isWallClimbing == false && GameManager.Scene != "CaveCutscene 3"){
					
					animationf = "attack2";  
					
					if (isAttacking == false){
						StartingTime = Time.time;
					}
					
					
					StopAllExcept (isAttacking);
					
					isAttacking = true;
					isAttack2ing = true;
					
					MyAnimation.SetBool ("isAttacking", isAttacking);
					MyAnimation.SetBool ("isAttack2ing", isAttack2ing);
						MyAnimation.Play ("PlayerAttack2",0,0.0f);


						SlashWave.GetComponent<SpriteRenderer>().enabled = true;
						SlashWave.GetComponent<BoxCollider> ().enabled = true;
					//	Attack2JumpCount ++;

					//	Trans.resetTrans();
					//	Trans.setStartTime();
					}
				}
			}

	//	if (GameManager.CutsceneEnabled == false){

		
			//Gets character's velocity
			float xVelocity = controller.velocity.x;
//			float yVelocity = controller.velocity.y;
			if (xVelocity < 0) {
				xVelocity *= -1;
	
			} else if (xVelocity > 0) {

			}
//			Debug.Log (xVelocity);
			//Flip Player based on velocity
			/*
			if(controller.velocity.x > 0 && PlayerController.isFlinching == false){
				transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
				direction = "right";
			}
			if(controller.velocity.x < 0 && PlayerController.isFlinching == false){
				transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
				direction = "left";
			}
*/
			if (PerformRegularAnimations == true){

			if (xVelocity < 0.25f) {
				//Do Idle animation
				if (controller.isGrounded && isAttacking == false) {
				

					animationf = "idle";  
			
					if (isIdle == false){
						StartingTime = Time.time;
					}

					StopAllExcept (isIdle);
					isIdle = true;
					MyAnimation.SetBool ("isIdle", isIdle);
						MyAnimation.Play ("PlayerIdle",0,0.0f);
			
				}
			} else {
				//Do Running animation
				if (controller.isGrounded && isAttacking == false) {
					animationf = "run";
					if (isRunning == false){
						StartingTime = Time.time;
							MyAnimation.Play ("PlayerWalk",0,0.0f);
					}
					StopAllExcept (isRunning);
					isRunning = true;
					MyAnimation.SetBool ("isRunning", isRunning);

					

				}
			}

			if (!controller.isGrounded && isAttack1ing == false && isAttack2ing == false) {
				animationf = "jump";

				//Do Jump
			//	if (!Jumping && yVelocity > 0.5f) {
					Jumping = true;
					StartingTime = Time.time;
					StopAllExcept (isJumping);
					isJumping = true;
					MyAnimation.SetBool ("isJumping", isJumping);

					counter = 0.0f;
					i = 0;
						

			//	}
					if (Jumping == true && MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
						StartingTime = Time.time;
						MyAnimation.Play ("PlayerJump",0,0.0f);

					}
				/* Jumping animation sprite version, but I'm a filthy casual so I used the Unity animator 
				if (Jumping) {
					counter += Time.deltaTime * jumpFrameRate;
					if (counter > i && i < jump.Length) {
						rend.sprite = jump [i];
						i += 1;
					}
				}
				*/
			}
			}
			//Jump cancel
			if (controller.isGrounded) {
				Jumping = false;
			}
		 
			//}

		}

	

	}



	void FixedUpdate(){

		Attack2JumpCounterTimeControl (Attack2JumpCounterList);
		if (ControllerScript.getTimeControl()== false) {			
			//Play animations (Note: It has to be done in this funky style because I want it to scale properly when timeScale is slowed down, and for the reverse functions
			//Unrelated jump attack list (Can only air attack 3 times max
			if (GameManager.CutsceneEnabled == false && PlayerController.DisableTimeControl == false) {
				
				
			
			}
			if (isIdle) {
				animationf = "idle";
			
			}
			
			
			if (isRunning) {
				animationf = "run";
			
			}
			if (isJumping) {
				animationf = "jump";
		
			}
				
			//Attacking animations
			if (isAttacking) {
				//Attack1
				if (isAttack1ing){
					animationf = "attack1";
				

					if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f || PlayerController.isFlinching == true){
						StopAllExcept(isAttacking);
						isAttacking = false;
						isAttack1ing = false;
						
						MyAnimation.SetBool ("isAttacking", isAttacking);
						MyAnimation.SetBool ("isAttack1ing", isAttacking);

						isIdle = true;
						MyAnimation.SetBool ("isIdle", isIdle);


						if (PlayerController.isFlinching == true){
							Debug.Log ("testerr");
						}

					}
					
				}
				//Attack2
				if (isAttack2ing){

					animationf = "attack2";


			
					//Stop air attack 2
					if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && StartingTime + 0.05f < Time.time  || controller.isGrounded || PlayerController.isFlinching== true || PlayerController.isWallClimbing == true){
						//	if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f || controller.isGrounded){
	
						StopAllExcept(isAttacking);
						isAttacking = false;
						isAttack2ing = false;
						
						MyAnimation.SetBool ("isAttacking", isAttacking);
						MyAnimation.SetBool ("isAttack2ing", isAttacking);

						isIdle = true;
						MyAnimation.SetBool ("isIdle", isIdle);

//						Debug.Log (PlayerController.isWallClimbing);
						
					}
				}
				
			}
			//Reset Attack2JumpCount when on ground
			if (controller.isGrounded){
				Attack2JumpCount = 0;
			}
			normalTime = MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime;

			//Adds animation to list Note: they are all separated because of the different speeds
			if (GameManager.CutsceneEnabled == false && PlayerController.DisableTimeControl == false) {

				myList.Add (new TimeAnimator (gameObject.transform.localScale.x, normalTime, animationf));
			
			}
			//IF TIMECONTROL == TRUE
		} else {


			//Direction facing....
			if (myList.Count > 0){
				if (myList[myList.Count-1].direction == 1.0f){
					
					transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
				}
				else{
					transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
					
				}
				//Reset all animations 
				isIdle = false;
				MyAnimation.SetBool ("isIdle", isRunning);
				isRunning = false;
				MyAnimation.SetBool ("isRunning", isRunning);
				isJumping = false;
				MyAnimation.SetBool ("isJumping", isJumping);
				isAttacking = false;
				MyAnimation.SetBool ("isAttacking", isAttacking);
				isAttack1ing = false;
				MyAnimation.SetBool ("isAttack1ing", isAttack1ing);
				
				

				//Determine which animation to use based on list ----

				//Idle
				if (myList[myList.Count-1].getAnimation() == "idle"){
					
					StopAllExcept (isIdle);
					
					isIdle = true;
					MyAnimation.SetBool ("isIdle", isIdle);
					MyAnimation.Play ("PlayerIdle",0,myList[myList.Count-1].frame);
				}
				//Run
				else if (myList[myList.Count-1].getAnimation() == "run"){
					//		Animation ["PlayerWalk"].speed = -1.5f;
					//	Animation ["PlayerWalk"].time = myList[myList.Count-1].frame;
					StopAllExcept (isRunning);
					
					
					/*
				isIdle = false;
				MyAnimation.SetBool ("isIdle", isRunning);
				isRunning = false;
				MyAnimation.SetBool ("isRunning", isRunning);
				isJumping = false;
				MyAnimation.SetBool ("isJumping", isJumping);
*/
					
					isRunning = true;
					MyAnimation.SetBool ("isRunning", isRunning);
					MyAnimation.Play ("PlayerWalk",0,myList[myList.Count-1].frame);
				}
				//Jump
				else if (myList[myList.Count-1].getAnimation() == "jump"){
					//	Animation ["PlayerJump"].speed = -1.0f;
					//	Animation ["PlayerJump"].time = myList[myList.Count-1].frame;
					
					/*
				
				isIdle = false;
				MyAnimation.SetBool ("isIdle", isRunning);
				isRunning = false;
				MyAnimation.SetBool ("isRunning", isRunning);
				isJumping = false;
				MyAnimation.SetBool ("isJumping", isJumping);
*/
					
					StopAllExcept (isJumping);
					isJumping = true;
					MyAnimation.SetBool ("isJumping", isJumping);
					MyAnimation.Play ("PlayerJump",0,myList[myList.Count-1].frame);
				}
				//Attack
				else if (myList[myList.Count-1].getAnimation() == "attack1"){
					
					
					StopAllExcept (isAttacking);
					isAttacking = true;
					MyAnimation.SetBool ("isAttacking", isAttacking);
					isAttack1ing = true;
					MyAnimation.SetBool ("isAttack1ing", isAttack1ing);
					//MyAnimation.Play ("PlayerAttack1",0,(Time.time - StartingTime)*1.6f*AttackingSpeed);
					MyAnimation.Play ("PlayerAttack1",0,myList[myList.Count-1].frame);
				}
				//Attack2
				else if (myList[myList.Count-1].getAnimation() == "attack2"){
					
					
					StopAllExcept (isAttacking);
					isAttacking = true;
					MyAnimation.SetBool ("isAttacking", isAttacking);
					isAttack2ing = true;
					MyAnimation.SetBool ("isAttack2ing", isAttack2ing);
					//MyAnimation.Play ("PlayerAttack1",0,(Time.time - StartingTime)*1.6f*AttackingSpeed);
					MyAnimation.Play ("PlayerAttack2",0,myList[myList.Count-1].frame);
				}
				
				//Remove last item in list.
				myList.RemoveAt (myList.Count-1);
			}
		} 
		
			

		//Cutscenestuff;
		if (GameManager.Location == "Field") {

			if (GameManager.Scene == "Field 1"){

				if (GameManager.EventNumber == 19){
					StartingTime = Time.time;
					GameManager.EventNumber ++;

				}
				else if (GameManager.EventNumber == 20){

					if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && StartingTime + 0.05f < Time.time){
						//Debug.Log ("TESTERRRRRR");
						WeaponReleaseInstance();
						StartingTime = Time.time;
						GameManager.EventNumber ++;
						//	Debug.Log (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime);
						
					}
				
				}
				else if (GameManager.EventNumber == 21){
					if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && StartingTime + 0.05f < Time.time){
						PlayerController.canControl = true;
						PerformRegularAnimations = true;
						GameManager.CutsceneEnabled = false;

						CameraSmooth.SmoothFollow = true;
						GameManager.EventNumber ++;
						isReleasingWeapon = false;
						MyAnimation.SetBool ("ReleaseWeapon", isReleasingWeapon);
						GameManager.Scene = "";
						GameManager.EventNumber = -1;
						PlayerHealthScript.ShowHearts();

					}
				}

			}

		}

	}




	//Clears animation lists
	public void ClearAnimatorLists(){
		for (int i = 0; i < myList.Count; i++) {
			myList.RemoveAt(i);
		}
		for (int i = 0; i < Attack2JumpCounterList.Count; i++) {
			Attack2JumpCounterList.RemoveAt(i);
		}
		GetComponent<playerHealth> ().ClearHealthLists ();

		//List<int> Attack2JumpCounterList = new List<int> ();

	}

	//Instances used for cutscene animation ---------
	public void IdleInstance(){
		GetComponent<PlayerController> ().Idle ();


		animationf = "idle";  
		
		if (isIdle == false){
			StartingTime = Time.time;
		}
		StopAllExcept (isIdle);
		isIdle = true;
		MyAnimation.SetBool ("isIdle", isIdle);
		MyAnimation.Play ("PlayerIdle",0,0.0f);

	}

	public void RunningInstance(bool right){
		GetComponent<PlayerController> ().Walk (right);

		animationf = "run";

		if (isRunning == false){
			StartingTime = Time.time;
		}
		
	StopAllExcept (isRunning);
		isRunning = true;
		
		MyAnimation.SetBool ("isRunning", isRunning);

		MyAnimation.Play ("PlayerWalk",0,0.0f);


	}
	public void JumpingInstance(){
		GetComponent<PlayerController> ().Jump ();
		Jumping = true;
		StartingTime = Time.time;
		StopAllExcept (isJumping);
		isJumping = true;
		MyAnimation.SetBool ("isJumping", isJumping);
		MyAnimation.Play ("PlayerJump",0,0.0f);

	}

	public void SittingInstance(){
		isSitting = true;

			MyAnimation.SetBool ("isSitting", isSitting);

	
	
	}
	public void SitToStandInstance(){
		isSitting = false;
		MyAnimation.SetBool ("isSitting", isSitting);
		isSitToStand = true;
		MyAnimation.SetBool ("SittingToStand", isSitToStand);
	}
	public void WeaponReleaseInstance(){
		isSitToStand = false;
		MyAnimation.SetBool ("SittingToStand", isSitToStand);
		isReleasingWeapon = true;
		MyAnimation.SetBool ("ReleaseWeapon", isReleasingWeapon);
	}

	//Flips chracter based on direction
	void Flip ()
	{
		if (direction == "left") {
			direction = "right";
		} else {
			direction = "left";
		}

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	private void StopAllExcept (bool me){

		isIdle = false;
		MyAnimation.SetBool ("isIdle", isIdle);
		isRunning = false;
		MyAnimation.SetBool ("isRunning", isRunning);
		isJumping = false;
		MyAnimation.SetBool ("isJumping", isJumping);

		isAttacking = false;
		MyAnimation.SetBool ("isAttacking", isAttacking);
		isAttack1ing = false;
		MyAnimation.SetBool ("isAttack1ing", isAttack1ing);
		isAttack2ing = false;
		MyAnimation.SetBool ("isAttack2ing", isAttack1ing);

		me = true;

	}
	private void Attack2JumpCounterTimeControl (List<int> list){
		if (ControllerScript.getTimeControl() == false) {
			list.Add (Attack2JumpCount);
		} else {
			Attack2JumpCount = list[list.Count-1];
			list.RemoveAt(list.Count-1);
		}
	}

}

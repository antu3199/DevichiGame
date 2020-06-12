using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LeafFlyingController : MonoBehaviour {
	CharacterController controller;
	List<EnemyTimeAnimator> myList = new List<EnemyTimeAnimator> ();
	Vector2 vel;
	private float jumpCounter = 0.0f;
	public float runSpeed;
	float counter = 0;
	float origX = 1.0f;
	bool CanControl = true;
	bool isFlinching = false;
	bool Attacking = false;
	bool right = false;
	GameObject target;
	GameObject Devilchu;
	bool isIdling = true;
	bool isMoving = false;
	bool isMovingDelay = false;
	bool isAttacking = false;
	float Counter = 0.0f;
	Animator MyAnimation;
	string State ;
	string Animation;
	float DelayTime;
	float ThisTime ;
	float normalTime ;
	public float RunningDelay = 1.0f;
	GameObject SlashWave;
	enemyHealth HealthScript;
	GameObject Player;
	PlayerController ControllerScript;
	playerHealth PHealthScript;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		PHealthScript = Player.GetComponent<playerHealth> ();
		
		controller = GetComponent<CharacterController> ();
		controller.detectCollisions = false;
		
		target = GameObject.FindGameObjectWithTag ("Player");
		//Physics.IgnoreCollision (target.GetComponent<Collider> (), GetComponent<Collider> ());
		Physics.IgnoreCollision (target.transform.root.GetComponent<Collider> (), GetComponent<Collider> ());
		
		
		Devilchu = GameObject.FindGameObjectWithTag ("Devilchu");
		Physics.IgnoreCollision (Devilchu.transform.root.GetComponent<Collider> (), GetComponent<Collider> ());
		
		MyAnimation = gameObject.transform.FindChild ("Animation").GetComponent<Animator> ();
		
		//SlashWave = gameObject.transform.FindChild ("Animation/LeafBody/LeafLLeg/SlashWave").gameObject;
		//SlashWave.GetComponent<BoxCollider> ().enabled = false;
		//SlashWave.GetComponent<SpriteRenderer> ().enabled = false;
		
		HealthScript = gameObject.GetComponent<enemyHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2)/75,transform.position.z);

		if (ControllerScript.getTimeControl () == false) {
			if (HealthScript.getDead () == false) {
				//Gravity
				/*
				if (!controller.isGrounded) {
					
					vel.y -= Time.deltaTime * 80;
					jumpCounter += Time.deltaTime;
				} else {
					vel.y = -1;
					jumpCounter = 0.0f;
				}
				*/
				if (GameManager.CutsceneEnabled == false) {
					
					
					if (isFlinching == true && vel.y < 0 && controller.isGrounded) {
						vel = Vector2.zero;
						CanControl = true;
						isFlinching = false;
					}
					
					if (CanControl == true) {
						//here we check the distance away the player is from the enemy
						float distance = target.transform.position.x - transform.position.x;
						
						float ydistance = target.transform.position.y - transform.position.y;
						if (distance < 0) {
							distance *= -1;
						}
						if (ydistance < 0) {
							ydistance *= -1;
						}
						
						
						if (target.transform.position.x > transform.position.x && transform.localScale.x == -1.0f && isAttacking == false) {
							State = "Idle";
							//StartMoving = false;
							StopAllExcept (isIdling);
							isIdling = true;
							MyAnimation.SetBool ("isIdle", isIdling);
							
							transform.localScale = new Vector3 (origX, transform.localScale.y, transform.localScale.z);
							right = true;
							
							
						}
						if (target.transform.position.x < transform.position.x && transform.localScale.x == 1.0f && isAttacking == false) {
							State = "Idle";
							//StartMoving = false;
							StopAllExcept (isIdling);
							isIdling = true;
							MyAnimation.SetBool ("isIdle", isIdling);
							
							
							transform.localScale = new Vector3 (-origX, transform.localScale.y, transform.localScale.z);
							right = false;
							
						}
						
						if (controller.isGrounded && vel.x == 0.0f) {
							
							///	StopAllExcept(isIdling);
							//	isIdling = true;
							
						}
						
						if (controller.isGrounded && isFlinching == false && isIdling == true) {
							vel = new Vector2 (0.0f, vel.y);
						}
						
						//		if (HealthScript.getDead () == false && playerHealth.Dead == false) {
						
						
						
						
						//if the player is close enough. the enemy can start animating and doing its thing.
						if (distance < 16 && ydistance < 8) {
							
							counter += Time.deltaTime;
							
							
						//	Debug.Log (ydistance);
							
							if (distance >= 13 && isAttacking == false) {

								State = "Idle";
								StopAllExcept (isIdling);
								isIdling = true;
								MyAnimation.SetBool ("isIdle", isIdling);

							}
							
							
							if (distance < 4 && ydistance < 1.2f && State != "Attacking") {

								StopAllExcept (isIdling);
								MyAnimation.SetBool ("isIdle", true);
								
								State = "Attacking";
								DelayTime = Time.time;
								
							}
							if (State == "Attacking") {
								ThisTime = Time.time;
							}
							if (State == "Attacking" && ThisTime > DelayTime + 1.0f && isAttacking == false) {
								StopAllExcept (isAttacking);
								//MyAnimation.SetBool("isIdle",true);
								isAttacking = true;
								MyAnimation.SetBool ("isAttacking", true);
								MyAnimation.Play ("Attack", 0, 0.0f);
								
							//	SlashWave.GetComponent<BoxCollider> ().enabled = true;
							//	SlashWave.GetComponent<SpriteRenderer> ().enabled = true;
								
							}
							
							
							if (isAttacking == true) {
								normalTime = MyAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime;
								
								if (MyAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) {  
									
									if (distance >= 4 || ydistance > 1.2f || target.transform.position.x > transform.position.x && transform.localScale.x == -1.0f  ||target.transform.position.x < transform.position.x && transform.localScale.x == 1.0f  ) {
									//	Debug.Log(distance + " " + ydistance);

										State = "Idle";
										StopAllExcept (isIdling);
										isIdling = true;
										isAttacking =false;
										MyAnimation.SetBool ("isIdle", isIdling);
									}
									else{
										MyAnimation.Play ("Attack", 0, 0.0f);
									}
									
								}
							}
							
							
							
							//MOVING
							if (distance < 13 && isAttacking == false && State != "Attacking" && State != "Moving") {
								
								StopAllExcept (isIdling);
								MyAnimation.SetBool ("isIdle", true);
								State = "Moving";
								
								DelayTime = Time.time;
								
							}
							
							
							if (State == "Moving") {
								ThisTime = Time.time;
							}
							
							if (State == "Moving" && ThisTime > DelayTime + 1.0f && isMoving == false) {
								StopAllExcept (isMoving);
								//MyAnimation.SetBool("isIdle",true);
								isMoving = true;
								MyAnimation.SetBool ("isMoving", isMoving);
							}
							
							if (isMoving == true && MyAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) {
								MyAnimation.Play ("Move", 0, 0.0f);
							}
							
							
							
							if (isMoving == true && isAttacking == false && State != "Attacking") {
								normalTime = MyAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime;
								
								if (target.transform.position.x < transform.position.x) {
									vel.x = -runSpeed;
								}
								if (target.transform.position.x > transform.position.x) {
									vel.x = runSpeed;
								}
								if (target.transform.position.y + 1.2f < transform.position.y) {
									vel.y = -2.0f;
								}
								if (target.transform.position.y + 1.2f  > transform.position.y) {
									vel.y =  2.0f;
								}


							} else {
								vel.x = 0.0f;
								vel.y = 0.0f;
							}
							
							if (isIdling) {
								normalTime = MyAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime;
								if (normalTime >= 1.0f){
									MyAnimation.Play ("Idle", 0, 0.0f);
								}
							}
							
							//else{
							//StopAllExcept(isIdling);
							//isIdling = true;
							//isMoving = false;
							
							//}
							
							
							//					Debug.Log (distance);
							if (distance < 2.5f && ydistance < 2.5f) {
								
								if (!isFlinching) {
									
								}
								
								
							}
							if (distance > 9 && distance < 16 && !Attacking) {
								
								if (target.transform.position.x < transform.position.x) {
									//	vel.x = -runSpeed;
									//Debug.Log (controller.isGrounded );
								}
								if (target.transform.position.x > transform.position.x) {
									//	vel.x = runSpeed;
								}
								//	if (counter > i && i < run.Length) {
								//		rend.sprite = run [i];
								//		i += 1;
								//	}
								//	if (counter > run.Length) {
								//		counter = 0.0f;
								//		i = 0;
								//	}
							}
							if (Attacking) {
								
								//	vel.x = 0;
								//	if (counter > i && i < shoot.Length) {
								//		rend.sprite = shoot [i];
								//		i += 1;
								//	}
							}
							
							
							
							//the enemy will shoot a bullet only if the player is close enough and shooting is indeed false.
							//	if (distance < 9 && Attacking == false && HealthScript.getDead () == false) {
							
							//ShowBullet();
							//		StartCoroutine (shootBullet ());
							
							
							//	}
							//	}
						}
						
					}
					
				}
				
				
				
				
				controller.Move (vel * Time.deltaTime);
			} else {
				State = "";
				StopAllExcept (isIdling);
				isIdling = false;
				MyAnimation.SetBool ("isIdle", isIdling);
				MyAnimation.Play ("Idle", 0, 0.0f);
				
			}
			
		}




	}

	void FixedUpdate(){

		
		if (ControllerScript.getTimeControl () == false) {
			
			if (isIdling == true) {
				Animation = "isIdle";
			} else if (isMoving == true) {
				Animation = "isMoving";
			} else if (isAttacking == true) {
				Animation = "isAttacking";
			}
			
			myList.Add (new EnemyTimeAnimator (transform.position, transform.localScale.x, normalTime, State, DelayTime, ThisTime, Animation));
		} else {
			
			if (myList.Count > 0) {
				
				
				
				
				transform.position = myList [myList.Count - 1].Position;
				
				if (myList [myList.Count - 1].direction == 1.0f) {
					transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
					
				} else {
					transform.localScale = new Vector3 (-1.0f, 1.0f, 1.0f);
				}
				
				StopAllExcept (isIdling);
				isIdling = false;
				
				MyAnimation.Play ("Idle", 0, 0.0f);
				
				if (myList [myList.Count - 1].animation == "isIdle") {
					isIdling = true;
					MyAnimation.Play ("Idle", 0, 0.0f);
				}
				
				
				if (myList [myList.Count - 1].state == "Move") {
					State = "Move";
					ThisTime = myList [myList.Count - 1].Thistime;
					DelayTime = myList [myList.Count - 1].delaytime;
				}
				
				//	if (myList[myList.Count-1].animation != null){
				//	Debug.Log (myList[myList.Count-1].animation);
				if (myList [myList.Count - 1].animation == "isMoving") {
					isMoving = true;
					MyAnimation.Play ("Move", 0, myList [myList.Count - 1].normalizedtime);
				}
				//	}
				if (myList [myList.Count - 1].state == "Attacking") {
					State = "Attacking";
					ThisTime = myList [myList.Count - 1].Thistime;
					DelayTime = myList [myList.Count - 1].delaytime;
					
				}
				
				if (myList [myList.Count - 1].animation == "isAttacking") {
					isAttacking = true;
					MyAnimation.Play ("Attack", 0, myList [myList.Count - 1].normalizedtime);
					
				}
				
				
				myList.RemoveAt (myList.Count - 1);
				
				
				
				
				
			}
			
			
			
		}

	}


	private void StopAllExcept (bool me)
	{
		
		isIdling = false;
		MyAnimation.SetBool ("isIdle", isIdling);
		
		
	//	isMovingDelay = false;
//		MyAnimation.SetBool ("isMovingDelay", isMovingDelay);
		
		isMoving = false;
		MyAnimation.SetBool ("isMoving", isMoving);
		
		isAttacking = false;
		MyAnimation.SetBool ("isAttacking", isAttacking);
		
		me = true;
		
	}

}

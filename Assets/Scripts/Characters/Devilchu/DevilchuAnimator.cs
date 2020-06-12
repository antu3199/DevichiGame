using UnityEngine;
using System.Collections;

public class DevilchuAnimator : MonoBehaviour {
	CharacterController controller;
//	private SpriteRenderer rend;
	private float origX;

	string direction;
	Animator MyAnimation;

	bool isIdle;
	bool isRunning;

	bool isSitting;
	public static bool PerformRegularAnimations = true;

	// Use this for initialization
	void Start () {
		PerformRegularAnimations = true;
		controller = GetComponent<CharacterController>();
	//	rend = GetComponent<SpriteRenderer>();
		origX = 1;

		if (transform.localScale.x == 1) {
			direction = "right";
		} else {
			direction = "left";
		}
		
		MyAnimation = gameObject.transform.FindChild ("Animation").GetComponent<Animator> ();

		if (GameManager.Location == "Field") {
			SittingInstance();
		}

	}
	
	// Update is called once per frame
	void Update () {
		float xVelocity = controller.velocity.x;
//		float yVelocity = controller.velocity.y;

		//Flip Player based on velocity
		if(controller.velocity.x > 0){
			transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
			direction = "right";
		}
		if(controller.velocity.x < 0){
			transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
			direction = "left";
		}


		if (isSitting == true) {
			if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
				MyAnimation.Play ("Sitting",0,0.0f);
				                 
			}
		}


		if (PerformRegularAnimations == true) {
			if (xVelocity < 0.25f) {
				//Do Idle animation
				if (controller.isGrounded) {
				
					/*
				if (isIdle == false){
					StartingTime = Time.time;
				}
				*/

					StopAllExcept (isIdle);
					isIdle = true;
					MyAnimation.SetBool ("isIdle", isIdle);
				
				}
			} else {
				//Do Running animation
				if (controller.isGrounded) {
					//animationf = "run";
					/*
				if (isRunning == false){
					StartingTime = Time.time;
				}
				*/
					StopAllExcept (isRunning);
					isRunning = true;
					MyAnimation.SetBool ("isRunning", isRunning);
				
				
				}
			}
		}
	}

	private void StopAllExcept (bool me){
		
		isIdle = false;
		MyAnimation.SetBool ("isIdle", isIdle);
		isRunning = false;
		MyAnimation.SetBool ("isRunning", isRunning);

		me = true;
		
	}

	public void SittingInstance(){
		PerformRegularAnimations = false;
		isSitting = true;
		MyAnimation.SetBool ("isSitting", isSitting);
		
		
		
	}

}

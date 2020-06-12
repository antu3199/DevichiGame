using UnityEngine;
using System.Collections;

public class EmptyAnimator : MonoBehaviour {
	CharacterController controller;
	private SpriteRenderer rend;
	private float origX;
	
	string direction;
	Animator MyAnimation;
	
	bool isIdle;
	bool isRunning;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		rend = GetComponent<SpriteRenderer>();
		origX = 1;
		if (transform.localScale.x == 1) {
			direction = "right";
		} else {
			direction = "left";
		}
		
		MyAnimation = gameObject.transform.FindChild ("Animation").GetComponent<Animator> ();
		
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
		
		
		if (xVelocity < 0.25f) {
			//Do Idle animation
			if (controller.isGrounded ) {
				
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
			if (controller.isGrounded ) {
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
	
	private void StopAllExcept (bool me){
		
		isIdle = false;
		MyAnimation.SetBool ("isIdle", isIdle);
		isRunning = false;
		MyAnimation.SetBool ("isRunning", isRunning);
		
		me = true;
		
	}
	
	
	
}

using UnityEngine;
using System.Collections;

public class WallClimbScript : MonoBehaviour {
	public static bool isTouching = false;
	public static float ClimbTouchDirection;

	GameObject Player;
	CharacterController controller;

	void Awake(){
		isTouching = false;
		ClimbTouchDirection = 0;
	}

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		controller = Player.GetComponent<CharacterController> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay (Collider other){
		if (other.tag == "Climbable" && !controller.isGrounded) {
		

			isTouching = true;

			ClimbTouchDirection = Player.transform.localScale.x;
		//	isClimning = true;
		} else {
		//	Debug.Log ("eXIT");
//			Debug.Log (other.tag);
		}

	}
	void OnTriggerExit (Collider other){
		if (other.tag == "Climbable" ) {
			isTouching = false;
			ClimbTouchDirection = 0;
//			Debug.Log (isTouching);
		} else {
			//			Debug.Log (other.tag);
		}
		
	}


}

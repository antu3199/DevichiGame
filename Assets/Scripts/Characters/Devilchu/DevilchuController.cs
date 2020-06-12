using UnityEngine;
using System.Collections;

public class DevilchuController : MonoBehaviour {
	CharacterController controller;

	Vector2 vel;
	private float jumpCounter = 0.0f;

	GameObject Emoticon;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		controller.detectCollisions = false;

		Emoticon = gameObject.transform.FindChild ("Emoticon").gameObject;
		Emoticon.GetComponent<SpriteRenderer> ().enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (controller.isGrounded );
		//Gravity
		if (!controller.isGrounded ) {
			
			vel.y -= Time.deltaTime * 80;
			jumpCounter += Time.deltaTime;
		} else {
			vel.y = -1;
			jumpCounter = 0.0f;
		}
		controller.Move (vel * Time.time);

	}

	


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController1 : MonoBehaviour
{



	private CharacterController controller;


	void Start ()
	{

		controller = GetComponent<CharacterController> ();

		controller.detectCollisions = false;

	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 vel = Vector3.zero;

		if (!controller.isGrounded) {
			vel.y -= Time.deltaTime * 80;
		}
		Debug.Log (controller.isGrounded);
		controller.Move (vel * Time.deltaTime);
	}






}

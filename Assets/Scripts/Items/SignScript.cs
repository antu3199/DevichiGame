using UnityEngine;
using System.Collections;

public class SignScript : MonoBehaviour {

	public string Sign;
	GameObject Player;
	PlayerAnimator AnimatorScript;
	PlayerController ControllerScript;
	playerHealth HealthScript;
	
	
	GameObject BBox;
	BlackBoxScript BBoxScript;
	
	// Use this for initialization
	void Start ()
	{
		Player = GameObject.Find ("Player");
		AnimatorScript = Player.GetComponent<PlayerAnimator> ();
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
		
		
		
		BBox = GameObject.FindGameObjectWithTag ("BlackBox");
		BBoxScript = BBox.GetComponent<BlackBoxScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider other){

		if (other.tag == "Player") {
			if (BBoxScript.getisOpen () == false) {
				if (Sign == "Jump") {
					GameManager.Scene = "Sign 1";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("Press the Up Arrow to jump!!!");

				}
				else if (Sign == "Attack"){
					GameManager.Scene = "Sign 2";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("Press the Z key to attack the leaf monsters!!!");

				}
				else if (Sign == "JumpAttack"){
					GameManager.Scene = "Sign 3";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("Press Z while jumping to do a spinning attack!");
					
				}
				else if (Sign == "JumpAttackX3"){
					GameManager.Scene = "Sign 4";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("You bounce when you do your jump attack. You can bounce up to 3 times before you have to land again.");
					
				}
			
				else if (Sign == "WallClimb"){
					GameManager.Scene = "Sign 5";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("You can climb blocks with paw-prints by holding the direction of the block (left/right), and pressing jump (up).");
					
				}
				else if (Sign == "WallJump"){
					GameManager.Scene = "Sign 6";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("Wall climb back and forth to {w{a{l{l{-{j{u{m{p. Wall-jumping is a bit faster than climbing, but it takes some practice.");
					
				}
				else if (Sign == "WallJump 2"){
					GameManager.Scene = "Sign 7";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("You can jump A LOT further horizontally by wall jumping compared to jumping regularly!");
					
				}
				else if (Sign == "Obstacles"){
					GameManager.Scene = "Sign 8";
					GameManager.EventNumber = 0;
					BBoxScript.setLeftPictureName ("Sign-san");
					BBoxScript.Slide ("Some deadly obstacles, such as spikes and saws, result in <i<n<s<t<a<n<t <d<e<a<t<h on contact.");
					
				}

	


			}


		}
	}
}

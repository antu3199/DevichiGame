using UnityEngine;
using System.Collections;

public class BlockCollider : MonoBehaviour {

	BreakableGroundGravity BGScript;
	BoxCollider col;
	GameObject Player;
	// Use this for initialization
	void Start () {
		BGScript = gameObject.transform.parent.gameObject.GetComponent<BreakableGroundGravity> ();
		col = GetComponent<BoxCollider> ();
		Player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	if (BGScript.getIsFalling ()) {
			col.enabled = true;
		} else {
			col.enabled = false;
		}


		if (BGScript.getisAlive () == false) {
			col.enabled = false;
		}
	}

	void OnTriggerEnter (Collider other){
		if (BGScript.getIsFalling () == true) {
			if (other.tag == "Player" && playerHealth.Dead == false && PlayerController.isFlinching == false) {
				
				//collider1.enabled = false;
				//collider2.enabled = false;
				if (gameObject.tag == "InstantKillObstacles"){
					//damage = playerHealth.MaxHealth;
				}
				other.transform.root.SendMessage ("takeDamage", playerHealth.MaxHealth, SendMessageOptions.DontRequireReceiver);
				
				PlayerController.isFlinching= true;
				
				PlayerController.vel = Vector2.zero;
				PlayerController.ExternalForce = Vector2.zero;
				
				if (Player.transform.position.x > gameObject.transform.position.x){
					PlayerController.vel = new Vector2 (7.0f,15.0f);
				}
				else{
					PlayerController.vel = new Vector2 (-7.0f,15.0f);
				}
				
				
			}
		}

	}

}

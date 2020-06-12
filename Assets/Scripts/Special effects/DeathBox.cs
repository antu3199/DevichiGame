using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {

	public int RespawnNumber;
	public Transform RespawnPoint;
	GameObject Player;
	playerHealth PlayerHealthScript;
	PlayerController PlayerControllerScript;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		PlayerHealthScript = Player.GetComponent<playerHealth> ();
		PlayerControllerScript = Player.GetComponent<PlayerController> ();
	}
	

	void OnTriggerEnter (Collider other){
		if (other.tag == "Player") {





			/*
			other.transform.root.SendMessage ("takeDamage", 6, SendMessageOptions.DontRequireReceiver);
			StartCoroutine(DeadRespawn ());
*/

		}
		if (other.tag == "Player") {
			PlayerControllerScript.EnableAutomatedTimeControl(false);
		
		}
	}

	IEnumerator DeadRespawn(){
		CameraSmooth.SmoothFollow = true;
		Player.transform.position = RespawnPoint.position;
		yield return new WaitForSeconds(2.0f);

		PlayerHealthScript.RespawnBack ();
	

	}


}

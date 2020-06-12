using UnityEngine;
using System.Collections;

public class EnemySlashWaveScript : MonoBehaviour {
	public int damage = 1;
	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter (Collider other){ 
		if (other.tag == "Player") {
			if (ControllerScript.getTimeControl() == false) {

				if (other.tag == "Player" && playerHealth.Dead == false && PlayerController.isFlinching == false) {

					if (gameObject.tag == "InstantKillObstacles"){
						damage = playerHealth.MaxHealth;
					}
					other.transform.root.SendMessage ("takeDamage", damage, SendMessageOptions.DontRequireReceiver);

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

}

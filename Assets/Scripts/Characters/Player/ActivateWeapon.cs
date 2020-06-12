using UnityEngine;
using System.Collections;

public class ActivateWeapon : MonoBehaviour {

	public Sprite Weapon;
	public Sprite ActivatedWeapon;
	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;
	int damage = 1;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
	}

	
	// Update is called once per frame
	void Update () {

		//if(PlayerWeapon.ActivateWeapon == true)

	if (ControllerScript.getTimeControl() == true && GetComponent<SpriteRenderer> ().sprite.name == "Weapon") {
			GetComponent<SpriteRenderer> ().sprite = ActivatedWeapon;


		} else if (ControllerScript.getTimeControl () == false && GetComponent<SpriteRenderer> ().sprite.name == "Weapon_Power") {
			GetComponent<SpriteRenderer> ().sprite = Weapon;
		}


	

	}
}

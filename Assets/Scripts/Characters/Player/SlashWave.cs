using UnityEngine;
using System.Collections;

public class SlashWave : MonoBehaviour {
	//float Trans = 1.0f;
	//float StartTime;
	// Use this for initialization

	GameObject BBox;
	BlackBoxScript BBoxScript;
	int Damage = 0;

	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;


	void Start () {

		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
		//GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1.0f);
	//	StartTime = Time.time;
		BBox = GameObject.FindGameObjectWithTag ("BlackBox");
		BBoxScript = BBox.GetComponent<BlackBoxScript> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Time.timeScale = 0.1f;
		/*
		Debug.Log ("test");

	if (enabled) {
		
			if (Time.time >= StartTime + 0.01){

				StartTime = Time.time;
				if (Trans - 0.1f >= 0) {
					Trans -= 0.1f;
			//	GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, Trans);
				}
				else{
			//			GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0.0f);
					}
			}
		

			//GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, Trans);

		} else {
			Trans = 1.0f;
			GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1.0f);
		}

*/
	}
	/*
	public void resetTrans (){
		Trans = 1.0f;
		GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 1.0f);
	}
	public void setStartTime (){
		StartTime = Time.time;
	}
*/
	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
			Debug.Log ("tester");

		}
	}
	void OnTriggerEnter (Collider other){
		if (ControllerScript.getTimeControl()== false) {
			if(other.tag == "enemybody"){
				GameObject Root;

				if (other.gameObject.GetComponent<shooter>() == null){
					Root = other.gameObject.transform.parent.gameObject.transform.parent.gameObject;
				}
				else{
					Root = other.gameObject;
				}
			
			

				if (Root.GetComponent<enemyHealth>().getBlinking() == false && Root.GetComponent <enemyHealth>().getDead() == false){
				if (PlayerAnimator.isAttack1ing == true){
					Damage = 3;
				}
				else if (PlayerAnimator.isAttack2ing == true){
					Damage = 1;

						PlayerController Script = transform.root.GetComponent<PlayerController>();



						if (PlayerAnimator.Attack2JumpCount < 3 && ControllerScript.getAttack2Bounce() == false){
							Script.Jump ();	
							PlayerAnimator.Attack2JumpCount ++;
						}

				}
					//Sends the damage
					Root.SendMessage("takeDamage", Damage, SendMessageOptions.DontRequireReceiver);

				}
				
			}
			if(other.tag == "Devilchu"){

				GameManager.Scene = "Field Extra 2";
				GameManager.EventNumber = 0;
				
				BBoxScript.Slide ("OWWW!! HEY! That actually really hurt!!!");
				BBoxScript.setLeftPictureName("Devilchu");

				BBoxScript.ActivateEmoticon("Devilchu","Angry", false);


			}
			if(other.tag == "Breakable"){
			if (other.GetComponent<BreakableGround>() != null){

					BreakableGround BGroundScript = other.GetComponent<BreakableGround>();
					if (BGroundScript.getisAlive() == true){
					BGroundScript.Explode();
					}

				}
				else if (other.GetComponent<BlockCollider2>() != null){
					BreakableGroundGravity BGroundScript = other.transform.parent.gameObject.GetComponent<BreakableGroundGravity>();
					if (BGroundScript.getisAlive() == true){
						BGroundScript.TakeDamage();
					}

				}
			}
		}
			

		
	}

}

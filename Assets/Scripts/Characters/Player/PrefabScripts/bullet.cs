using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bullet : MonoBehaviour {
	//this script only controls the life of the bullet. we make it disappear after 1 second. You don't want stray bullets floating around forever for no reason.
	public float bulletLife = 1.0f;
	//heres the counter variable
	private float lifeCounter = 0.0f;
	private float damage;
	private float origX;

	public static bool bulletActivate = false;
	public static  bool bulletActive = false;
	Rigidbody rb;

	List<Vector3> myList = new List<Vector3> ();
	List<bool> BulletActiveList = new List<bool> ();

	public static bool right = true;

	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;
	

	void Awake (){
		right = true;
	}

	void Start () {

		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();


		//Flip bullet based on velocity
		origX = transform.localScale.x;
		rb = GetComponent<Rigidbody> ();
		HideBullet ();
	}

	void Update () {

		if (ControllerScript.getTimeControl () == false) {
			if (bulletActivate == true) {
				bulletActivate = false;
				bulletActive = true;
				GetComponent<SpriteRenderer>().enabled = true;
			}
			if (bulletActive == true) {
				
				
				//flip the bullet depending on the velocity (Looks like doesn't do anything because bullet is symmetrical)
				if(GetComponent<Rigidbody>().velocity.x < 0){
					transform.localScale = new Vector3(-origX,transform.localScale.y,transform.localScale.z);
				}
				if(GetComponent<Rigidbody>().velocity.x > 0){
					transform.localScale = new Vector3(origX,transform.localScale.y,transform.localScale.z);
				}
				//here we add time to the counter variable
				lifeCounter += Time.deltaTime;
				
				//when the counter is higher than 1 (1 second) it will destroy the bullet it is attached to.
				if(lifeCounter > bulletLife){
					//Destroy(gameObject);
					HideBullet ();
				}
			}
		}

	}
	void FixedUpdate(){
	

		TransformTimeControl (myList);

		BulletActiveControl (BulletActiveList);
		
	}

	void BulletActiveControl (List<bool> list){
		
		if (ControllerScript.getTimeControl () == false) {
			
			
			if (bulletActive == true) {
				list.Add (true);
				
			} else {
				list.Add (false);
			}
			
			
		} else {
			if (list.Count > 0){
				if (lifeCounter - (Time.deltaTime * Time.timeScale) > 0) {
					lifeCounter -= (Time.deltaTime * Time.timeScale);
				}
				
				
				if (list [list.Count - 1] == true) {
					ShowBullet ();
					
				} else {
					HideBullet ();
				}
				list.RemoveAt(list.Count-1);
				/*
				if (list.Count == 0){
					PlayerController.TimeControl = false;
					
				}
*/
			}
		}


	}
	void ShowBullet(){
		GetComponent<SpriteRenderer>().enabled = true;
		if (right == true) {
			rb.velocity = new Vector3 (18.0f, 0, 0);
		
		} else {
			rb.velocity = new Vector3(-18.0f,0,0);

		}
	
		bulletActive = true;
		if (lifeCounter >= bulletLife) {
			lifeCounter = bulletLife;
		}

		
	}

	void HideBullet(){
		GetComponent<SpriteRenderer>().enabled = false;
		rb.velocity = Vector3.zero;
	
		bulletActive = false;
		lifeCounter = 0.0f;

	}
	
	void OnTriggerEnter (Collider other){
		if (bulletActive == true) {
			if(other.tag == "enemy"){
				if (other.GetComponent <enemyHealth>().getDead() == false){
					other.SendMessage("takeDamage", 1, SendMessageOptions.DontRequireReceiver);
					//	Destroy(gameObject);
					
					HideBullet ();
				}
			

			}
		}
	
	}

	void TransformTimeControl(List<Vector3> list){
		if (ControllerScript.getTimeControl () == false) {
			list.Add (transform.position);
			
		}
		
		else if (ControllerScript.getTimeControl () == true) {
			if (list.Count > 0){
			transform.position = list[list.Count-1];
			list.RemoveAt(list.Count-1);



			}
			
		}
	}



}

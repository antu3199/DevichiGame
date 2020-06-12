using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyBullet : MonoBehaviour
{

	public int damage = 1;
	//this script only controls the life of the bullet. we make it disappear after 1 second. You don't want stray bullets floating around forever for no reason.
	public float bulletLife = 1.0f;
	//heres the counter variable
	private float lifeCounter = 0.0f;
	private float origX = 0.0f;
	List<Vector3> myList = new List<Vector3> ();
	 
	private bool eBulletActivate = false;
	private bool eBulletActive = false;
	public int test;


	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;


	Rigidbody rb;
	void Start ()
	{
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();


		origX = transform.localScale.x;
		rb = GetComponent<Rigidbody> ();

	}
	
	void Update ()
	{
		//Time.timeScale = 0.5f;
		if (ControllerScript.getTimeControl () == false) {
		
			//flip the bullet depending on the velocity
			if (GetComponent<Rigidbody> ().velocity.x < 0) {
				transform.localScale = new Vector3 (-origX, transform.localScale.y, transform.localScale.z);
			}
			if (GetComponent<Rigidbody> ().velocity.x > 0) {
				transform.localScale = new Vector3 (origX, transform.localScale.y, transform.localScale.z);
			}
			//here we add time to the counter variable
			if (eBulletActive){
				lifeCounter += Time.deltaTime;
			}
	
		}

	}

	void FixedUpdate ()
	{
		if (GameManager.CutsceneEnabled == false && PlayerController.DisableTimeControl == false) {
			TransformTimeControl (myList);
		}
	}


	public void setLifeCounter ( float count){
		lifeCounter = count;
	}
	
	public  float getLifeCounter (){
		return lifeCounter;
	}
	public void setBulletLife (  float life){
		bulletLife = life;
	}
	
	public  float getBulletLife (){
		return bulletLife;
	}

	void OnTriggerEnter (Collider other)
	{
		if (ControllerScript.getTimeControl () == false) {
			if (eBulletActive == true){
			
				if (other.tag == "Player" && playerHealth.Dead == false) {

					other.transform.root.SendMessage ("takeDamage", damage, SendMessageOptions.DontRequireReceiver);



					GetComponent<SpriteRenderer> ().enabled = false;
					rb.velocity = Vector3.zero;
					
					eBulletActive = false;
					setLifeCounter (0.0f);
					
					
					//Destroy(gameObject);
				}
				else if (other.tag == "SlashWave"&& playerHealth.Dead == false){
					GetComponent<SpriteRenderer> ().enabled = false;
					rb.velocity = Vector3.zero;
					
					eBulletActive = false;
					setLifeCounter (0.0f);
				}


			}
		}
	}


	
	public void setBulletActivate (bool act)
	{
		
		eBulletActivate = act;
		
		
	}
	
	public bool getBulletActivate ()
	{
		return eBulletActivate;
		
		
	}
	
	public void getBulletActive (bool act)
	{
		eBulletActive = act;
	}
	
	public void setBulletActive (  bool act)
	{
		eBulletActive = act;
	}
	
	public bool getBulletActive ()
	{
		return eBulletActive;
	}


	void TransformTimeControl (List<Vector3> list)
	{
		if (ControllerScript.getTimeControl () == false) {
			list.Add (transform.position);
			
		} else if (ControllerScript.getTimeControl () == true) {
			if (list.Count > 0) {
				transform.position = list [list.Count - 1];
				list.RemoveAt (list.Count - 1);
				
				
				
			}
			
		}
	}




}

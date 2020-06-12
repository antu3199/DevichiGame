using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shooter : MonoBehaviour
{

	//here are the public variables for the shooter enemy. these can be changed in the inspector.
	public float runSpeed = 4.0f;
	//the sound when he shoots a bullet
	public AudioClip shootSound;
	//the textures that make him look like an enemy
	public Sprite[] run;
	public Sprite[] shoot;
	//the bullet he shoots
	public GameObject enemyBullet;
	public float bulletSpeed = 16.0f;
	public Transform bulletPosition;
	
	//private variables that help with animating the enemy
	private CharacterController controller;
	private float counter = 0.0f;
	private int i = 0;
	private GameObject target;
	private float frameRate = 8.0f;
	private bool shooting = false;
	private SpriteRenderer rend;
	private float origX;
	private Vector3 vel;
	public GameObject bullet;
	bool right = true;

	enemyBullet bulletScript;
	Rigidbody bulletrb;

	enemyHealth HealthScript;

	bool CanControl = true;
	bool isFlinching = false;

	List<Vector3> EnemyPositions = new List <Vector3>();
	List<Vector2> EnemyVelocities = new List <Vector2>();
	List<int> DirectionList = new List<int>();


	List<bool> BulletActiveList = new List<bool> ();

	public GameObject SlashWave;
	GameObject Player;
	PlayerController ControllerScript;
	playerHealth PHealthScript;

	//we want to use the player as a reference for animating and giving a simple AI.
	void Start ()
	{
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		PHealthScript = Player.GetComponent<playerHealth> ();

		controller = GetComponent<CharacterController> ();
		rend = GetComponent<SpriteRenderer> ();
		target = GameObject.FindGameObjectWithTag("Player");
		//Physics.IgnoreCollision (target.GetComponent<Collider> (), GetComponent<Collider> ());
		Physics.IgnoreCollision (target.transform.root.GetComponent<Collider> (), GetComponent<Collider> ());
		//Physics.IgnoreCollision (SlashWave.GetComponent<BoxCollider> (), GetComponent<Collider> ());

		origX = transform.localScale.x;
		//here it makes it ignore other enemy colliders so they can't get caught on each other.
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");

		foreach (GameObject en in enemies) {
			if (en.GetComponent<Collider> () != GetComponent<Collider> ()) {
				Physics.IgnoreCollision (GetComponent<Collider> (), en.GetComponent<Collider> ());
			}
		}
		bullet = Instantiate (enemyBullet, bulletPosition.position, Quaternion.Euler (0, 0, 0)) as GameObject;
		bullet.GetComponent<SpriteRenderer> ().enabled = false;
		bulletrb = bullet.GetComponent<Rigidbody> ();
		bulletScript = bullet.GetComponent<enemyBullet> ();

		HealthScript = GetComponent<enemyHealth> ();

	}
	public void Flinch( int strength){
		CanControl = false;
		isFlinching = true;
		vel.y = strength * 5.0f;


		if (transform.localScale.x == -1.0f) {
			vel.x = strength * 3.0f;
		} else {
			vel.x = -strength * 3.0f;
		}
		//shooting = false;
		//counter = 0.0f;
		//i = 0;
		//StopCoroutine (shootBullet ());
		//StartCoroutine (StopFlinch ());

	}
	//IEnumerator StopFlinch(){
	//	yield return new WaitForSeconds (0.3f);
	//	vel = Vector2.zero;
		//CanControl = true;
	///	StopCoroutine (StopFlinch ());
	//}


	void Update ()
	{
//		Debug.Log (vel.y);
		if (GameManager.CutsceneEnabled == false) {
			if (ControllerScript.getTimeControl () == false) {

				//Apply Gravity
				if (!controller.isGrounded || vel.y > 0) {
					vel.y -= Time.deltaTime * 80;
				} else {
					vel.y = -1;
				}
				if (isFlinching) {
					//	shooting = false;
					//counter = 0.0f;
					//i = 0;
					//StopCoroutine (shootBullet ());
				}
				if (isFlinching == true && vel.y < 0 && controller.isGrounded) {
					vel = Vector2.zero;
					CanControl = true;
					isFlinching = false;
				}

				if (CanControl == true) {
					//here we check the distance away the player is from the enemy
					float distance = target.transform.position.x - transform.position.x;

					float ydistance = target.transform.position.y - transform.position.y;
					if (distance < 0) {
						distance *= -1;
					}
					if (ydistance < 0) {
						ydistance *= -1;
					}
					if (target.transform.position.x > transform.position.x) {
						transform.localScale = new Vector3 (origX, transform.localScale.y, transform.localScale.z);
						right = true;
					}
					if (target.transform.position.x < transform.position.x) {
						transform.localScale = new Vector3 (-origX, transform.localScale.y, transform.localScale.z);
						right = false;
					}


					if (bulletScript.getBulletActive () == true && bulletScript.getLifeCounter () > bulletScript.getBulletLife ()) {
						HideBullet ();
					}

		
					if (bulletScript.getBulletActive () == false && HealthScript.getDead () == false && playerHealth.Dead == false) {




						//if the player is close enough. the enemy can start animating and doing its thing.
						if (distance < 16 && ydistance < 8) {
//							Debug.Log (counter);
							counter += Time.deltaTime * frameRate;
							if (distance > 9 && distance < 16 && !shooting) {
								if (target.transform.position.x < transform.position.x) {
									vel.x = -runSpeed;
								}
								if (target.transform.position.x > transform.position.x) {
									vel.x = runSpeed;
								}
								if (counter > i && i < run.Length) {
									rend.sprite = run [i];
									i += 1;
								}
								if (counter > run.Length) {
									counter = 0.0f;
									i = 0;
								}
							}
							if (shooting) {

								vel.x = 0;
								if (counter > i && i < shoot.Length) {
									rend.sprite = shoot [i];
									i += 1;
								}
							}



							//the enemy will shoot a bullet only if the player is close enough and shooting is indeed false.
							if (distance < 9 && shooting == false && HealthScript.getDead () == false) {

								//ShowBullet();
								StartCoroutine (shootBullet ());

						
							}
						}
					}
		
				}
		
				//if the enemy falls down a hole, we want to destroy it so it doesn't exist for no reason.
				if (transform.position.y < -10) {
					//	Destroy (gameObject);
				}
		
				//Apply movement to player based on vel variable which is a Vector3.
				controller.Move (vel * Time.deltaTime);
			} else {

			}
			if (ControllerScript.getTimeControl ()) {
				//	isFlinching = false;
			}
		}

	}

	void FixedUpdate(){
		if (GameManager.CutsceneEnabled == false && PlayerController.DisableTimeControl == false) {

		
		TransformTimeControl (EnemyPositions);
		DirectionTimeControl (DirectionList);
		VelocityTimeControl (EnemyVelocities);
		BulletActiveControl (BulletActiveList);
		}
	}

	void ShowBullet ()
	{
		bullet.GetComponent<SpriteRenderer> ().enabled = true;
		if (right == true) {
			bulletrb.velocity = new Vector3 (16.0f, 0, 0);
			
		} else {
			bulletrb.velocity = new Vector3 (-16.0f, 0, 0);
			
		}
		
		bulletScript.setBulletActive (true);

		if (bulletScript.getLifeCounter () >= bulletScript.getBulletLife ()) {
			bulletScript.setLifeCounter (bulletScript.getBulletLife ());
		}
		
		
	}
	
	void HideBullet ()
	{
		bullet.GetComponent<SpriteRenderer> ().enabled = false;
		bulletrb.velocity = Vector3.zero;
		
		bulletScript.setBulletActive (false);
		bulletScript.setLifeCounter (0.0f);
		
	}


	public void setRight (bool direction)
	{
		right = direction;
	}
	
	public bool getRight ()
	{
		return right;
	}

	//if the shooter wants to shoot a bullet, then this function is called.
	public IEnumerator shootBullet ()
	{

		if (ControllerScript.getTimeControl () == false) {

			vel.x = 0;
			shooting = true;
			counter = 0.0f;
			i = 0;
			//we wait for a bit before the shot fires so that the player can see he's about to do it
			yield return new WaitForSeconds (1.0f);
			//play the shot sound
			//GetComponent<AudioSource>().PlayOneShot(shootSound);
			//spawn the bullet

			///////////////////////////////////////
	
			if (HealthScript.getDead() == false && isFlinching == false && CanControl == true){
				ShowBullet ();
				
				bullet.transform.position = bulletPosition.position;
				
				//set velocity to the bullet
				if (bulletPosition.position.x < transform.position.x) {
					bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (-bulletSpeed, 0, 0);
				} else {
					bullet.GetComponent<Rigidbody> ().velocity = new Vector3 (bulletSpeed, 0, 0);
				}
				//then pause again before the enemy is ready to fire again.
				yield return new WaitForSeconds (1.5f);
				shooting = false;
			}
			else{
				StopCoroutine(shootBullet ());
				shooting = false;
			}
		}
	}

	void BulletActiveControl (List<bool> list){
		
		if (ControllerScript.getTimeControl () == false) {
			
			
			if (bulletScript.getBulletActive() == true) {
				list.Add (true);
				
			} else {
				list.Add (false);
			}
			
			
		} else {
			if (list.Count > 0){
				if (bulletScript.getLifeCounter() - (Time.deltaTime * Time.timeScale) > 0) {
					bulletScript.setLifeCounter(bulletScript.getLifeCounter() - (Time.deltaTime * Time.timeScale));
			
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
	void DirectionTimeControl (List<int> list){
		if (ControllerScript.getTimeControl () == false) {
			if (transform.localScale.x == 1) {
			
				list.Add (1);
			} else {
			
				list.Add (-1);
			}

			
		} else {
			if (list.Count > 0) {
				transform.localScale = new Vector3 (list[list.Count-1], transform.localScale.y, transform.localScale.z);

				list.RemoveAt (list.Count - 1);
			}
		}


		}
	void VelocityTimeControl (List<Vector2> list)
	{
		if (ControllerScript.getTimeControl () == false) {
			list.Add (vel);
			
		} else if (ControllerScript.getTimeControl () == true) {
			if (list.Count > 0) {
				vel = list [list.Count - 1];
				list.RemoveAt (list.Count - 1);
				
				
				
			}
			
		}
	}



}

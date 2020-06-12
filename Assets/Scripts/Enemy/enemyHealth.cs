using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyHealth : MonoBehaviour {

	//the death animation that spawns if he dies
	public GameObject deathAnim;
	public GameObject deathAnimBack;
	//a heart that might drop when he dies
//	public GameObject heartDrop;
	public int health = 6;
	public AudioClip hurtSound;
	
	private SpriteRenderer rend;
	private bool isDead = false;
	deathAnimation DeathAnimationScript;
	List<int> HealthList = new List<int> ();

	bool isBlinking = false;

	shooter ShooterScript;
	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;

	public bool isFlinchable;
	string EnemyType;


	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
		if (GetComponent<shooter> () != null) {
			EnemyType = "shooter";
			ShooterScript = GetComponent<shooter>();
		}
		if (GetComponent<LeafController> () != null) {
			EnemyType = "Leaf";
		}


	}

	void FixedUpdate(){
	//	Debug.Log (isDead);
		if (GameManager.CutsceneEnabled == false && PlayerController.DisableTimeControl == false) {

		
		DamageTimeControl (HealthList);
		}
	}
	//here we manage health of the enemy when a bullet hits it. if its health is 0 it will spawn the death animation, possibly spawn a heart, then destroy itself.
	void takeDamage ( int damage) {
		if (!isDead) {

			//GetComponent<AudioSource>().PlayOneShot(hurtSound);
			//FLINCHER 

			if (isFlinchable ){
				if (EnemyType == "shooter"){
					ShooterScript.Flinch(damage);
				}

			}

			health -= damage;
			if (health <= 0) {
				isDead = true;
				GameObject DeathAnimation = Instantiate (deathAnim, transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;
				DeathAnimationScript = DeathAnimation.GetComponent<deathAnimation>();


			//	rend.enabled = false;

				Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
				for (int i = 0; i < children.Length; ++i) {
					
					if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
						children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
						
					}
				}
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
				
				foreach (GameObject en in enemies) {
					if (en.GetComponent<Collider> () != GetComponent<Collider> ()) {
						Physics.IgnoreCollision (GetComponent<Collider> (), en.GetComponent<Collider> ());
					}
				}

			
		
				/* Drops item (heart) if randNum = 2
				int randNum = Random.Range(1,4);
				if(randNum == 2){
					Instantiate(heartDrop, transform.position, Quaternion.Euler(0,180,0));
				}
				*/
				//Destroy(gameObject);

			} else {
				StartCoroutine (resetColor ());
				StartCoroutine (Blinking());
			}
		} 
	}
	
	//called when hit to show that the enemy was hit
	public IEnumerator resetColor () {
		Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().color = new Vector4(1.0f,0.25f,0.25f,1.0f);
				
			}
		}
		//rend.color = new Vector4(1.0f,0.25f,0.25f,1.0f);
		yield return new WaitForSeconds (0.15f);
		for (int i = 0; i < children.Length; ++i) {
			
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().color = new Vector4(1.0f,1.0f,1.0f,1.0f);
				
			}
		}

	}
	public IEnumerator Blinking () {
		isBlinking = true;

		yield return new WaitForSeconds (0.15f);
	
		isBlinking = false;
	}

	public bool getBlinking(){
		return isBlinking;
	}

	public IEnumerator spawnBack () {
		yield return new WaitForSeconds (0.2f);
		//if (PlayerController.TimeControl == true) {
		Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				
			}
		}

		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("enemy");
		
		foreach (GameObject en in enemies) {
			if (en.GetComponent<Collider> () != GetComponent<Collider> ()) {
				Physics.IgnoreCollision (GetComponent<Collider> (), en.GetComponent<Collider> (),false);
			}
		}

			isDead = false;
		//}


	}

	public bool getDead(){
		return isDead;
	}


	void DamageTimeControl ( List<int> list ){
		if (ControllerScript.getTimeControl () == false) {
			list.Add (health);
			
		} else if (ControllerScript.getTimeControl () == true) {
			//	checkHealth();
			int PreviousHealth = health;

			if (list.Count > 0) {
				health = list [list.Count - 1];
				if (PreviousHealth <= 0 && health > 0){
					GameObject DeathAnimation = Instantiate (deathAnim, transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;
					DeathAnimationScript = DeathAnimation.GetComponent<deathAnimation>();
					//DeathAnimationScript.setBackwards(true);
					StartCoroutine(spawnBack());
	

				}
				//updateHearts();
			
				
				list.RemoveAt (list.Count - 1);
				
				
				
			}
			
		}
	}

}

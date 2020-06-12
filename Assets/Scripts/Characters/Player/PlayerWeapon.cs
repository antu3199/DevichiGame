using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {
	//bullet objects found in the Prefabs folder
	public GameObject[] bullets;
	//damage for each bullet
	public float[] bulletsDamage;
	//speed for each bullet
	public float[] bulletsSpeed;
	//firerate for each bullet
	public float[] bulletsFirerate;
	//spawn position of bullet
	public Transform spawnPosition;
	//sound of the bullet when fired
	//public AudioClip bulletSound;
	//sound when you pick up a bullet upgrade. by default we just used the same sound as the heart pickup.
	//public AudioClip pickupSound;
	
	//private variables used to control shooting bullets
	private float bulletCounter = 0.0f;
	//private float bulletPos = 0.0f;
//	private int weaponSet = 0;
//	private GameObject currentBullet;
	private float currentSpeed;
//	private float currentDamage;
	private float fireRate = 0.25f;
	//private bool dead = false;

	// Use this for initialization
	void Start () {
		//when the game starts we want to check to see what bullet the player was using last. This is also called when a pickup is hit.
		//updateBulletType();
	//	currentBullet = bullets[0];
		fireRate = bulletsFirerate[0];
		currentSpeed = bulletsSpeed[0];
		//currentDamage = bulletsDamage[0];
	}
	
	// Update is called once per frame
	void Update () {
		//keep track of time so we know when a bullet can fire
		///Gets Width
		//Debug.Log ("" + gameObject.GetComponent<SpriteRenderer>().bounds.size.x);

		bulletCounter += Time.deltaTime;

		if(playerHealth.Dead == false){

			//controls for shooting bullets for web versions of the game. These are the same as standalone, but are only compiled if its web
			#if UNITY_STANDALONE || UNITY_WEBPLAYER
			if(Input.GetKey(KeyCode.X)   ){
			
				if(bulletCounter > fireRate && bullet.bulletActive == false){

					shootBullet();
				}
			}
			#endif
			
			//controls for shooting bullets for ios versions of the game. These are the same as android, but are only compiled if its ios
			#if UNITY_IOS || UNITY_ANDROID
			/*
			if(Input.touchCount > 0){
				for(var touch1 : Touch in Input.touches) { 
					//2nd touch for jump button
					if(touch1.position.x > Screen.width/2 && touch1.position.x < Screen.width/4*3 && touch1.position.y < Screen.height/3){
						if(bulletCounter > fireRate){
							shootBullet();
						}	
					}
				}
			}
			*/
			#endif
		}

	}
	void shootBullet () {
		Vector3 pos = spawnPosition.position;
	
		GameObject bulletPrefab = GameObject.Find ("bullet1");
		bulletPrefab.transform.position = pos;
		bullet.bulletActivate = true;

		//	GameObject bulletPrefab = Instantiate(currentBullet, pos, Quaternion.Euler(0,180,0)) as GameObject;

		//bulletPrefab.SendMessage("getDamageAmount", currentDamage, SendMessageOptions.DontRequireReceiver);
		//GetComponent<AudioSource>().PlayOneShot(bulletSound);
		if(spawnPosition.position.x > transform.position.x){
			bulletPrefab.transform.GetComponent<Rigidbody>().velocity = new Vector3(currentSpeed,0,0);
			bullet.right = true;
		}else{
			bulletPrefab.transform.GetComponent<Rigidbody>().velocity =  new Vector3(-currentSpeed,0,0);
			bullet.right = false;
		}
		bulletCounter = 0.0f;

	}
	/*
	void updateBulletType () {
		//PlayerPrefs.DeleteAll ();
		//PlayerPrefs is used to save data....

		var getSet = PlayerPrefs.GetInt("weaponset");


		if(getSet >= bullets.Length){
			getSet = bullets.Length-1;
		}
		currentBullet = bullets[getSet];
		fireRate = bulletsFirerate[getSet];
		currentSpeed = bulletsSpeed[getSet];
		currentDamage = bulletsDamage[getSet];


	}
*/



}

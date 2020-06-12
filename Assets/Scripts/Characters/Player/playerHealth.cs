using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerHealth : MonoBehaviour
{
	public static bool Dead = false;
	//here are public variables that can be edited in the inspector for the players health.
	public int hearts = 3;
	//sound when he's hit
	public AudioClip hitSound;
	//death animation if he dies
	public GameObject deathAnim;
	//the 3 different heart textures that will be changed in the hearts at the top of the screen depending on his health.
	public Texture heartWhole;
	public Texture heartHalf;
	public Texture heartEmpty;
	//the sound if a heart is picked up
	public AudioClip heartSound;

	private bool canGetHurt = true;
	private SpriteRenderer rend;
	private GUITexture[] heartsGUI;
	private int health;
	List<int> HealthList = new List<int> ();
	deathAnimation DeathAnimationScript;

	public GameObject Body;
	public static int MaxHealth;

	GameObject Player;
	PlayerController ControllerScript;

	void Awake(){
		Dead = false;
	}

	void Start ()
	{
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		//Display all hearts on screen
		health = hearts * 2;
		MaxHealth = health;
		GameObject getHearts = GameObject.Find ("hearts"); // Inside GUI object
		getHearts.SendMessage ("addHearts", hearts, SendMessageOptions.DontRequireReceiver);
		GUITexture[] allChildren = getHearts.GetComponentsInChildren<GUITexture> ();
		heartsGUI = new GUITexture[allChildren.Length];
		health = allChildren.Length * 2;
		for (int i = 0; i < allChildren.Length; i++) {
			heartsGUI [i] = allChildren [i] as GUITexture;
		}
	
		rend = GetComponent<SpriteRenderer> ();

		if (GameManager.Location == "FieldCutscene") {
			
			Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
			for (int i = 0; i < children.Length; ++i) {
				
				//if (transform.GetChild (i).childCount > 0){
				if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
					children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
			}

		}

	}

	void FixedUpdate ()
	{
		//Update list for time control
		if (GameManager.CutsceneEnabled == false && PlayerController.DisableTimeControl == false) {
			DamageTimeControl (HealthList);
		}

	}

	//Take damage from a damaging thing.
	void takeDamage (int amount)
	{
		if (canGetHurt && !Dead) {
			canGetHurt = false;
			//GetComponent<AudioSource>().PlayOneShot(hitSound);
			health -= amount;
			StartCoroutine (checkHealth ());
			StartCoroutine (resetCanHurt ());
		}
	}

	//Adds back health if in time control
	void DamageTimeControl (List<int> list)
	{
		if (ControllerScript.getTimeControl () == false) {
			list.Add (health);
			
		} else if (ControllerScript.getTimeControl () == true) {
			//	checkHealth();
			int PreviousHealth = health;
			
			if (list.Count > 0) {
				health = list [list.Count - 1];
				if (PreviousHealth <= 0 && health > 0) {
					//Death Animation
					GameObject DeathAnimation = Instantiate (deathAnim, transform.position, Quaternion.Euler (0, 180, 0)) as GameObject;
					DeathAnimationScript = DeathAnimation.GetComponent<deathAnimation> ();
					//DeathAnimationScript.setBackwards(true);
				//Spawning delay
					StartCoroutine (spawnBack ());
				}
				updateHearts ();
				list.RemoveAt (list.Count - 1);

			}
			
		}
	}

	public IEnumerator spawnBack ()
	{
		yield return new WaitForSeconds (0.2f);
		Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {

			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null && children[i].gameObject.name != "Emoticon") {
			
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;

			}
		}
		Dead = false;

		
	}
	public void RespawnBack(){
		health = 6;
		updateHearts ();
		StartCoroutine (RespawnBackCor ());
	}
	public IEnumerator RespawnBackCor ()
	{
		yield return new WaitForSeconds (0.2f);
		Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				
			}
		}
		Dead = false;
		PlayerController.canControl = true;
		
	}


	//here we check to see if the we hit an enemy or spikes, but we won't get hurt if our color was changed because that was the indication that we were hurt not too long ago. like 0.25 seconds ago.
	void OnTriggerStay (Collider other)
	{
		/*
		if(other.tag == "enemy" || other.tag == "spikes"){
			if(canGetHurt && !dead){
				canGetHurt = false;
			//	GetComponent<AudioSource>().PlayOneShot(hitSound);
				health -= 1;
				StartCoroutine(checkHealth());
				StartCoroutine(resetCanHurt());
			}
		}
		//if its a heart though, we want health back instead of taken away.
		if(other.GetComponent<Collider>().tag == "heart"){
		//	Destroy(other.gameObject);
			addHealth();
		}
*/
	}
	
	//this is the same as ontriggerstay, but for enemy's whose colliders aren't triggers.
	void OnCollisionStay (Collision other)
	{
		/*
		if (other.collider.tag == "enemy" || other.collider.tag == "spikes") {
			if (canGetHurt && !Dead) {
				canGetHurt = false;
				//GetComponent<AudioSource>().PlayOneShot(hitSound);
				health -= 1;
				StartCoroutine (checkHealth ());
				StartCoroutine (resetCanHurt ());
			}
		}
		*/
	}
	
	public IEnumerator resetCanHurt ()
	{
		Body.GetComponent<SpriteRenderer>().color = new Vector4 (1.0f, 0.25f, 0.25f, 1.0f);
		yield return new WaitForSeconds (0.25f);
		Body.GetComponent<SpriteRenderer>().color = new Vector4 (1.0f, 1.0f, 1.0f, 1.0f);
		canGetHurt = true;
	}



	// -HEART STUFF ---------------------------------------------------------------------------
	//here we checkhealth when a player is hit by an enemy.
	public IEnumerator checkHealth ()
	{
		//here we update the hearts on the screen so that they show an accurate health amount
		updateHearts ();
	
		// if health is 0 then we're going to do all of this stuff once, which is why we check to see if dead was previously false.
		//it turns off a bunch of stuff like physics, renderers, scripts, then waits for 3 seconds before it reloads the scene again.	
		if (health <= 0 && Dead == false) {
			Dead = true;
			Instantiate (deathAnim, transform.position, Quaternion.Euler (0, 180, 0));
			BroadcastMessage ("died", SendMessageOptions.DontRequireReceiver);
			//var rend = GetComponent<SpriteRenderer>();
		


			Transform[] children = gameObject.GetComponentsInChildren<Transform> ();
			for (int i = 0; i < children.Length; ++i) {
			
				//if (transform.GetChild (i).childCount > 0){
				if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
					children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
		

			yield return new WaitForSeconds (1);
			if (GameManager.Location == "Field"){
				ControllerScript.EnableAutomatedTimeControl(true);
			}


			//	string lvlName = Application.loadedLevelName;
			//	Application.LoadLevel(lvlName);
		}
	}
	
	//here we add health back.
	void addHealth ()
	{
		GetComponent<AudioSource> ().PlayOneShot (heartSound);
		health += 2;
		//if the players health is more than 6, we want to make sure its 6 because thats the max we chose.
		if (health > 6) {
			health = 6;
		}
		//here we update the hearts on the screen so that they show an accurate health amount
		updateHearts ();
	}
	public void ClearHealthLists(){
		for (int i = 0; i < HealthList.Count; i++) {
			HealthList.RemoveAt(i);
		}
	}

	public void HideHearts(){
		GameObject heartsgui = GameObject.FindWithTag ("PlayerHearts");
		GUITexture[] children = heartsgui.GetComponentsInChildren<GUITexture> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){
			if (children [i].GetComponent<GUITexture> () != null) {
				children [i].GetComponent<GUITexture> ().enabled = false;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}
	public void ShowHearts(){
		GameObject heartsgui = GameObject.FindWithTag ("PlayerHearts");
		GUITexture[] children = heartsgui.GetComponentsInChildren<GUITexture> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){
			if (children [i].GetComponent<GUITexture> () != null) {
				children [i].GetComponent<GUITexture> ().enabled = true;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}

	public void updateHearts ()
	{	
	
	
		//here we check how much health the player has, then apply the texture to the hearts in GUI/hearts accordingly
		for (int i = 0; i < heartsGUI.Length; i++) {
			int check = (i + 1) * 2;
			if (check < health + 1) {
				heartsGUI [i].texture = heartWhole;
			}
			if (check == health + 1) {
				heartsGUI [i].texture = heartHalf;
			}
			if (check > health + 1) {
				heartsGUI [i].texture = heartEmpty;
			}
		}
	}
}

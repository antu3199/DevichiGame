using UnityEngine;
using System.Collections;

public class deathAnimation : MonoBehaviour {

	//here are the star textures that make a "stylized" explosion. this is used on both enemies and the player on death.
	public Sprite[] deathSprites;
	public Sprite[] deathSpritesBack;
	//we also want to multiply the counter by a number to get a specific framerate
	public float frameRate = 12.0f;
	//death sound
	public AudioClip deathSound;
	//we want to set a counter so the animation can be based on time
	private float counter = 0.0f;
	private int i = 0;
	private SpriteRenderer rend;
	bool DoneAnimation = false;

	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;



	void Start () {
		
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();

		rend = GetComponent<SpriteRenderer>();
		//play the death sound once as soon as this object is spawned
		GetComponent<AudioSource>().PlayOneShot(deathSound);
	}
	
	void Update () {
		if (ControllerScript.getTimeControl ()) {
		

			//keeping track of time with counter
			counter += Time.deltaTime * frameRate;
			if (counter > i && i < deathSprites.Length) {
				rend.sprite = deathSprites [i];
				i += 1;
			}
			//If animation finishes, we destroy the object
			if (counter > deathSprites.Length) {
				Destroy (gameObject);
			}

		} else {
			//keeping track of time with counter
			counter += Time.deltaTime*frameRate;
			if(counter > i && i < deathSpritesBack.Length){
				rend.sprite = deathSpritesBack[i];
				i += 1;
			}
			//If animation finishes, we destroy the object
			if(counter > deathSpritesBack.Length){
				//DoneAnimation = true;
				Destroy(gameObject);
			}
		}
	
	}
	public bool isDoneAnimation(){
		return DoneAnimation;
	}
	public void DestroyEverything(){
		Destroy (gameObject);
	}



}

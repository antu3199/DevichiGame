using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BreakAbleClassGravity{
	
	public Vector3 pos;
	public Vector2 vel;
	public int Health;

	public float StartTime;

	public bool StartingToFall;
	public bool isFalling;

	public bool isBlinking;
	public float BlinkingCounter;


	public bool isPlaying;
	public ParticleSystem.Particle [] Particles;
	public bool isGrounded;

	public float ThisTime;
	public BreakAbleClassGravity(Vector3 _pos, Vector2 _vel, int _Health, float _StartTime, 
	  bool _StartingToFall, bool _isFalling, bool _isBlinking, 
	        float _BlinkingCounter, bool _isPlaying, ParticleSystem.Particle [] _Particles, bool _isGrounded, float _ThisTime ){

		pos = _pos;
		vel = _vel;
		Health = _Health;
		StartTime = _StartTime;
		StartingToFall = _StartingToFall;
		isFalling = _isFalling;
		isBlinking = _isBlinking;
		BlinkingCounter = _BlinkingCounter;
		isPlaying = _isPlaying;
		Particles = _Particles;
		isGrounded = _isGrounded;
		ThisTime = _ThisTime;
	}
	
}

public class BreakableGroundGravity : MonoBehaviour {

	
	List<BreakAbleClass> myList = new List<BreakAbleClass> ();

	List<BreakAbleClassGravity> myList2 = new List<BreakAbleClassGravity> ();
	
	public Sprite BigBlock;
	public Sprite SmallBlock;
	public Sprite DamagedBlock1;
	public Sprite DamagedBlock2;

	
	public bool isBigBlock;
	
	public Sprite BigGround1;
	
	public ParticleSystem Explosion;
	public ParticleSystem Smoke;

	public int Health;


	bool isAlive = true;
	SpriteRenderer rend;
	
	bool isPlaying = false;
	
	
	ParticleSystem.Particle [] Particles;
	ParticleSystem.Particle [] SmokeParticles;

	bool SmokePlaying;

	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;

	CharacterController controller;

	bool StartingToFall = false;
	bool isFalling = false;

	float StartTime = 0.0f;
	Vector2 vel;

	bool isBlinking = false;
	float BlinkCounter = 0.0f;

	bool isGrounded = false;
	float TimeThing = 0.0f;
	float ThisTime = 0.0f;
	// Use this for initialization
	void Start () {
		
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();
		

		isAlive = true;
		rend = GetComponent<SpriteRenderer> ();

		if (isBigBlock) {
			Explosion.startLifetime = 1.5f;
			Explosion.startSize = 0.7f;
			Explosion.maxParticles = 30;
			Explosion.startSpeed = 1.0f;


		} else {
			Explosion.startLifetime = 0.56f;
			Explosion.startSize = 0.65f;
			Explosion.maxParticles = 25;
		}
		controller = GetComponent<CharacterController> ();
		controller.detectCollisions = false;
	
		Physics.IgnoreCollision (Player.transform.root.GetComponent<Collider> (), GetComponent<Collider> ());

	


	}
	//3.132505
	// Update is called once per frame
	void Update () {
	
		if (ControllerScript.getTimeControl () == false) {

			if (isAlive == true){
			


			if (isBlinking == true) {
				BlinkCounter += 10 * Time.deltaTime;
				if (BlinkCounter > 3.0f) {
					isBlinking = false;
					BlinkCounter = 0.0f;
				}

			}

			
				if (ThisTime < 0.1f + TimeThing) {
					//Debug.Log ("k");
					if (!isGrounded) {
						vel.y -= Time.deltaTime * 70;
					} else {
						vel.y = -1.0f;
					}
				}
				else{

				}


				if (!isGrounded && StartingToFall == false && isFalling == false && ThisTime >= 0.1f + TimeThing) {

					Smoke.Play ();

					StartTime = ThisTime;
					vel.y = 0.0f;
					StartingToFall = true;
			

				}
			
				if (StartingToFall == true) {


					if (isFalling == false) {
						vel.y = 0.0f;
					}
		
				}

				if (StartingToFall == true && ThisTime > 2.5f + StartTime) {

					isFalling = true;
					Smoke.Stop ();
				}



				if (isFalling == true) {


					if (!isGrounded) {
						vel.y -= Time.deltaTime * 70;
					} else {
						if ( ThisTime >= 0.1f + TimeThing){
							isFalling = false;
							StartingToFall = false;
						}


					}
			

				}

				controller.Move (vel * Time.deltaTime);
			
			}
			
			SmokePlaying = Smoke.isPlaying;
			if (SmokePlaying) {
				SmokeParticles = new ParticleSystem.Particle[Smoke.particleCount];
				Smoke.GetParticles (SmokeParticles);
			}
			
			
			isPlaying = Explosion.isPlaying;
			if (Explosion.enableEmission == false) {
				Explosion.enableEmission = true;
			}
			
			if (isPlaying) {
				
				Particles = new ParticleSystem.Particle[Explosion.particleCount];
				Explosion.GetParticles (Particles);
				
				
			}
		}
		//	Explosion.Play ();
	}
	void FixedUpdate(){
		if (ControllerScript.getTimeControl () == false) {

			ThisTime += Time.fixedDeltaTime;

			myList.Add ( new BreakAbleClass(isAlive, isPlaying, Particles));
			/*
		pos = _pos;
		vel = _vel;
		Health = _Health;
		StartTime = _StartTime;
		StartingToFall = _StartingToFall;
		isFalling = _isFalling;
		isBlinking = _isBlinking;
		BlinkingCounter = _BlinkingCounter;
		isPlaying = _isPlaying;
		Particles = _Particles;
		*/

			isGrounded = controller.isGrounded;
			myList2.Add (new BreakAbleClassGravity(transform.position, vel, Health, StartTime, StartingToFall, isFalling,
			                                       isBlinking, BlinkCounter, SmokePlaying, SmokeParticles, isGrounded, ThisTime));
		} else {

		

		


			if (myList.Count > 0){

				isAlive = myList[myList.Count-1].isAlive;
				
				if (isAlive == true){
					if (rend.sprite == null){
						if (isBigBlock == true){
							rend.sprite = BigBlock;
						}
						else{
							rend.sprite = SmallBlock;
						}

					}
				}
				else{
					if (rend.sprite != null){
						rend.sprite = null;

					}


					isPlaying = myList[myList.Count-1].isPlaying;
					if (isPlaying == true){
						Explosion.Play ();
						Explosion.SetParticles (myList[myList.Count-1].Particles, myList[myList.Count-1].Particles.Length);
				

					}
					else{
						Explosion.enableEmission = false;
						
					}


				}

				if (isAlive == true){
					Explosion.SetParticles (null, 0);
					Explosion.enableEmission = false;
				}

				
				myList.RemoveAt (myList.Count-1);

			}

			if (myList2.Count > 0){
				
				transform.position = myList2[myList2.Count-1].pos;
			
				isGrounded = myList2[myList2.Count-1].isGrounded;
				
				Health = myList2[myList2.Count-1].Health;
				UpdateHealth();
				
				StartTime = myList2[myList2.Count-1].StartTime;
				StartingToFall = myList2[myList2.Count-1].StartingToFall;
				isFalling = myList2[myList2.Count-1].isFalling;

				vel = myList2[myList2.Count-1].vel;


				isBlinking = myList2[myList2.Count-1].isBlinking;
				BlinkCounter = myList2[myList2.Count-1].BlinkingCounter;
				
				
				SmokePlaying = myList2[myList2.Count-1].isPlaying;
				if (SmokePlaying == true){
					Smoke.Play ();
					Smoke.SetParticles (myList2[myList2.Count-1].Particles, myList2[myList2.Count-1].Particles.Length);
						Smoke.loop = true;
				}
				else{
					Smoke.Stop();
					
				}
				ThisTime = myList2[myList2.Count-1].ThisTime;
				TimeThing = ThisTime;
				
				
				myList2.RemoveAt (myList2.Count-1);
			}




		}

	}

	void OnTriggerEnter( Collider other){

	}

	public bool getIsFalling(){
		return isFalling;
	}

	public void Explode(){
		//Explosion.GetParticles();
		//Explostion.SetParticles();
	
		
		Explosion.Play ();
		rend.sprite = null;
		isAlive = false;

		Smoke.Stop ();
		isFalling = false;
		StartingToFall = false;
		//GetComponent<Collider>().enabled = false;
	//	collider1.enabled = false;
	//	collider2.enabled = false;



	}
	public bool getisAlive(){
		return isAlive;
	}

	public void TakeDamage(){
		if (isBlinking == false) {
			Health --;
			UpdateHealth ();
			isBlinking = true;
			BlinkCounter = 0.0f;
		}
	}
	void UpdateHealth (){

		if (Health == 3 && rend.sprite != BigBlock) {
			rend.sprite = BigBlock;
		}
		if (Health == 2 && rend.sprite != DamagedBlock1) {
			rend.sprite = DamagedBlock1;
		}
	else if (Health == 1 && rend.sprite != DamagedBlock2) {
			rend.sprite = DamagedBlock2;
		} else if (Health <= 0 && ControllerScript.getTimeControl () == false) {
			Explode ();
		}

	}
	public bool getisBlinking(){
		return isBlinking;

	}


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BreakAbleClass{
	
	public bool isAlive;
	public bool isPlaying;
	public ParticleSystem.Particle [] Particles;

	public BreakAbleClass(bool _isAlive, bool _isPlaying, ParticleSystem.Particle [] _Particles){
		isAlive = _isAlive;
		isPlaying = _isPlaying;
		Particles = _Particles;
	}
	
}



public class BreakableGround : MonoBehaviour {

	List<BreakAbleClass> myList = new List<BreakAbleClass> ();

	public Sprite BigBlock;
	public Sprite SmallBlock;

	public bool isBigBlock;

	public Sprite BigGround1;

	ParticleSystem Explosion;

	bool isAlive = true;
	SpriteRenderer rend;
	BoxCollider collider;

	bool isPlaying = false;


	ParticleSystem.Particle [] Particles;

	GameObject Player;
	PlayerController ControllerScript;

	// Use this for initialization
	void Start () {

		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();


		Explosion = GetComponent<ParticleSystem> ();
	//	Explosion.playbackSpeed = -1.0f;
		isAlive = true;
		rend = GetComponent<SpriteRenderer> ();
		collider = GetComponent<BoxCollider> ();
		if (isBigBlock) {
			Explosion.startLifetime = 1.5f;
			Explosion.startSize = 0.7f;
			Explosion.maxParticles = 30;
		} else {
			Explosion.startLifetime = 0.56f;
			Explosion.startSize = 0.65f;
			Explosion.maxParticles = 25;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (ControllerScript.getTimeControl () == false) {
			isPlaying = Explosion.isPlaying;
		
			if (isPlaying) {
				//	Debug.Log (Explosion.GetParticles);
				//Particles = Explosion.getP
				//Particles = Explosion.GetParticles(Particles);
				//Debug.Log (Particles);
				//Explosion.Particle[3];
				Particles = new ParticleSystem.Particle[Explosion.particleCount];
				Explosion.GetParticles (Particles);
			

			}
//			Debug.Log (isAlive);
			if (isAlive == true){
			
			//	Explosion.Stop ();
			}


		}
	//	Explosion.Play ();
	}
	void FixedUpdate(){
		if (ControllerScript.getTimeControl () == false) {
			myList.Add ( new BreakAbleClass(isAlive, isPlaying, Particles));
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

						collider.enabled = true;
					}
				}
				else{
					if (rend.sprite != null){
						rend.sprite = null;
						collider.enabled = false;
					}

					isPlaying = myList[myList.Count-1].isPlaying;
					if (isPlaying == true){
						Explosion.Play();
						Explosion.SetParticles (myList[myList.Count-1].Particles, myList[myList.Count-1].Particles.Length);
					
					}
					else{
						Explosion.Stop();

					}

				}
				if (isAlive == true){
					Explosion.SetParticles (null, 0);
					Explosion.Stop();
				}

				myList.RemoveAt (myList.Count-1);
				
			}
			//Explosion.Stop();
		}

	}

	public void Explode(){
		//Explosion.GetParticles();
		//Explostion.SetParticles();
	

		Explosion.Play ();
		rend.sprite = null;
		isAlive = false;
		collider.enabled = false;
	}
	public bool getisAlive(){
		return isAlive;
	}


}

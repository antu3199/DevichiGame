using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {
	ParticleSystem Explosion;
	bool Active = true;
	SpriteRenderer rend;
	// Use this for initialization
	void Start () {
		Explosion = GetComponent<ParticleSystem> ();
		rend = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
void OnTriggerEnter( Collider other){

		if (other.tag == "Leafree" && Active == true || other.tag == "Ground" && Active == true) {
			Explosion.Play();
			Active = false;
			rend.enabled = false;

		}
	}
	void OnCollisionEnter( Collision other){
		if (other.gameObject.tag == "Ground" && Active == true) {
			Explosion.Play();
			Active = false;
			rend.enabled = false;
			Debug.Log ("aTERZT");

		}

	}
}

using UnityEngine;
using System.Collections;

public class CutsceneGroundBreak : MonoBehaviour {
	SpriteRenderer rend;
	public Sprite Original;
	public Sprite Cracked;
	bool Broken = false;
	public int TriggerNumber;
	BoxCollider Collide2D;
	ParticleSystem Explosion;
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		Collide2D = GetComponent<BoxCollider> ();
		if (GetComponent<ParticleSystem> () != null) {

			Explosion = GetComponent<ParticleSystem>();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Location == "FieldCutscene"){
if (GameManager.Scene == "FieldCutscene 2"){
			if (TriggerNumber == 1){
				if (GameManager.EventNumber == 12) {
					rend.sprite = Cracked; 
				}
			}
			if (TriggerNumber == 2){
				if (GameManager.EventNumber == 13) {
					rend.sprite = Cracked; 
				}
			}
			if (GameManager.EventNumber == 14) {
				rend.enabled = false;
				Collide2D.enabled = false;


				if (GetComponent<ParticleSystem> () != null) {
					
					Explosion.Play ();
					StartCoroutine(Delay ());
				}

			}

	
			}
		}
	}

	
	IEnumerator Delay ()
	{
		yield return new WaitForSeconds (0.1f);

		if (GameManager.EventNumber == 14) {
			GameManager.EventNumber++;
		}

	}

}




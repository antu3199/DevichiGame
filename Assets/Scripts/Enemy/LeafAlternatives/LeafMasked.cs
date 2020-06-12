using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeafMasked : MonoBehaviour {
	CharacterController controller;
	List<EnemyTimeAnimator> myList = new List<EnemyTimeAnimator> ();
	List<Vector3> TransformTimeList = new List<Vector3>();
	List<bool> FadeInList = new List<bool>();
	List<float> FaderList = new List<float>();


	Vector2 vel;
	private float jumpCounter = 0.0f;
	public float runSpeed;
	float counter = 0;
	float origX = 1.0f;
	bool CanControl = true;
	bool isFlinching = false;
	bool Attacking = false;
	bool right = false;
	GameObject target;
	GameObject Devilchu;
	bool isIdling = true;

	float Counter = 0.0f;
	Animator MyAnimation;
	string State ;
	string Animation;
	float DelayTime;
	float ThisTime ;
	float normalTime ;
	public float RunningDelay = 1.0f;
	GameObject SlashWave;
	enemyHealth HealthScript;
	GameObject Player;
	PlayerController ControllerScript;
	playerHealth PHealthScript;
	GameObject Aura;

	float AuraFader = 0.0f;
	bool FadeIn = true;
	public float FloatSpeed;
	public float FloatDistanceDivider;


	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		PHealthScript = Player.GetComponent<playerHealth> ();
		
		controller = GetComponent<CharacterController> ();
		controller.detectCollisions = false;
		
		target = GameObject.FindGameObjectWithTag ("Player");
		//Physics.IgnoreCollision (target.GetComponent<Collider> (), GetComponent<Collider> ());
		Physics.IgnoreCollision (target.transform.root.GetComponent<Collider> (), GetComponent<Collider> ());
		
		
		Devilchu = GameObject.FindGameObjectWithTag ("Devilchu");
		Physics.IgnoreCollision (Devilchu.transform.root.GetComponent<Collider> (), GetComponent<Collider> ());
		
		MyAnimation = gameObject.transform.FindChild ("Animation").GetComponent<Animator> ();
		
		//SlashWave = gameObject.transform.FindChild ("Animation/LeafBody/LeafLLeg/SlashWave").gameObject;
		//SlashWave.GetComponent<BoxCollider> ().enabled = false;
		//SlashWave.GetComponent<SpriteRenderer> ().enabled = false;
		
		HealthScript = gameObject.GetComponent<enemyHealth> ();
		Aura = gameObject.transform.FindChild ("Animation/LeafBody/Aura").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (HealthScript.getDead () == false) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + Mathf.Sin (Time.time * FloatSpeed) / FloatDistanceDivider, transform.position.z);
			isIdling = true;
			if (isIdling == true && MyAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f) {
				MyAnimation.Play ("Idle", 0, 0.0f);
			}
			if (FadeIn == true) {
				if (AuraFader + 0.8f * Time.deltaTime < 1.0f) {
					AuraFader += 0.8f * Time.deltaTime;
			
				} else {
					FadeIn = false;

				}


			} else if (FadeIn == false) {
				if (AuraFader - 0.8f * Time.deltaTime > 0.0f) {
					AuraFader -= 0.8f * Time.deltaTime;
				
				} else {
					FadeIn = true;
				
				}
			}
			Aura.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, AuraFader);

		}
	}

	void FixedUpdate(){
		TransformTimeControl (TransformTimeList);
		FadeInTimeControl (FadeInList);
		FaderTimeControl (FaderList);

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
	void FadeInTimeControl (List<bool> list){
		if (ControllerScript.getTimeControl () == false) {
			list.Add(FadeIn);
			
			
		} else {
			if (list.Count > 0) {
				FadeIn = list[list.Count-1];
				list.RemoveAt (list.Count - 1);
			}
		}
		
		
	}
	void FaderTimeControl (List<float> list){
		if (ControllerScript.getTimeControl () == false) {
			list.Add(AuraFader);
			
			
		} else {
			if (list.Count > 0) {
				AuraFader = list[list.Count-1];
				list.RemoveAt (list.Count - 1);
			}
		}
		
		
	}



}




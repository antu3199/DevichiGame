using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rain : MonoBehaviour {

	SpriteRenderer Spriter;

	public Sprite Rain1;
	public Sprite Rain2;
	public Sprite Rain3;

	int RainSprite = 1;

	public float Delay;

	float StartTime;
	List <int> SpriteList = new List<int>();


	GameObject Player;
	PlayerController ControllerScript;
	playerHealth HealthScript;
	

	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		HealthScript = Player.GetComponent<playerHealth> ();

		Spriter = GetComponent<SpriteRenderer> ();
		StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (ControllerScript.getTimeControl() == false) {
			if (Time.time > StartTime + Delay) {
				StartTime = Time.time;
				
				if (RainSprite == 1) {
					RainSprite++;
					Spriter.sprite = Rain1;
					
				} else if (RainSprite == 2) {
					RainSprite ++;
					Spriter.sprite = Rain2;
				} else if (RainSprite == 3) {
					RainSprite = 1;
					Spriter.sprite = Rain3; 
				}

				
			}
			SpriteList.Add (RainSprite);

		} else {
			if (SpriteList.Count > 0){
				RainSprite = SpriteList[SpriteList.Count-1];
				if (RainSprite == 1) {
					Spriter.sprite = Rain1;
				} else if (RainSprite == 2) {
					Spriter.sprite = Rain2;
				} else if (RainSprite == 3) {
					
					Spriter.sprite = Rain3; 
				}
				
				SpriteList.RemoveAt(SpriteList.Count-1);
			}


		}
	


	
	}
}

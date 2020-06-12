using UnityEngine;
using System.Collections;

public class BlueScript : MonoBehaviour {

	public GameObject RingInner;
	public GameObject RingOuter;
	public GameObject RingMiddle;

	public GameObject RingOuter2;
	public GameObject Aura;


	SpriteRenderer InnerRend;
	SpriteRenderer OuterRend;
	SpriteRenderer OuterRend2;
	SpriteRenderer MiddleRend;
	SpriteRenderer AuraRend;

	public GameObject Flame;
	SpriteRenderer FlameRend;


	float RingTrans = 0.0f;

	PlayerController ControllerScript;

	float Rotation1 = 0.0f;
	float Rotation2 = 0.0f;
	float Rotation3 = 0.0f;
	float Rotation4 = 0.0f;

	int RingPick;

	float MaxRotate = 90;

	bool isRotating = false;

	 public GameObject GodlyTextBox;
	SpriteRenderer GodlyTextBoxRend;

	float TextBoxCounterX = 0.0f;
	float TextBoxCounterY = 0.2f;

	public static bool AddEventNumber = false;


	// Use this for initialization
	void Start () {
		ControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();

		InnerRend = RingInner.GetComponent<SpriteRenderer> ();
		OuterRend = RingOuter.GetComponent<SpriteRenderer> ();
		OuterRend2 = RingOuter2.GetComponent<SpriteRenderer> ();
		MiddleRend = RingMiddle.GetComponent<SpriteRenderer> ();
		FlameRend = Flame.GetComponent<SpriteRenderer> ();

		AuraRend = Aura.GetComponent<SpriteRenderer> ();
		UpdateRingTrans ();

		GodlyTextBoxRend = GodlyTextBox.GetComponent<SpriteRenderer> ();
		AddEventNumber = false;


	}
	
	// Update is called once per frame
	void Update () {
		//Max is exclusive
		Debug.Log (RingPick);
		if (ControllerScript.getTimeControl () == true || GameManager.TimeControlTalk == true) {
			UpdateRingTrans ();
			if (RingTrans <= 0.55){
			RingTrans += 0.5f * Time.deltaTime;
			}

			if (isRotating == false){
				RingPick = Random.Range (1, 4);
				//ForwardBackward = Random.Range (1, 3);
			//	ForwardBackward = 1;
				isRotating = true;

				if (RingPick == 1){
					MaxRotate = Rotation1 + 60.0f;
				}
				else if (RingPick == 2){
					MaxRotate = Rotation2 + 60.0f;
				}
				else if (RingPick == 3){
					MaxRotate = Rotation3 + 60.0f;
				}

			}

			if (isRotating == true){
				//Rotation += 100 * Time.deltaTime;
			if (RingPick == 1){
					Rotation1 += 550 * Time.deltaTime;
				
					if (Rotation1 >= MaxRotate){
						isRotating = false;
					}

					RingInner.transform.eulerAngles = new Vector3(0.0f,0.0f,Rotation1);

				}
				else if (RingPick == 2){
					Rotation2 += 550 * Time.deltaTime;
					
					if (Rotation2 >= MaxRotate){
						isRotating = false;
					}
					RingMiddle.transform.eulerAngles = new Vector3(0.0f,0.0f,Rotation2);
			
				}
				else if (RingPick == 3){
					Rotation3 += 550 * Time.deltaTime;
					
					if (Rotation3 >= MaxRotate){
						isRotating = false;
					}
					RingOuter.transform.eulerAngles = new Vector3(0.0f,0.0f,Rotation3);
				
				}
			//	else if (RingPick == 3){
					Rotation4 += 50 * Time.deltaTime;
					
					if (Rotation4 >= MaxRotate){
						isRotating = false;
					}
					RingOuter2.transform.eulerAngles = new Vector3(0.0f,0.0f,Rotation4);

			//	if (Rotation >= MaxRotate){
		
			//		isRotating = false;

			//	}

			}

		} else { 

			if (RingTrans > 0.0f){
				RingTrans -= 1.0f * Time.deltaTime;
				UpdateRingTrans();
				AuraRend.color = new Color (1.0f, 1.0f, 1.0f, RingTrans);
			}
			else{
				Flame.GetComponent<SpriteRenderer>().enabled = false;
				InnerRend.enabled = false;
				OuterRend.enabled = false;
				MiddleRend.enabled = false;
				//gameObject.GetComponent<SpriteRenderer>().enabled = false;
				isRotating = false;
			//	Rotation = 0.0f;
				RingTrans = 0.0f;
				UpdateRingTrans();
			}


		}


		if (GameManager.TimeControlTalk == true) {
			if (GodlyTextBoxRend.enabled == false) {
				GodlyTextBoxRend.enabled = true;
			}
			if (TextBoxCounterX < 1.0f) {
				TextBoxCounterX += 5.0f * Time.deltaTime;
				GodlyTextBox.transform.localScale = new Vector3 (TextBoxCounterX, 0.2f, 1.0f);
			} else {
				if (TextBoxCounterY < 1.0f) {
					TextBoxCounterY += 5.0f * Time.deltaTime;
					GodlyTextBox.transform.localScale = new Vector3 (1.0f, TextBoxCounterY, 1.0f);
				} else {
				
					if (AddEventNumber == true){
						GameManager.EventNumber++;
						AddEventNumber = false;
					}

				}

			}

		} else {
			if ((TextBoxCounterY > 0.2f)){
				TextBoxCounterY -= 5.0f * Time.deltaTime;
				GodlyTextBox.transform.localScale = new Vector3 (1.0f, TextBoxCounterY, 1.0f);

			}
			if (TextBoxCounterY <= 0.2f && TextBoxCounterX > 0.0f){
				TextBoxCounterX -= 5.0f * Time.deltaTime;
				GodlyTextBox.transform.localScale = new Vector3 (TextBoxCounterX, 0.2f, 1.0f);
			}
			if (TextBoxCounterY < 0.2f){
				TextBoxCounterY = 0.2f;
				GodlyTextBox.transform.localScale = new Vector3 (TextBoxCounterX, 0.2f, 1.0f);
			}
			if (TextBoxCounterX < 0.0f){
				TextBoxCounterX = 0.0f;
				GodlyTextBox.transform.localScale = new Vector3 (TextBoxCounterX, 0.2f, 1.0f);
			}

			if (TextBoxCounterY <= 0.2f && TextBoxCounterX <= 0.0f){
				GodlyTextBoxRend.enabled =false;
			}

		}


	}
	void UpdateRingTrans(){
		InnerRend.color = new Color (1.0f, 1.0f, 1.0f, RingTrans);
		OuterRend.color = new Color (1.0f, 1.0f, 1.0f, RingTrans);
		OuterRend2.color = new Color (1.0f, 1.0f, 1.0f, RingTrans);
		MiddleRend.color = new Color (1.0f, 1.0f, 1.0f, RingTrans);

		if (RingTrans <= 0.2) {
			FlameRend.color = new Color (1.0f, 1.0f, 1.0f, RingTrans);
		} else {
			FlameRend.color = new Color (1.0f, 1.0f, 1.0f, 0.2f);
		}
	
	}

}

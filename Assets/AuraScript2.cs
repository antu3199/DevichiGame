using UnityEngine;
using System.Collections;

public class AuraScript2 : MonoBehaviour {
	GameObject Aura;
	public float MaxAura;
	float AuraFader = 0.0f;
	bool FadeIn = true;
	PlayerController ControllerScript;

	// Use this for initialization
	void Start () {
		Aura = gameObject;
		ControllerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if (GameManager.TimeControlTalk == true || ControllerScript.getTimeControl () == true) {


		
			if (FadeIn == true) {
				if (AuraFader + 0.5f * Time.deltaTime < MaxAura) {
					AuraFader += 0.5f * Time.deltaTime;
					
					transform.localScale = new Vector3 (transform.localScale.x + 0.05f, transform.localScale.x + 0.05f, 1.0f);
					
				} else {
					
					FadeIn = false;
					
				}
				
			} else if (FadeIn == false) {
				if (AuraFader - 0.5f * Time.deltaTime > 0.0f) {
					AuraFader -= 0.5f * Time.deltaTime;
					
					
					transform.localScale = new Vector3 (transform.localScale.x - 0.05f, transform.localScale.x - 0.05f, 1.0f);
					
				} else {
					
					FadeIn = true;
					
				}
			}
			Aura.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, AuraFader);
		} else {
			FadeIn = true;

			if (AuraFader - 1.0f * Time.deltaTime > 0.0f) {
				AuraFader -= 1.0f * Time.deltaTime;
				Aura.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, AuraFader);
			}
			else{
				AuraFader = 0.0f;
				Aura.GetComponent<SpriteRenderer>().enabled = false;
			}

			if (transform.localScale.x - 0.5f > 1.0f){
				transform.localScale = new Vector3 (transform.localScale.x - 0.5f, transform.localScale.x - 0.5f, 1.0f);
			}
		



		}


	}
}

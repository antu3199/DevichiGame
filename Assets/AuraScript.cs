using UnityEngine;
using System.Collections;

public class AuraScript : MonoBehaviour {
	GameObject Aura;

	public float MaxAura;
	float AuraFader = 0.0f;
	bool FadeIn = true;
//	float FadeDelay = 0.0f;
	//public float MaxDelay;
	bool StopGlowing = false;
	void Awake(){


	}
	// Use this for initialization
	void Start () {
		Aura = gameObject;

	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.Scene == "CaveCutscene 5") {
			if (GameManager.EventNumber == 2){
			//	if (gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled == true){
				gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
					StopGlowing = true;
					FadeIn = false;
			//	}
				//GameManager.EventNumber ++;
			}
			
		}
		if (StopGlowing == false) {
			if (FadeIn == true) {
				if (AuraFader + 0.3f * Time.deltaTime < MaxAura) {
					AuraFader += 0.3f * Time.deltaTime;

					transform.localScale = new Vector3 (transform.localScale.x + 0.05f, transform.localScale.y + 0.05f, 1.0f);

				} else {
				
					FadeIn = false;
				
				}

			} else if (FadeIn == false) {
				if (AuraFader - 0.3f * Time.deltaTime > 0.0f) {
					AuraFader -= 0.3f * Time.deltaTime;
			

					transform.localScale = new Vector3 (transform.localScale.x - 0.05f, transform.localScale.y - 0.05f, 1.0f);

				} else {
		
					FadeIn = true;

				}
			}
		} else {
			AuraFader -= 0.6f * Time.deltaTime;
			transform.localScale = new Vector3 (transform.localScale.x - 0.1f, transform.localScale.y - 0.05f, 1.0f);

		}
			Aura.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, AuraFader);
	
	}
	public float getFade (){
		return AuraFader;

	}

}

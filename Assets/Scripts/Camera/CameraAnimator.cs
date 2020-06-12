using UnityEngine;
using System.Collections;

public class CameraAnimator : MonoBehaviour {
	public static bool WalkToCutscene = false;
	public static bool AnimatorEnabled = false;

	public float WalkToCutsceneSpeed;
	public GameObject Player;
	public float WalkToCutsceneDistance;

	public bool EarthquakeEnabled;

	public float speed;

	public float EarthQuakeMaxAngle;



	void Awake(){
		WalkToCutscene = false;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	//	Debug.Log (GameManager.EventNumber);

		if (EarthquakeEnabled) {
			Shake ();


			
		}

		if (GameManager.Location == "FieldCutscene" ) {
			if ( GameManager.EventNumber == 7){
			GameManager.EventNumber ++;
			PlayEarthQuakeInstance(5.0f);
			}
			else if (GameManager.EventNumber == 8) {
				EarthQuakeMaxAngle += 0.005f;
			}
			
			else if ( GameManager.EventNumber == 11){
				GameManager.EventNumber ++;
				PlayEarthQuakeInstance(5.0f);
			}
			else if ( GameManager.EventNumber == 12 || GameManager.EventNumber == 13){
				EarthQuakeMaxAngle += 0.005f;
			}
	
		}



	if (GameManager.CutsceneEnabled == true) {
		if (WalkToCutscene == true){
				if ( transform.position.x  < Player.transform.position.x +WalkToCutsceneDistance){
					transform.position = new Vector3 (transform.position.x + WalkToCutsceneSpeed, transform.position.y, transform.position.z);
				}

			
			}



		}
	}

	void Shake ()
	{
		float AngleAmount = (Mathf.Cos (Time.time * speed) * 180) / Mathf.PI * 0.5f;
		
		AngleAmount = Mathf.Clamp (AngleAmount, -EarthQuakeMaxAngle, EarthQuakeMaxAngle);
		transform.localRotation = Quaternion.Euler (0, 0, AngleAmount);

	}
	public void PlayEarthQuakeInstance(float time){
		if (EarthquakeEnabled == true) {
			EarthquakeEnabled = false;
			EarthQuakeMaxAngle  = 0.5f;
			transform.localRotation = Quaternion.Euler (0, 0, 0.0f);
			StopCoroutine(StopAfterTime(time));
		}
		EarthquakeEnabled = true;
		StartCoroutine (StopAfterTime (time));

	}
	IEnumerator StopAfterTime(float time){
		yield return new WaitForSeconds (time);
		EarthquakeEnabled = false;
		EarthQuakeMaxAngle  = 0.5f;
		transform.localRotation = Quaternion.Euler (0, 0, 0.0f);
		if (GameManager.EventNumber == 9) {
			GameManager.EventNumber ++;
		}
	}

}

using UnityEngine;
using System.Collections;

public class CameraSetTrigger : MonoBehaviour {
	public bool isEventTriggerOnly;
	bool EventTrigger = false;

	GameObject EventTriggerObject;



	//Is event Trigger only means that the hitbox is NOT the camera;

	// Use this for initialization
	void Start () {
		if (isEventTriggerOnly) {
			EventTriggerObject = gameObject.transform.FindChild ("EventTrigger").gameObject;
		}

	}
	
	// Update is called once per frame
	void Update () {

	if (isEventTriggerOnly && GameManager.EnableTriggers == true) {
			if (EventTrigger) {

				CameraSmooth.SmoothFollow = true;
				CameraSmooth.FollowTarget = transform;
				EventTrigger = false;
			}
		}

	}
	public void setEventTrigger( bool trig){
		EventTrigger = trig;
	}
	public bool getEventTrigger(){
		return EventTrigger;
	}

	public void setisEventTriggerOnly( bool trig){
		isEventTriggerOnly = trig;
	}
	public bool getisEventTriggerOnly(){
		return isEventTriggerOnly;
	}

	public void ResetToPlayer(){
		CameraSmooth.SmoothFollow = true;
		CameraSmooth.FollowTarget = GameObject.Find ("Player").transform;
	}


	void OnTriggerEnter (Collider other){
	//	if (GameManager.EnableTriggers == true) {

			if (isEventTriggerOnly == false) {

			if (GameManager.RealCutscene == false) {


				if (other.tag == "Player") {
					Debug.Log ("Test");
					CameraSmooth.SmoothFollow = true;
					CameraSmooth.FollowTarget = transform;
				}
			}
		}
	//	}
	}
	void OnTriggerExit(Collider other){
	//	if (GameManager.EnableTriggers == true) {
			if (GameManager.CutsceneEnabled == false) {

			if (GameManager.RealCutscene == false) {
	
				if (other.tag == "Player") {

		
					CameraSmooth.SmoothFollow = true;
					CameraSmooth.FollowTarget = GameObject.Find ("Player").transform;
				}
			}
		}
	//	}
		}
}

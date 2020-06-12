using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

	// This script gets attached to the player. It will find the camera and make it follow. 
	// Set Dead Zone to 0 if you want the camera to follow the player exactly.
	
	// The camera in the scene. It is private because it is dealt with in function Start()
	public GameObject target;
	
	public float deadZone = 0;
	public bool followVertical = false;
	public bool followHorizontal = true;
	public static float minimumHeight = 1;
	public bool FixedVertical;

	public float FollowVeritcalDownHeight = 4.03f;

 void	Awake(){
		minimumHeight = 1;
	}

	
	void Start () {
		//The variable cam will look for the Main Camera in the scene before the scene starts running and make it become the variable cam.
		if(target == null){
			target = GameObject.Find("Player");
		}
	}
	
	void Update () {
		if (GameManager.CutsceneEnabled == false && CameraSmooth.SmoothFollow == false) {
		
		
			if (target != null) {
				//If Follow Horizontal is checked in inspector, the camera follows player horizonally with the deadzone.
				if (followHorizontal == true && target.transform.position.y > transform.position.y - 20.0f) {
					if (transform.position.x >= target.transform.position.x + deadZone) {
						transform.position = new Vector3 (target.transform.position.x + deadZone, transform.position.y, transform.position.z);
					}
					if (transform.position.x <= target.transform.position.x - deadZone) {
						transform.position = new Vector3 (target.transform.position.x - deadZone, transform.position.y, transform.position.z);
					}
				}
			
				//If Follow Vertical is checked in inspector, the camera follows player vertically with the deadzone.
				if (followVertical == true) {
					//if (transform.position.y >= target.transform.position.y + 4.03f) {
					transform.position = new Vector3 (transform.position.x, target.transform.position.y - FollowVeritcalDownHeight, transform.position.z);
				//	}
				//	if (transform.position.y <= target.transform.position.y - deadZone) {
					transform.position = new Vector3 (transform.position.x, target.transform.position.y + FollowVeritcalDownHeight, transform.position.z);
				//	}
				}
			
				//if the camera hits the stated minimum height, it will not go any lower than that.
				if (FixedVertical == true){
					if (target.transform.position.y < minimumHeight) {
						transform.position = new Vector3 (transform.position.x, minimumHeight, transform.position.z);
					}
				}

			}




		} 
	}
}


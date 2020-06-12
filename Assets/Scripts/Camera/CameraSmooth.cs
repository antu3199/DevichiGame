using UnityEngine;
using System.Collections;

public class CameraSmooth : MonoBehaviour
{
	//CAMERA VARIABLE(S)
	public float DampTime = 0.1f;
	public static  Transform FollowTarget;

//	public Transform FollowTarget;

	private Vector3 Velocity = Vector3.zero;

	public static bool SmoothFollow = false;
	public static bool FollowPlayer = false;

	public bool FollowHorizontal;
	public bool FollowVertical;

	public bool FixedVertical;

//	bool Follow = false;

	GameObject Player;
//	PlayerController PlayerControllerScript;

	void Awake (){
		SmoothFollow = false;
		FollowPlayer = true;
	}
	void Start(){
		Player = GameObject.Find ("Player");
		FollowTarget = Player.transform;

	//	PlayerControllerScript = Player.GetComponent<PlayerController> ();

	}


	void Update ()
	{
		if (FollowTarget) {
			if (SmoothFollow == true){

				//Debug.Log ("tester");


	if (FollowTarget.gameObject.name == "Player"){
					FollowPlayer = true;
				}
				else{
					FollowPlayer = false;
					FollowVertical = false;
					FixedVertical = false;
				}

			Vector3 Point = GetComponent<Camera> ().WorldToViewportPoint (FollowTarget.position);
			Vector3 Delta = FollowTarget.position - GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, Point.z));

			Vector3 Destination = transform.position + Delta;
			
				if (FollowHorizontal == false){
					Destination =new Vector3(transform.position.x,Destination.y , Destination.z);
				}
				if (FollowVertical == true){

				Destination =new Vector3(Destination.x,FollowTarget.position.y - CameraSmoothVertical.CameraHeight, Destination.z);
				}
				if (FixedVertical == true){
					Destination =new Vector3(Destination.x,cameraFollow.minimumHeight, Destination.z);
				}

			transform.position = Vector3.SmoothDamp (transform.position, Destination, ref Velocity, DampTime);
				
			if (FollowPlayer == true){
					FollowVertical = true;
					FixedVertical = false;
				if (transform.position.x >= FollowTarget.position.x){
					if (transform.position.x - FollowTarget.position.x < 1.0f){
						SmoothFollow = false;
							FollowVertical = false;
							FixedVertical = true;
							//Follow = false;
					}



				}
				else {
					if (FollowTarget.position.x - transform.position.x < 1.0f){
						SmoothFollow = false;
							FollowVertical = false;
							FixedVertical = true;
						//	Follow = false;
					}

				}

				}

		}
		}

	}
	void FixedUpdate(){


	}

	IEnumerator StopFollowPlayer(){
		yield return new WaitForSeconds (0.5f);
	}



}
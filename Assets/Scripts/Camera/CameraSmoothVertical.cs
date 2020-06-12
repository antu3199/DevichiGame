using UnityEngine;
using System.Collections;

public class CameraSmoothVertical : MonoBehaviour {
	//CAMERA VARIABLE(S)
	private Vector3 Velocity = Vector3.zero;
	public float DampTime = 0.1f;

	public static float CameraHeight = -4.03f;

 Transform FollowTarget;
	public bool Enabled;

	GameObject Player;


	//PlayerController PlayerControllerScript;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("Player");
		FollowTarget = Player.transform;
//		PlayerControllerScript = Player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	if (Enabled) {
			if (FollowTarget == Player.transform && CameraSmooth.SmoothFollow == false) {

				DampTime = 0.0f / Time.timeScale;

				Vector3 Point = GetComponent<Camera> ().WorldToViewportPoint (FollowTarget.position);
				Vector3 Delta = FollowTarget.position - GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, Point.z));
				
				Vector3 Destination = transform.position + Delta;
				Destination =new Vector3(transform.position.x,Destination.y , Destination.z);
					Destination =new Vector3(Destination.x,FollowTarget.position.y - CameraHeight, Destination.z);
				transform.position = Vector3.SmoothDamp (transform.position, Destination, ref Velocity, DampTime);


			}
		

		}
	}
}

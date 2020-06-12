using UnityEngine;
using System.Collections;

public class RotatingObject : MonoBehaviour {
	public float RotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	//	transform.eulerAngles = new Vector3 (0, 0, -RotateVar);
		transform.Rotate (new Vector3(0.0f,0.0f,-RotationSpeed*Time.deltaTime));
	}
}

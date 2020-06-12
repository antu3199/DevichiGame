using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter (Collider other){
	if (other.tag == "Player") {
			if (PlayerController.SpawnPoint.x != transform.position.x){
			PlayerController.SpawnPoint = new Vector2 (transform.position.x,transform.position.y);
			}
		}


	}
}

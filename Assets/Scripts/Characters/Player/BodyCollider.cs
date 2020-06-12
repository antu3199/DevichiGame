using UnityEngine;
using System.Collections;

public class BodyCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//this will happen if a trigger collider hits it. we check the tag though so only objects tagged with pickup will make this happen. it upgrades the weapons on pickup.
	void OnTriggerEnter (Collider other){
		
		
		
		if(other.tag == "pickup"){
			
			Destroy(other.gameObject);
			//GetComponent<AudioSource>().PlayOneShot(pickupSound);
			
		}
	}

}

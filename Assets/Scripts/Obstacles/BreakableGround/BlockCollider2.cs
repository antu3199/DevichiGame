using UnityEngine;
using System.Collections;

public class BlockCollider2 : MonoBehaviour {

	
	BreakableGroundGravity BGScript;
	BoxCollider col;
	// Use this for initialization
	void Start () {
		BGScript = gameObject.transform.parent.gameObject.GetComponent<BreakableGroundGravity> ();
		col = GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (BGScript.getIsFalling () ||  BGScript.getisAlive() == false) {
		//	col.enabled = false;
			col.isTrigger = true;

	

		} else {
			//col.enabled = true;
			col.isTrigger = false;
		

		}

		if (BGScript.getisAlive () == false) {

			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("BreakableGravity");
			
			foreach (GameObject en in enemies) {
				if (en.GetComponent<CharacterController> () != gameObject.transform.parent.gameObject.GetComponent<CharacterController> ()) {
					Physics.IgnoreCollision (gameObject.transform.parent.gameObject.GetComponent<CharacterController> (), en.GetComponent<CharacterController> ());
				}
			}
		} else {
			/*
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("BreakableGravity");
			
			foreach (GameObject en in enemies) {
				if (en.GetComponent<CharacterController> () != gameObject.transform.parent.gameObject.GetComponent<CharacterController> ()) {
					Physics.IgnoreCollision (gameObject.transform.parent.gameObject.GetComponent<CharacterController> (), en.GetComponent<CharacterController> (), false);
				}
			}
			*/
		}

		/*
		if (BGScript.getisAlive() == false){
			col.enabled = false;
		}
		*/
//		Debug.Log (BGScript.getisAlive ());


	}
}

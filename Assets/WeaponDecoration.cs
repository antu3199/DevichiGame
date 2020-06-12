using UnityEngine;
using System.Collections;

public class WeaponDecoration : MonoBehaviour {

	public GameObject Glow;
	SpriteRenderer WeaponRend;


	// Use this for initialization
	void Start () {
		WeaponRend = gameObject.GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

	}
}

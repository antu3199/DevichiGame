using UnityEngine;
using System.Collections;

public class MobilePress : MonoBehaviour {
	BoxCollider2D tester;

	PlayerAnimator PAnimator;

	// Use this for initialization
	void Start () {
		tester = GetComponent<BoxCollider2D> ();
		PAnimator = GameObject.Find ("Player").GetComponent<PlayerAnimator> ();
	}
	
	// Update is called once per frame
	void Update () {

	

		#if UNITY_IOS || UNITY_ANDROID
		if (Input.touchCount == 1)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (tester == Physics2D.OverlapPoint(touchPos))
			{
				//your code
				PlayerController.vel.x = 8.0f;
				
				
			}
			else{
				PlayerController.vel.x = 0.0f;
			}
		}
		#endif
	}
}

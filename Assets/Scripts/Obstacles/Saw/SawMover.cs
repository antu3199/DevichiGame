using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SawList{

	public Vector3 pos;
	public Vector3 dest;
	public bool TriggerOn;
	public float ThisTime;

	public SawList(Vector3 _pos, Vector3 _dest, bool _TriggerOn, float _ThisTime ){
		pos = _pos;
		dest = _dest;
		TriggerOn = _TriggerOn;
		ThisTime = _ThisTime;
	}

}


public class SawMover : MonoBehaviour {
	float x;
	float y;
	Vector3 dest;


	public float distance;


	public float angle;
	public float Speed;

	float ThisTime = 0.0f;
	public float DelayTime;

	bool TriggerOn = false;

	LineRenderer Line;

	List<SawList> myList = new List<SawList>();

	// Use this for initialization
	GameObject Player;
	PlayerController ControllerScript;

	Vector3 initPosition;

	bool Forward = true;



	void Start () {
		Player = GameObject.Find ("Player");
		ControllerScript = Player.GetComponent<PlayerController> ();
		initPosition = transform.position;

		x = distance * Mathf.Cos (angle * Mathf.Deg2Rad);
		y = (distance * Mathf.Sin (angle * Mathf.Deg2Rad)) ;
		dest = new Vector3 (x+(initPosition.x), y+(initPosition.y), 0.0f);
//		dest = new Vector3 (x, y, 0.0f);
		/*
		float Plusx;
		float Minusx;
		float Plusy;
		float Minusy;

		Vector3 LinePosition1;
		Vector3 LinePosition2;
*/
		Line = gameObject.transform.FindChild ("SawLine").gameObject.GetComponent<LineRenderer> ();
		Line.SetPosition (0, transform.position );
	//	Line.SetPosition (0, -dest);
		Line.SetPosition (1, dest);
		Line.useWorldSpace = true;


		/*
		x = distance * Mathf.Cos (angle * Mathf.Deg2Rad);
		y = distance * Mathf.Sin (angle * Mathf.Deg2Rad);
		//		dest = new Vector3 (x, y, 0.0f);
		Line = gameObject.transform.FindChild ("SawLine").gameObject.GetComponent<LineRenderer> ();
		//	Line.SetPosition (0, -dest);
		Line.SetPosition (1, dest);
		Line.useWorldSpace = true;
*/
	
	}
	
	// Update is called once per frame
	void Update () {

		if (ControllerScript.getTimeControl () == false) {

			transform.position = Vector3.MoveTowards (transform.position, dest, Speed * Time.deltaTime);
			if (transform.position == dest) {
				if (TriggerOn == false) {
					TriggerOn = true;
					ThisTime = Time.time;
				}

				if (Time.time > ThisTime + DelayTime) {

					if (Forward == true){
						dest = initPosition;
						Forward = false;
					}
					else{
						x = distance * Mathf.Cos (angle * Mathf.Deg2Rad);
						y = distance * Mathf.Sin (angle * Mathf.Deg2Rad);
						dest = new Vector3 (x+(initPosition.x), y+(initPosition.y), 0.0f);
						Forward = true;
					}
					//dest = -dest;


					TriggerOn = false;
				}
			}

		} 



	}


	void FixedUpdate(){
		if (ControllerScript.getTimeControl () == false) {
			myList.Add (new SawList(transform.position, dest,TriggerOn, ThisTime));


		} else {
			if (myList.Count > 0){

				transform.position = myList[myList.Count-1].pos;
				dest = myList[myList.Count-1].dest;

				TriggerOn = myList[myList.Count-1].TriggerOn;
				if (TriggerOn == true){
					ThisTime = myList[myList.Count-1].ThisTime;

				}


			myList.RemoveAt (myList.Count-1);

			}
		}

	}

}

using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	BlackBoxScript BBoxScript;
	GameObject Camera;
	GameManager ManagerScript;
	PlayerController PlayerScript;
	Animator MyAnimation;
	// Use this for initialization
	void Start () {
		BBoxScript = GameObject.Find ("GUI/BlackBox").GetComponent<BlackBoxScript> ();
		Camera = GameObject.Find ("Animations/Scene/Main Camera");
		ManagerScript = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		PlayerScript = GameObject.Find ("Player").GetComponent<PlayerController> ();
		MyAnimation = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		/*
		if (EmoticonEnabled == true) {
			if (EmoticonFadeIn == true){
				if (EmoticonCounter + (2.0f * Time.deltaTime) < 1.0f){
					EmoticonRend.color = new Color(1.0f, 1.0f, 1.0f, EmoticonCounter);
					EmoticonCounter += (2.0f* Time.deltaTime);
				}
				else{
					EmoticonCounter += (2.0f * Time.deltaTime);
					
					if (EmoticonCounter >= 4.0f){
						EmoticonFadeIn = false;
						EmoticonCounter = 1.0f;
					}
					
					
					
				}
			} else {
				if (EmoticonCounter - (2.0f * Time.deltaTime) > 0.0f){
					EmoticonRend.color = new Color(1.0f, 1.0f, 1.0f, EmoticonCounter);
					EmoticonCounter -= (2.0f* Time.deltaTime);
				}
				else{
					EmoticonFadeIn = true;
					EmoticonEnabled = false;
					EmoticonRend.enabled = false;
					
				}
				
			}

			
		}
*/	

	}
	public void EventNumberPlus (){
		GameManager.EventNumber ++;
	}
	public void PlayAnimation (string anim){
		MyAnimation.Play (anim, 0, 0.0f);
		GameManager.Scene = anim;
		GameManager.EventNumber = 0;
		GameManager.TextNumber = 0;



	}
	public void Note (string note){
		Debug.Log ("Note: " + note);
		
		
	}
	public void test (string msg) {

	}
	//public void Slide (string text, string name, bool left){
	public void TextNameDirection (string textnamedirection){
		string[] words = textnamedirection.Split (char.Parse("+"));
		//textnamedirection.Split (" ");

		string text = words [0];
		string name = words [1];
		bool left;
		BBoxScript.ClearTextHolder ();
		if (words [2].Equals ("left")) {
			left = true;
		} else {
			left = false;

		}
		GameManager.TextNumber ++;
			

		if (left == true) {
			BBoxScript.setLeftPictureName (name);
		} else {
			BBoxScript.setRightPictureName (name);
			BBoxScript.SwitchBoxDirection();
			//Debug.Log (left);
		}
	
		BBoxScript.Slide (text);

	}


	public void MyAnimationEventHandler (AnimationEvent animationEvent)
	{
		string stringParm = animationEvent.stringParameter;
		float floatParam = animationEvent.floatParameter;
		int intParam = animationEvent.intParameter;
		
		// Etc.
	}


	public void CamToPlayer (){
		
		CameraSmooth.SmoothFollow = true;
		CameraSmooth.FollowTarget = GameObject.Find ("Animations/Scene/PlayerCutscene").transform;
	}
	public void tester(){
		Camera.gameObject.transform.position = Vector3.zero;

	}
	public void ShowNormalizedTime(){
		Debug.Log (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime);
	}

	public void FadeIn (string colour){
		ManagerScript.CutsceneFadeIn ("Black");

	}
	public void FadeOut (string colour){
		ManagerScript.CutsceneFadeOut ("Black");
		
	}

	public void CutsceneToPlayer(){


	}
public void DecorationsNumberFade( string numfade){
		string[] words = numfade.Split (char.Parse("+"));
	
		string Number = words [0];
		string Fade = words [1];
		GameObject go =  GameObject.Find ("Animations/Scene/Decoration" + Number);
		go.GetComponent<SpriteRenderer> ().enabled = true;


	}

	public void OpenGodlyCutscene(){
		PlayerScript.TimeControlTalk ();
	}




}

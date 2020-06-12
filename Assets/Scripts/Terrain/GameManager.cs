using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool EnabledTriggers;

	public static bool CutsceneEnabled = false;
	public static string Location = "Castle";
	public static string Scene = "";
	public static int CSEventNumber = 0;
	public static int EventNumber = 0;
	public static int TextNumber = 0;
	public static bool MoveableCutscene = false;
	public static bool EnableTriggers = false;

	public static bool TimeControlTalk = false;

	public static bool FadeInAndOut = false;
	bool Whiten = true;

	public int StartEventAt = 0;


	private GameObject CutsceneBlue;
	float FadeIn = 1.0f;
	playerHealth PHealthScript;

	public GameObject BlackBox;
	BlackBoxScript BBScript;

	GameObject Player;
	PlayerAnimator PAnimator;

	float FadeColour = 0.0f;

	bool isFadingIn = false;
	bool isFadingOut = false;
	Animator MyAnimation;

	public static bool RealCutscene = false;

	//Manages non-trigger related cutscenes and global variables

	void Awake(){
		Location = Application.loadedLevelName;
		Debug.Log ("You are now entering: " + Location);
		CSEventNumber = 0;
		EventNumber = 0;
		TextNumber = 0;
	if (EnabledTriggers == true) {
			EnableTriggers = true;
		} else {
			EnableTriggers = false;
		}

		EventNumber = StartEventAt;
		RealCutscene = false;
	}

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;


		Player = GameObject.Find ("Player");
		BBScript = BlackBox.GetComponent<BlackBoxScript> ();
		PAnimator = Player.GetComponent <PlayerAnimator> ();
		RealCutscene = false;
		TimeControlTalk = false;
		if (Location == "Castle") {

			Scene = "Castle 1";
		}

	if (Location == "Field") {
		CutsceneEnabled = true;
		PlayerController.canControl = false;
		
		//PlayerController.DisableTimeControl = true;
		CutsceneBlue = GameObject.Find ("PlayerUI/CutsceneWhite");
		CutsceneBlue.GetComponent<GUITexture>().color = new Color (1f,1f, 1f, 1f);
		Scene = "Field 1";
		
			PHealthScript = GameObject.Find ("Player").GetComponent<playerHealth> ();
			PHealthScript.HideHearts ();
		
			PlayerAnimator.PerformRegularAnimations = false;
			PAnimator.SittingInstance();


		//PController.ClearAllLists();
	}
		if (Location == "FieldCutscene") {
			RealCutscene = true;
			CutsceneBlue = GameObject.Find ("PlayerUI/CutsceneWhite");
			Scene = "FieldCutscene 1";
			PHealthScript = GameObject.Find ("Player").GetComponent<playerHealth> ();
			PHealthScript.HideHearts ();
			
			PlayerAnimator.PerformRegularAnimations = false;
			CutsceneEnabled = true;
			PlayerController.canControl = false;
			CutsceneFadeOut("Black");

		}
		if (GameManager.Location == "FieldCutscene") {
			MyAnimation = GameObject.Find ("Animations/Scene").GetComponent<Animator>();
		}
		if (GameManager.Location == "CaveCutscene") {
			PHealthScript = GameObject.Find ("Player").GetComponent<playerHealth> ();
			PHealthScript.HideHearts ();

			RealCutscene = true;
			CutsceneEnabled = true;
			PlayerController.canControl = false;
			MyAnimation = GameObject.Find ("Animations/Scene").GetComponent<Animator>();
			CutsceneBlue = GameObject.Find ("PlayerUI/CutsceneWhite");
			//StartCoroutine(Tester());

			//Scene = "CaveCutscene 1";


		//	MyAnimation.Play ("CaveCutscene 1",0, 0.0f);

			Scene = "CaveCutscene 3";
			
			
			MyAnimation.Play ("CaveCutscene 3",0, 0.0f);


		//	MyAnimation.Play ("CaveCutscene 2",0, 0.0f);
		//	EventNumber = 6;
		}


	}

	public void CutsceneFadeIn (string Colour ){
		CutsceneBlue.GetComponent<GUITexture>().enabled = true;
		isFadingIn = true;
		isFadingOut = false;
		FadeIn = 0.0f;
		if (Colour == "Black") {
			FadeColour = 0.0f;

		} else if (Colour == "White") {
			FadeColour = 1.0f;
		}
		CutsceneBlue.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
	}
	public void CutsceneFadeOut (string Colour ){
		CutsceneBlue.GetComponent<GUITexture>().enabled = true;
		isFadingIn = false;
		isFadingOut = true;
		FadeIn = 1.0f;
		if (Colour == "Black") {
			FadeColour = 0.0f;
			
		} else if (Colour == "White") {
			FadeColour = 1.0f;
		}
		CutsceneBlue.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (GameManager.EventNumber);
		if (EnabledTriggers == true) {
			EnableTriggers = true;
		} else {
			EnableTriggers = false;
		}


		if (isFadingIn) {
		
			if (FadeIn + 0.015f < 1.0f){
				FadeIn += 0.015f;
				CutsceneBlue.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
			}
			else{
				EventNumber++;
				isFadingIn = false;
			}


		}

		if (isFadingOut) {
		
			if (FadeIn - 0.015f > 0){
				FadeIn -= 0.015f;
				CutsceneBlue.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
			}
			else{
				EventNumber++;
				isFadingOut = false;
				CutsceneBlue.GetComponent<GUITexture>().enabled = false;

			}
		}

		if (FadeInAndOut == true) {

			if (CutsceneBlue.GetComponent<GUITexture>().enabled == false){
				CutsceneBlue.GetComponent<GUITexture>().enabled = true;
				FadeIn = 0.0f;
				CutsceneBlue.GetComponent<GUITexture>().color = new Color (1f, 1f, 1f,FadeIn);
				Whiten = true;
			}
			if (Whiten == true){
				if (FadeIn + 0.015f < 1.0f){
					FadeIn += 0.015f;
					CutsceneBlue.GetComponent<GUITexture>().color = new Color (1f, 1f, 1f,FadeIn);
				}
				else{
					//DO SOMETHING....
					Whiten = false;

				}
			}
			if (Whiten == false){
				if (FadeIn - 0.015f > 0){
					FadeIn -= 0.015f;
					CutsceneBlue.GetComponent<GUITexture>().color = new Color (1f, 1f, 1f,FadeIn);
				}
				else{
					EventNumber++;
					TextNumber ++;
					CutsceneBlue.GetComponent<GUITexture>().enabled = false;
					FadeInAndOut = false;
				}
			}


		}

	if (Location == "Field") {
			if (Scene == "Field 1"){
				if (EventNumber == 0){

				
				if (FadeIn - 0.015f > 0){
					FadeIn -= 0.015f;
					CutsceneBlue.GetComponent<GUITexture>().color = new Color (1f, 1f, 1f,FadeIn);
				}
				else{
					CutsceneBlue.GetComponent<GUITexture>().enabled = false;
						EventNumber ++;

					//BlackBoxScript.SpeedAble = true;
						BBScript.setLeftPictureName("Devilchu");
						BBScript.setRightPictureName("Devichi");
					BBScript.Slide ("...And that is why apples are factually the superior fruit!~");
						BBScript.ActivateEmoticon("Devilchu","Happy", false);
				}
				}
				else if (EventNumber == 1){

				}

			}
		}

		if (Location == "FieldCutscene") {
			if (Scene == "FieldCutscene 1"){
				if (EventNumber == 1){
					BBScript.Slide ("Hey you!");
					BBScript.setRightPictureName ("Leafree");
					BBScript.SwitchBoxDirection();
					EventNumber++;
					//TextNumber = EventNumber;
				}
				if (EventNumber == 9){
					Scene = "FieldCutscene 2";
					MyAnimation.Play ("FieldLeafree 2", 0, 0.0f);
					EventNumber++;
				}
			}
			if (Scene == "FieldCutscene 2"){
				if(EventNumber == 15){
					Scene = "FieldCutscene 3";
					EventNumber = 0;
					TextNumber = 0;

					MyAnimation.Play ("FieldLeafree 3", 0, 0.0f);
				}
			}

		}

	

	}
	public void Reset(){
		CutsceneEnabled = false;
		Location = "Castle";
		Scene = "";
	}
	IEnumerator Tester(){
		yield return new WaitForSeconds(2.0f);
		MyAnimation.Play ("CaveCutscene 1",0, 0.0f);

	}
}

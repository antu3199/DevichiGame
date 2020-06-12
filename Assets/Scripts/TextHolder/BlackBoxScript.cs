using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackBoxScript : MonoBehaviour
{

	public static bool FinishedText = false;

	 bool SpeedAble = false;
	//int DialogueNumber = 1;
	int finishedCounter = 0;

	public Sprite DevichiPortrait;
	public Sprite DevilchuPortrait;
	public Sprite SignPortrait;
	public Sprite LeafreePortrait;
	public Sprite TemporiaPortrait;

	public float letterPause = 0.01f;
	public float longerPause = 0.2f;
	float SentencePause = 1.0f;
	public AudioClip sound;
	public static string message;
	float ThisTime;
	bool BreakLoop = false;
	string InitialMessage = "Tester";
	bool top;
	bool isSlided = false;
	bool isSliding = false;
	bool isSlidingBack = false;
	public float SlideSpeed;
	public GameObject Hearts;
	public GameObject TextHolder;
	Text TTextObject;

	public bool isLeftPicture = false;
	public GameObject PictureLeft;
	public GameObject PictureRight;
	public GameObject PictureNameLeft;
	public static string LeftPictureString;
	public GameObject PictureNameRight;
	public static string RightPictureString;
	bool Skip = false;

	public static bool ChoiceEnabled = false;

	int Choice = 0;
	int SubmittedChoice = 0;

	public GameObject ChoiceLeft;
	public GameObject ChoiceRight;
	public GameObject ChoiceCursor;


	bool isOpen = false;

GameObject Player;
	GameObject Devilchu;


	PlayerAnimator PAnimator;


	int CharIndex = 0;

	int HighLightCount = 0;


	GameObject Emoticon;
	SpriteRenderer EmoticonRend;


	bool EmoticonEnabled = true;
	bool EmoticonFadeIn = true;
	float EmoticonCounter = 1.0f;

	public Sprite[] EmoticonSprites; 

	GameObject MainCamera;
	CameraAnimator CameraAnimatorScript;

	Animator MyAnimation;
	Transform CutsceneCamera;

	GameManager ManagerScript;

	GameObject GUI;

	bool InitGodTextBox = true;

	float GBoxAlpha = 0.0f;

	void Awake(){

		SpeedAble = false;
		FinishedText = false;
		message = "";
		LeftPictureString = "";
		RightPictureString = "";
		ChoiceEnabled = false;
	}

	//Create animation prefab and link all sprites together... 

	// Use this for initialization
	void Start ()
	{
		GUI = GameObject.Find ("GUI");

		DisableTextHolder ();
		//GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 1f, 0.5f);
		TTextObject = TextHolder.GetComponent<Text> ();
		setLeftPictureName ("Devichi");
		setRightPictureName ("The Masked Man");

		//	EnableTextHolder ();
		//	UpdateBoxDirection ();

		//message = "Bagels are tasty. I Like them!";
		message = "";
		TTextObject.GetComponent<Text> ().text = "";


		if (ChoiceEnabled == false) {
			HideChoices ();
		}

		Player = GameObject.Find ("Player");
		PAnimator = Player.GetComponent<PlayerAnimator> ();

		Emoticon = Player.transform.FindChild ("Emoticon").gameObject;
		EmoticonRend = Emoticon.GetComponent<SpriteRenderer> ();

		Devilchu = GameObject.Find ("Devilchu");

		MainCamera = GameObject.Find ("Main Camera");
		CameraAnimatorScript = MainCamera.GetComponent<CameraAnimator> ();

		if (GameManager.Location == "FieldCutscene" || GameManager.Location == "CaveCutscene") {
			MyAnimation = GameObject.Find ("Animations/Scene").GetComponent<Animator>();


		}
		ManagerScript = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	//	GetComponent<SpriteRenderer> ().enabled = true;
	}

	//0.05 right, -0.05 left
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKey (KeyCode.Q)){
			MyAnimation.StopPlayback();
			MyAnimation.enabled = false;
			
		}

		if (GameManager.TimeControlTalk == true) {
			if (InitGodTextBox == true){
				GBoxAlpha = 0.0f;
		

			
				//if (GetComponent<SpriteRenderer>() == true){
					GetComponent<SpriteRenderer> ().enabled = false;
			//	}
			//	if (TTextObject.color != new Color (0.0f,0.0f,0.0f,1.0f)){
			//	TTextObject.color = new Color (0.0f,0.0f,0.0f,1.0f);

				//TTextObject.color = new Color (0.0f,0.0f,0.0f,GBoxAlpha);
			//	PictureNameLeft.GetComponent<Text>().color = new Color (0.0f,0.0f,0.0f,GBoxAlpha);
			//	PictureLeft.GetComponent<SpriteRenderer>().color = new Color (1.0f,1.0f,1.0f,GBoxAlpha);

			//	}
				setRightPictureName ("Temporia: The Legendary Weapon");
				
			//	if (GUI.GetComponent<Canvas>().sortingLayerName != "TextBox"){
					GUI.GetComponent<Canvas>().sortingLayerName = "TextBox";

				PictureLeft.GetComponent<SpriteRenderer>().sortingLayerName= "TextBox";
			//	}



				InitGodTextBox = false;


			}
			/*
			if (GBoxAlpha < 1.0f){
				TTextObject.color = new Color (0.0f,0.0f,0.0f,GBoxAlpha);
				PictureNameLeft.GetComponent<Text>().color = new Color (0.0f,0.0f,0.0f,GBoxAlpha);
				PictureLeft.GetComponent<SpriteRenderer>().color = new Color (1.0f,1.0f,1.0f,GBoxAlpha);

			}
*/

			if (isLeftPicture == true && PictureNameLeft.GetComponent<Text> ().text != "Temporia: The Legendary Weapon"){
				gameObject.GetComponent<SpriteRenderer>().enabled = true;
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "TextBox";
				TTextObject.color = new Color (1.0f,1.0f,1.0f,1.0f);
				TextHolder.GetComponent<RectTransform>().sizeDelta = new Vector2 (467.8f, 115f);
			}
			else{
				gameObject.GetComponent<SpriteRenderer>().enabled = false;
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
				TTextObject.color = new Color (0.0f,0.0f,0.0f,1.0f);
				TextHolder.GetComponent<RectTransform>().sizeDelta = new Vector2 (467.8f, 120f);

			}





		}


		//Debug.Log (GameManager.Scene);
		//Debug.Log (FinishedText);
	/*
		Useful Functions:
		Slide("Text"); Slides open the textholder and puts that text initially
		setLeft/RightPicture("Name"); Sets name bar eg "Devichi, Devilchu"
		


*/
		//GetComponent<SpriteRenderer> ().enabled = true;
		//Speeds up text speed if hold Z
		if (PlayerController.canControl == true && isSlided && GameManager.CutsceneEnabled == false) {
			SpeedAble = false;
			
		} else {
			SpeedAble = true;
		}

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


		if (GameManager.CutsceneEnabled == true) {
			if (Input.GetKey (KeyCode.Z) && FinishedText == false && isSlided == true) {
				letterPause = 0.001f;
				longerPause = 0.005f;
			}
			if (Input.GetKeyUp (KeyCode.Z) && letterPause != 0.05f) {
				letterPause = 0.04f;
				longerPause = 0.15f;
			}

		

			if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown ("enter") || Input.GetKeyDown (KeyCode.Return)) {
//				Debug.Log ("test");
				Skip = true;
				if (GameManager.Location == "Castle"){
					if (GameManager.Scene == "Castle 1"){
						GameManager.EventNumber = 4;
					}

				}
				else if (GameManager.Location == "Field"   && GameManager.EventNumber >= 1){
					if (GameManager.Scene == "Field 1"){
						GameManager.EventNumber = 18;
					}

					if (GameManager.Scene == "Field 2" && GameManager.CutsceneEnabled == true){
						GameManager.EventNumber = 3;
					}

				}


				if (GameManager.Location == "CaveCutscene"){
					if (GameManager.Scene == "CaveCutscene 3"){

						GameObject CutscenePlayer = GameObject.Find ("Animations/Scene/PlayerCutscene");
						CutscenePlayer.transform.position = new Vector3 (8.5f,-1069.988f,0.0f);
						MainCamera.transform.position = new Vector3(8.5f,-1063.83f,-10f);
						CutsceneToPlay();
						GameManager.TextNumber = 5;
						GameManager.EventNumber = 5;
						//Disable Weapon
						GameObject.Find ("Player/Animation/Body/LArm/Weapon").GetComponent<SpriteRenderer>().enabled = false;
					}

				}

				FinishedText = true;
				ClearTextHolder ();
			}

	
			//Choice choose 
			if (ChoiceEnabled == true){
				if (Input.GetKeyDown (KeyCode.LeftArrow)){
					if (Choice == 2){
						Choice = 1;
						UpdateChoices ();
					}
				}
				if (Input.GetKeyDown (KeyCode.RightArrow)){
					if (Choice == 1){
						Choice = 2;
						UpdateChoices ();
					}
				}
				if (Input.GetKeyDown (KeyCode.Z)){
					SubmittedChoice = Choice;
					HideChoices ();
					ChoiceEnabled = false;
					GameManager.EventNumber ++;
				}
				
			}

		}

		//Dialoge advance
		if (GameManager.Location == "Castle"){
			if (GameManager.Scene == "Castle 1") {
				if (FinishedText == true) {
				if (GameManager.EventNumber == 0){
						SwitchBoxDirection ();
						ChangeText ("Indeed I have.");
						FinishedText = false;
					GameManager.EventNumber ++;
				}
					else if (GameManager.EventNumber == 1){

						ChangeText ("However, I have not yet written a script. So you must wait!");
						FinishedText = false;
						GameManager.EventNumber ++;
				}
						else if (GameManager.EventNumber == 2){
						SwitchBoxDirection ();
						ChangeText ("NOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!");
						FinishedText = false;
							GameManager.EventNumber ++;
					}
							else if (GameManager.EventNumber == 3){
						SwitchBoxDirection ();
						ChangeText ("Well, onto the story!");
						FinishedText = false;
								GameManager.EventNumber ++;
					}
								else if (GameManager.EventNumber == 4){
									
						StopCoroutine (TypeText ());
						ClearTextHolder ();
						SlideBack ();
								
					}


				}
			}
		}
		if (GameManager.Location == "Field"){
			 if (GameManager.Scene == "Field 1"){

				if (FinishedText == true){
					
					if (GameManager.EventNumber == 1){
						
						SwitchBoxDirection();
					//	ChangeText ("(What happened? Was that just a dream? No, it was much too vivid for a dream... It was as if I... <color=#ff0000ff>reversed time</color> somehow)");
						ActivateEmoticon("Devichi", "Exclamation", false);

						ChangeText ("(What happened? Was that just a dream? No, it was much too vivid for a dream... It was as if I... <r<e<v<e<r<s<e<d <t<i<m<e< somehow)");
						HighLightCount = 13;
						FinishedText = false;

						GameManager.EventNumber ++;
					}
					else if (GameManager.EventNumber == 2){
						SwitchBoxDirection();
						ChangeText ("Uhm, HELLOOOO!?!?!? Are you even listening?");
						FinishedText = false;
						
						GameManager.EventNumber ++;
					}
					else if (GameManager.EventNumber == 3){
						SwitchBoxDirection();
						ChangeText ("Listen, I had a >v>i>s>i>o>n from the future that you... died.");
						FinishedText = false;
						
						GameManager.EventNumber ++;
					}
					else if (GameManager.EventNumber == 4){

						ChangeText ("And in that >v>i>s>i>o>n, there was a voice inside my head, granting me the {p{o{w{e{r to change that future...");
						FinishedText = false;
						
						GameManager.EventNumber ++;
					}
					else if (GameManager.EventNumber == 5){
						SwitchBoxDirection();
						ChangeText ("Stop talking nonsense! Just try to enjoy our nice picnic together~ I haven't even started packing for our journey tomorrow!");
						FinishedText = false;
						
						GameManager.EventNumber ++;
					}

				else if (GameManager.EventNumber == 6){
					GameManager.EventNumber ++;
					GameManager.FadeInAndOut = true;
				
				}
				else if (GameManager.EventNumber == 8){
					SwitchBoxDirection();
					ChangeText ("Wait no, don't go! I don't want to risk you being in danger!");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 9){
					SwitchBoxDirection();
					ChangeText ("Hmmph, I can take care of myself!");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 10){
					if (SubmittedChoice == 0){

					SwitchBoxDirection();
					}
					ChangeText ("Actually,");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 11){
					ChoiceEnabled = true;
					GameManager.EventNumber ++;
					Choice = 1;
					SubmittedChoice = 0;
					ShowChoices();
					UpdateChoices ();
				}
				else if (GameManager.EventNumber == 13 && SubmittedChoice == 1){
					ChangeText ("(What am I thinking? If she goes, she'll get killed! You only YOLO once!)");
					FinishedText = false;
					//SwitchBoxDirection();
					GameManager.EventNumber = 10;
				}

				else if (GameManager.EventNumber == 13 && SubmittedChoice == 2){
					ChangeText ("Don't go, I don't want to risk you getting hurt.");
					FinishedText = false;
					Choice = 1;
					SubmittedChoice = 0;
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 14){
					SwitchBoxDirection();
					ChangeText ("But I was really looking forward to spending time with you, brother...");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 15){
					SwitchBoxDirection();
					ChangeText ("Next time, okay?");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 16){
					SwitchBoxDirection();
					ChangeText ("Fine...");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 17){
					SwitchBoxDirection();
					ChangeText ("Alright, time to get ready and warm up for my grand adventure!");
					FinishedText = false;
					
					GameManager.EventNumber ++;
				}
				else if (GameManager.EventNumber == 18){
					StopCoroutine (TypeText ());
				//	ClearTextHolder ();
					SlideBack ();
					PAnimator.SitToStandInstance();
					GameManager.EventNumber ++;
				}

				}
				}


				if (GameManager.Scene == "Field Extra 1"){
				if (FinishedText == true){
					if (GameManager.EventNumber == 0){

						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";

					
					}
				}
			}
			else if (GameManager.Scene == "Field Extra 2"){
				if (FinishedText == true){
					if (GameManager.EventNumber == 0){
					
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
						
						
					}
				}
			}
			if (GameManager.Scene == "Field 2"){
				if (FinishedText == true){
					if (GameManager.EventNumber == 2){


						ChangeText ("It was as if... I caught a >g>l>i>m>p>s>e >o>f >t>h>e >f>u>t>u>r>e... As if someone... or something is {g{u{i{d{i{n{g {m{e....");
						FinishedText = false;
						
						GameManager.EventNumber ++;


						
						
					}
					else if (GameManager.EventNumber == 3){
						
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
						PlayerController.canControl = true;
						GameManager.CutsceneEnabled = false;

					}
				}

			}
			if (GameManager.Scene == "Field Died"){
				if (FinishedText == true){
					StopCoroutine (TypeText ());
				//	ClearTextHolder ();
					SlideBack ();
					GameManager.EventNumber = -1;
					GameManager.Scene = "";
				}
			}
			if (GameManager.Scene == "Sign 1"     ){
				if (FinishedText == true){
					StopCoroutine (TypeText ());
				//	ClearTextHolder ();
					SlideBack ();
					GameManager.EventNumber = -1;
					GameManager.Scene = "";
				}
			}

			if (GameManager.Scene == "Sign 2"){
			
				if (FinishedText == true){
					
					if (GameManager.EventNumber == 0){
						
						ChangeText ("They may look cute on the outside, but they're actually pretty <E<V<I<L<!<!<!... I promise!");
						FinishedText = false;
						GameManager.EventNumber ++;
					}
					else if (GameManager.EventNumber == 1){
						
						StopCoroutine (TypeText ());
				//		ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
					}
				}
			}

			if (GameManager.Scene == "Sign 3"){
			
				if (FinishedText == true){

					if (GameManager.EventNumber == 0){
						
						ChangeText ("Although it does less damage than a ground attack, it has great maneuverability, so use it a bunch!");
						FinishedText = false;
						GameManager.EventNumber ++;
					}
					
					else if (GameManager.EventNumber == 1){
						
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
					}
				}
			}
				
			if (GameManager.Scene == "Sign 4"){
				if (FinishedText == true){
					if (GameManager.EventNumber == 0){

						ChangeText ("Also note that monsters with a glowing >a>u>r>a often damage you on contact. Please be careful!");
						FinishedText = false;
						
						GameManager.EventNumber ++;
						
					}
					
					else if (GameManager.EventNumber == 1){
					
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
					}
				}
			}
			if (GameManager.Scene == "Sign 5"){
				
				if (FinishedText == true){
					
					if (GameManager.EventNumber == 0){
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
					}
				
				}
			}
			if (GameManager.Scene == "Sign 6"){
				if (FinishedText == true){
					if (GameManager.EventNumber == 0){
						
						ChangeText ("You CAN just climb the wall, but I can {g{u{a{r{a{n{t{e{e that wall-jumping will be a useful technique.");
						FinishedText = false;
						
						GameManager.EventNumber ++;
						
					}
					
					else if (GameManager.EventNumber == 1){
						
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
					}
				}
			}
			if (GameManager.Scene == "Sign 7"){
				
				if (FinishedText == true){
					
					if (GameManager.EventNumber == 0){
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						SlideBack ();
						GameManager.EventNumber = -1;
						GameManager.Scene = "";
					}
					
				}
			}
			if (GameManager.Scene == "Sign 8"){
				if (FinishedText == true){
					if (GameManager.EventNumber == 0){
						setRightPictureName("Devichi");
						SwitchBoxDirection();

						ChangeText ("Uhh... so tell me, why are there floating mechanical death machines here again?");
						FinishedText = false;
						
						GameManager.EventNumber ++;
						
					}
					
					else if (GameManager.EventNumber == 1){
						SwitchBoxDirection();
						
						ChangeText ("...I don't know! Just deal with it~ *puts on sunglasses*");
						FinishedText = false;
						
						GameManager.EventNumber ++;
					
					}
					else if (GameManager.EventNumber == 2){
					StopCoroutine (TypeText ());
				//	ClearTextHolder ();
					SlideBack ();
					GameManager.EventNumber = -1;
					GameManager.Scene = "";
					}

				}
			}

			if (GameManager.Scene == "Field Leafree"){
			
				if (FinishedText == true ){
					if (GameManager.EventNumber == 1){

						GameManager.FadeInAndOut = true;
					}
					if (GameManager.EventNumber == 2){
						
						Application.LoadLevel ("FieldCutscene");
					}
				}

			}




			}

if (GameManager.Location == "FieldCutscene") {

			if (GameManager.Scene == "FieldCutscene 1"){
//			

				if (GameManager.EventNumber == 0){
				MyAnimation.Play ("Leafree Flip",0,0.0f);

					
				}
			 if (GameManager.EventNumber == 2){
					if (MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){

						if (FinishedText == true ){
							GameManager.TextNumber = 0;
							FinishedText = false;
							GameManager.EventNumber++;
							GameManager.TextNumber ++;
							ChangeText ("How's it going, Devichi?");


						}
					}
				}

			


				if (FinishedText == true){
			
				if (GameManager.TextNumber == 1){
						//FinishedText = false;
						GameManager.FadeInAndOut = true;
					
				}
			else	if (GameManager.TextNumber == 2){
						FinishedText = false;
					SwitchBoxDirection();
					setLeftPictureName("Devichi");
					ChangeText ("That's Leafree, the Hero of the Leafs and also my childhood friend.");
					GameManager.TextNumber ++;

			
				}
			else	if (GameManager.TextNumber == 3){
						FinishedText = false;
						ChangeText ("...But in that vision I had earlier, he-");
						GameManager.TextNumber ++;
				}
					else if (GameManager.TextNumber == 4){

						SlideBack();
						StopCoroutine (TypeText ());
					//	ClearTextHolder ();
						GameManager.TextNumber ++;
						GameManager.EventNumber++;
						//DRAGON ENTERS
				


					}




			}


			}
			if (GameManager.Scene == "FieldCutscene 3"){
			
				if (FinishedText == true && MyAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f){
					GameManager.Scene = "FieldCutscene 4";
					GameManager.EventNumber = 0;
					GameManager.TextNumber = 0;
					SlideBack();
					StopCoroutine (TypeText ());
				//	ClearTextHolder ();
					

					//MyAnimation.Play ("Nothing", 0, 0.0f);
					//CameraSmooth.SmoothFollow = true;
				//	CameraSmooth.FollowTarget = GameObject.Find ("Animations/Scene/PlayerCutscene").transform;
					MyAnimation.Play ("FieldLeafree 4", 0, 0.0f);
				//	MainCamera.transform.position = CutsceneCamera.position;
				}




			}

			if (GameManager.Scene == "FieldCutscene 4"){

				if (GameManager.EventNumber == 1){
					GameManager.EventNumber ++;
					Application.LoadLevel ("CaveCutscene");

				}
			}




		}

		if (GameManager.Location == "CaveCutscene") {
			Debug.Log ("EventNumber: " + GameManager.EventNumber + " TextNumber: " + GameManager.TextNumber);

			if (GameManager.Scene == "CaveCutscene 1"){
				
		

			if (FinishedText == true){
			
				if ( GameManager.TextNumber == 1){

					

					
					SlideBack();
					StopCoroutine (TypeText ());
					ClearTextHolder ();
					GameManager.TextNumber++;

				}
				if (GameManager.TextNumber == 4){

					GameManager.TextNumber++;
					SlideBack();
					StopCoroutine (TypeText ());
					//ClearTextHolder ();
				}

			}
			if (GameManager.EventNumber == 2){
				GameManager.FadeInAndOut = true;
				GameManager.EventNumber++;
			//	Slide 


			}
			if (GameManager.EventNumber == 4){
				Slide ("I remember this sensation... this happened before...not too long ago...");
				GameManager.EventNumber++;
				GameManager.TextNumber++;
			}
			}
			/*
			if (GameManager.EventNumber == 7){
				MyAnimation.Play ("CaveCutscene 2", 0, 0.0f);
				GameManager.EventNumber ++;

			}
*/
			if (GameManager.Scene == "CaveCutscene 2"){

				if (GameManager.EventNumber == 1){
					PlayAnimation("CaveCutscene 3");
					
				}
			}
		
			if (GameManager.Scene == "CaveCutscene 3"){
				if (FinishedText == true){

					if (GameManager.TextNumber == 1 ){
						//PlayAnimation ("CaveCutscene 4");
						GameManager.TextNumber ++;
						FinishedText = false;
						ChangeText ("But what am I going to do now? I lost my weapon and I'm stuck in this cave long from the surface.");
					}
					else if (GameManager.TextNumber == 2){
						FinishedText = false;
						SlideBack();
						StopCoroutine (TypeText ());
						ClearTextHolder ();
						GameManager.TextNumber ++;
						GameManager.FadeInAndOut = true;

					}
					else if (GameManager.TextNumber == 4 && GameManager.FadeInAndOut == false){
						FinishedText = false;
						SlideBack();
						//StopCoroutine (TypeText ());
						//ClearTextHolder ();
						GameObject.Find ("Player/Animation/Body/LArm/Weapon").GetComponent<SpriteRenderer>().enabled = false;


						GameManager.TextNumber ++;
						CutsceneToPlay();
					

					}

				}
				if (GameManager.EventNumber == 3){
					GameManager.EventNumber++;
					Slide ("(Calm down, I know what happens next...)");
					
					
				}

			}

			if (GameManager.Scene == "OntoCaveCutscene 4"){
				if (GameManager.EventNumber == 1){
				
					ManagerScript.CutsceneFadeOut ("Black");
				

					PlayToCutscene("CaveCutscene 4");

					Debug.Log ("Testererrer");

					GameManager.EventNumber++;
				}
				else if (GameManager.EventNumber == 3){


				}

			}
			if (GameManager.Scene == "CaveCutscene 4" )
			{


				if (FinishedText == true){
					if (GameManager.TextNumber == 1){
					//	SwitchBoxDirection ();
					//	setRightPictureName("Temporia: The Legendary Weapon");
						ChangeText ("The legendary weapon Temporia is an instrument of destruction that supposedly grants its wielder the gift of immortality.");
						GameManager.TextNumber++;
						FinishedText = false;
					}
					else if (GameManager.TextNumber == 2){

					

						//Slide ("Testererererererererer");


						//SwitchBoxDirection();

						ChangeText ("This is my second time encountering it, but when I last used it, there seemed to be nothing special about it.");
						GameManager.TextNumber++;
						FinishedText = false;
					}
					else if (GameManager.TextNumber == 3){
						ChangeText ("This time feels different though, I feel as though it is calling for me...");
						GameManager.TextNumber++;
						FinishedText = false;
					}
					else if (GameManager.TextNumber == 4){
						ChangeText ("The very air around me seems to be pulsating me towards the glow of the Temporia.");
						GameManager.TextNumber++;
						FinishedText = false;
					}
					else if (GameManager.TextNumber == 5){
						SlideBack();
						StopCoroutine (TypeText ());

						GameManager.TextNumber++;
						FinishedText = false;
						PlayAnimation ("CaveCutscene 5");

					}

				}
				/*
				The legendary weapon Temporia is an instrument of destruction that
				supposedly grants its weilder the gift of immortality. 

				This is my second time encountering it, but when I used it, there was nothing special about it.

				This time feels different though, I feel as though it is calling for me...

				*/
				//PLay animb

			}
			if (GameManager.Scene == "CaveCutscene 5"){
				if (GameManager.EventNumber == 3){
					setRightPictureName("Temporia: The Legendary Weapon");
					Slide ("Hi, what's up? ");
					SwitchBoxDirection();
					GameManager.EventNumber++;
					GameManager.TextNumber ++;
					FinishedText = false;
				}
				if (FinishedText == true){
					if (GameManager.TextNumber == 1){

					}

				}


			}



		}




	
	}



	void FixedUpdate ()
	{

		if (isSliding == true) {
			if (transform.localPosition.y - SlideSpeed > 190f) {

			
				transform.Translate (Vector3.up * -SlideSpeed);
				Hearts.transform.Translate (Vector3.up * -SlideSpeed * 0.05f);
			} else {
			
				isSliding = false;
				isSlided = true;
				ChangeText (InitialMessage);

				//Do something
			}
		}

		if (isSlidingBack == true) {
			if (transform.localPosition.y + SlideSpeed < 300f) {
				
				
				transform.Translate (Vector3.up * SlideSpeed);
				Hearts.transform.Translate (Vector3.up * SlideSpeed * 0.05f);
			} else {
				isSlidingBack = false;
				isSlided = false;
				isOpen = false;
				FinishedText = false;

				GetComponent<SpriteRenderer> ().enabled = true;
				ClearTextHolder ();
				if (GameManager.Scene == "Castle 1" && GameManager.EventNumber == 4) {
					//LOAD LEVEL
					GameManager.EventNumber = -1;
					GameManager.CutsceneEnabled = false;
				//	GameManager.Scene = "";

					Application.LoadLevel ("Field");
				
				}

				if (GameManager.Location == "FieldCutscene"){
					if (GameManager.Scene == "FieldCutscene 1"){

					
					if (GameManager.EventNumber == 5){
						//Play Sound;
							Debug.Log ("test");
							MyAnimation.Play("Field Leafree 1", 0 , 0.0f);
							GameManager.EventNumber ++;
					}
					}
				}
				if (GameManager.Location == "CaveCutscene" ){
					GameManager.EventNumber ++;

				}

		
			}
		}

	}

	public void SwitchBoxDirection ()
	{
		if (isLeftPicture == true) {
			isLeftPicture = false;
		} else {
			isLeftPicture = true;

		}



		UpdateBoxDirection ();

	}

	public void UpdateBoxDirection ()
	{
		if (isLeftPicture == true) {
			PictureLeft.GetComponent<SpriteRenderer> ().enabled = true;
			PictureRight.GetComponent<SpriteRenderer> ().enabled = false;
			
			PictureNameRight.GetComponent<Text> ().enabled = false;
			PictureNameLeft.GetComponent<Text> ().enabled = true;
			TextHolder.transform.localPosition = new Vector3 (0.04f, TextHolder.transform.localPosition.y, TextHolder.transform.localPosition.z);


			ChoiceLeft.transform.localPosition = new Vector3 (-0.03306865f,ChoiceLeft.transform.localPosition.y,ChoiceLeft.transform.localPosition.z);
			ChoiceRight.transform.localPosition = new Vector3 (0.1243379f,ChoiceRight.transform.localPosition.y,ChoiceRight.transform.localPosition.z);

			ChoiceCursor.transform.localPosition =  new Vector3 (ChoiceLeft.transform.localPosition.x - 0.08f, ChoiceCursor.transform.localPosition.y,ChoiceCursor.transform.localPosition.z);


			
		} else {
			PictureLeft.GetComponent<SpriteRenderer> ().enabled = false;
			PictureRight.GetComponent<SpriteRenderer> ().enabled = true;
			
			PictureNameRight.GetComponent<Text> ().enabled = true;
			PictureNameLeft.GetComponent<Text> ().enabled = false;
			TextHolder.transform.localPosition = new Vector3 (-0.04f, TextHolder.transform.localPosition.y, TextHolder.transform.localPosition.z);

			ChoiceLeft.transform.localPosition = new Vector3 ( -0.1243409f,ChoiceLeft.transform.localPosition.y,ChoiceLeft.transform.localPosition.z);
			ChoiceRight.transform.localPosition = new Vector3 ( 0.03306865f,ChoiceRight.transform.localPosition.y,ChoiceRight.transform.localPosition.z);

			ChoiceCursor.transform.localPosition =  new Vector3 (ChoiceLeft.transform.localPosition.x - 0.08f, ChoiceCursor.transform.localPosition.y,ChoiceCursor.transform.localPosition.z);

		}
	}
	//Neutral Y = 300f
	// Slided = 185f
	//Most important function
	public void Slide (string init)
	{


		if (isOpen == true) {
			transform.localPosition = new Vector3 (transform.localPosition.x,300f,transform.localPosition.z);

			Hearts.transform.localPosition = new Vector3 (Hearts.transform.localPosition.x,-0.65f, Hearts.transform.localPosition.z); 

			isSlidingBack = false;
			isSliding = false;
			isSlided = false;
			ClearTextHolder();
		}

		EnableTextHolder ();

		UpdateBoxDirection ();
		isSliding = true;
		InitialMessage = init;
		FinishedText = false;
		isOpen = true;

	}

	//Go back
	public void SlideBack ()
	{
		isSliding = false;
		isSlidingBack = true;
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	//Sets Left picture name
	public void setLeftPictureName (string name)
	{
		LeftPictureString = name;
		PictureNameLeft.GetComponent<Text> ().text = name;
		if (name == "Devichi") {
		
			PictureLeft.GetComponent<SpriteRenderer> ().sprite = DevichiPortrait;
		} else if (name == "Devilchu") {

			PictureLeft.GetComponent<SpriteRenderer> ().sprite = DevilchuPortrait;
		} else if (name == "Sign-san") {
			
			PictureLeft.GetComponent<SpriteRenderer> ().sprite = SignPortrait;
		} else if (name == "Leafree") {
			
			PictureLeft.GetComponent<SpriteRenderer> ().sprite = LeafreePortrait;
		} else if (name == "Temporia: The Legendary Weapon") {
			PictureLeft.GetComponent<SpriteRenderer> ().sprite = TemporiaPortrait;
			PictureLeft.GetComponent<SpriteRenderer>().sortingLayerName = "TextBox";
			PictureNameLeft.GetComponent<Text> ().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			PictureNameLeft.transform.localPosition = new Vector3 ( -0.1799984f, -0.26f, 42.083f);

		}
		else if (!(name == "Temporia: The Legendary Weapon")){
			PictureNameLeft.GetComponent<Text> ().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		if (name != "Temporia: The Legendary Weapon" && GameManager.TimeControlTalk == false) {
			PictureLeft.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			PictureNameLeft.GetComponent<Text> ().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			PictureNameLeft.transform.localPosition = new Vector3 ( -0.1799984f, -0.2129993f, 42.083f);
		}

		
	}

	public void setRightPictureName (string name)
	{
		RightPictureString = name;
		PictureNameRight.GetComponent<Text> ().text = name;
		if (name == "Devichi") {
			PictureRight.GetComponent<SpriteRenderer>().sprite = DevichiPortrait;
			
		} else if (name == "Devilchu") {
			PictureRight.GetComponent<SpriteRenderer>().sprite = DevilchuPortrait;
		}
		else if (name == "Sign-san") {
			PictureRight.GetComponent<SpriteRenderer>().sprite = SignPortrait;
		}	else if (name == "Leafree") {
			PictureRight.GetComponent<SpriteRenderer>().sprite = LeafreePortrait;
		}
		else if (name == "Temporia: The Legendary Weapon") {
			PictureRight.GetComponent<SpriteRenderer> ().sprite = TemporiaPortrait;
			PictureRight.GetComponent<SpriteRenderer>().sortingLayerName = "TextBox";
			PictureNameRight.GetComponent<Text> ().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
			PictureNameRight.transform.localPosition = new Vector3 ( 0.18f, -0.26f, 42.083f);
			
		}

		if (name != "Temporia: The Legendary Weapon" && GameManager.TimeControlTalk == false) {
			PictureRight.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
			PictureNameRight.GetComponent<Text> ().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			PictureNameRight.transform.localPosition = new Vector3 ( 0.18f, -0.2129993f, 42.083f);
		}

	}
	//Clear evertthing about the textholder (But doesn't slide back)

	public void ClearTextHolder ()
	{
		StopCoroutine (TypeText ());
		isLeftPicture = true;
		UpdateBoxDirection ();
		FinishedText = true;
		finishedCounter = 0;
		message = "";
		BreakLoop = true;
		SpeedAble = false;
		TTextObject.GetComponent<Text> ().text = "";
		DisableTextHolder ();



	}

	//Advances text
	public void ChangeText (string text)
	{
		if (isOpen == true) {
		
			//ClearTextHolder();
		
		}
		message = "";
		FinishedText = false;
		BreakLoop = false;
		finishedCounter = 0;
		message = text;
		TTextObject.GetComponent<Text> ().text = "";



		StartCoroutine (TypeText ());
	}
	//Check if the textholder is already open
	public bool getisOpen (){
		return isOpen;
	}

	//Disables Text holder
	void DisableTextHolder ()
	{
		GetComponent<SpriteRenderer> ().enabled = false;
		PictureLeft.GetComponent<SpriteRenderer> ().enabled = false;
		PictureRight.GetComponent<SpriteRenderer> ().enabled = false;
		PictureNameLeft.GetComponent<Text> ().enabled = false;
		PictureNameRight.GetComponent<Text> ().enabled = false;
	}

	//Enables text holder
	void EnableTextHolder ()
	{
		GetComponent<SpriteRenderer> ().enabled = true;
		PictureLeft.GetComponent<SpriteRenderer> ().enabled = true;
		PictureRight.GetComponent<SpriteRenderer> ().enabled = true;
		PictureNameLeft.GetComponent<Text> ().enabled = true;
		PictureNameRight.GetComponent<Text> ().enabled = true;
	}

	//Choice functions
		void HideChoices(){
		ChoiceLeft.GetComponent<Text>().enabled = false;
		ChoiceRight.GetComponent<Text>().enabled = false;
		ChoiceCursor.GetComponent<SpriteRenderer>().enabled = false;
	}
	void ShowChoices(){
		ChoiceLeft.GetComponent<Text>().enabled = true;
		ChoiceRight.GetComponent<Text>().enabled = true;
		ChoiceCursor.GetComponent<SpriteRenderer>().enabled = true;
	}
	void UpdateChoices(){
		if (Choice == 1) {
			ChoiceCursor.transform.localPosition = new Vector3 (ChoiceLeft.transform.localPosition.x - 0.08f, ChoiceCursor.transform.localPosition.y, ChoiceCursor.transform.localPosition.z);
		} else if (Choice == 2) {
			ChoiceCursor.transform.localPosition =  new Vector3 (ChoiceRight.transform.localPosition.x - 0.08f, ChoiceCursor.transform.localPosition.y,ChoiceCursor.transform.localPosition.z);
		}


	}

	public void ActivateEmoticon( string name, string emoticon, bool truecutscene){
		EmoticonFadeIn = true;
		EmoticonEnabled = false;
		EmoticonRend.enabled = false;



		if (name == "Devichi") {
			if (truecutscene == false){
				Emoticon = Player.transform.FindChild ("Emoticon").gameObject;
				EmoticonRend = Emoticon.GetComponent<SpriteRenderer> ();
			}
			else{
				Emoticon = GameObject.Find ("Animations/Scene/PlayerCutscene/Player_Emotion");
				EmoticonRend = Emoticon.GetComponent<SpriteRenderer> ();

			}
	
		} else if (name == "Devilchu") {
			Emoticon = Devilchu.transform.FindChild ("Emoticon").gameObject;
			EmoticonRend = Emoticon.GetComponent<SpriteRenderer> ();
		}


		EmoticonEnabled = true;
		EmoticonCounter = 0.0f;
		EmoticonRend.color = new Color (1.0f, 1.0f, 1.0f, 0.0f);
		EmoticonFadeIn = true;
		if (emoticon == "Exclamation") {
			EmoticonRend.sprite = EmoticonSprites[0];
		} else if (emoticon == "Question") {
			EmoticonRend.sprite = EmoticonSprites[1];
		}
		else if (emoticon == "Happy") {
			EmoticonRend.sprite = EmoticonSprites[2];
		}
		else if (emoticon == "Angry") {
			EmoticonRend.sprite = EmoticonSprites[3];
		}
		else if (emoticon == "Sigh") {
			EmoticonRend.sprite = EmoticonSprites[4];
		}


		EmoticonRend.enabled = true;
	


	}

	//Types text
	IEnumerator TypeText ()
	{
		ThisTime = (Time.time);

		char[] CharArray = message.ToCharArray();
		int RealLength = 0;
		int AddedLength = 0;
		bool StoppedTyping = false;
		for (int i = 0; i < CharArray.Length; i++) {
			RealLength ++;

			string letter = CharArray[i].ToString();

			if (BreakLoop) {
				break;
			}
			//if (letter == "<"){
			
			//message+= letter 
			//	}
			/*
			if (CharArray[i].ToString() == "<"){
				int AmountSkipped = 25 + HighLightCount;
				for (int j = i; j < i+ AmountSkipped; j++){
					TTextObject.text += CharArray[j];
				}

				i += 25;
				i += HighLightCount;
			
			
			}


*/
			//Red
			if (letter == "<"){
				i++;
				letter = CharArray[i].ToString();
				letter = "<color=#ff0000ff>" + letter + "</color>";
			}
			//Blue
			else if (letter == ">"){
				i++;
				letter = CharArray[i].ToString();
				letter = "<color=#00ffffff>" + letter + "</color>";
			}
			//Yellow
			else if (letter == "{"){
				i++;
				letter = CharArray[i].ToString();
				letter = "<color=#ffff00ff>" + letter + "</color>";
			}

			//Advances text
			TTextObject.text += letter;
			finishedCounter ++;
			
			//Limit sound frequency and play sound
			if (Time.time > ThisTime + 0.1f) {
				ThisTime = Time.time;
				GetComponent<AudioSource> ().Play ();
			}
			
			
			
			//If certain punctuation, pause longer
			if (letter.ToString () == "." || letter.ToString () == "," || letter.ToString () == "!" || letter.ToString () == "?") {
				yield return new WaitForSeconds (longerPause);
			} else {
				yield return new WaitForSeconds (letterPause);
			}
			
			//	Debug.Log (message.ToCharArray().Length + " " + finishedCounter);
			//if (  finishedCounter >= message.ToCharArray ().Length && message.ToCharArray ().Length > 0) {
			if (  i >= CharArray.Length-1) {
			//	Debug.Log ("tester");
				SentencePause = RealLength * 0.025f;
				if (letterPause == 0.001f){
					SentencePause = SentencePause / 5.0f;
				}
				
				yield return new WaitForSeconds (SentencePause);
				finishedCounter = 0;
				FinishedText = true;
				BreakLoop = true;
				
			}
			

		}




		foreach (char letter in message.ToCharArray()) {
			

		}
	}
	public void PlayAnimation (string animm){
		MyAnimation.Play (animm, 0, 0.0f);
		GameManager.Scene = animm;
		GameManager.EventNumber = 0;
		GameManager.TextNumber = 0;
		
	}
	public void CutsceneToPlay(){


//		Transform TempTransform = MainCamera.transform;

		GameObject CutscenePlayer = GameObject.Find ("Animations/Scene/PlayerCutscene");
		Player.transform.position = CutscenePlayer.transform.position;
		
		Transform[] children = CutscenePlayer.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		PlayerController.canControl = true;
		GameManager.CutsceneEnabled = false;
		GameManager.RealCutscene = false;
		MainCamera.transform.SetParent(null);
		StartCoroutine(DelayAndStopAnimations ());
	


	//	MyAnimation.StopPlayback();
	//	MyAnimation.enabled = false;

//		MainCamera.transform.position = TempTransform.position;



	///	CameraSmooth.SmoothFollow = true;
	//	CameraSmooth.FollowTarget = Player.transform;

	}
	IEnumerator DelayAndStopAnimations(){
		yield return new WaitForSeconds(0.5f);

			MyAnimation.StopPlayback();
			MyAnimation.enabled = false;
			
		

	}

	public void PlayToCutscene(string anim){
		PlayerController.canControl = false;
		MyAnimation.enabled = true;
		GameManager.RealCutscene = true;
		GameManager.CutsceneEnabled = true;



		Transform[] children = Player.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){

			if (children [i].gameObject.name != "Emoticon") {


			
			if (children [i].gameObject.GetComponent<SpriteRenderer> () != null) {
				children [i].gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		}


		GameObject CutscenePlayer = GameObject.Find ("Animations/Scene/PlayerCutscene");
	
		
		Transform[] children2 = CutscenePlayer.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < children2.Length; ++i) {
			
			//if (transform.GetChild (i).childCount > 0){

			if (children2 [i].gameObject.name != "Player_Emoticon") {

				if (children2 [i].gameObject.GetComponent<SpriteRenderer> () != null) {
					children2 [i].gameObject.GetComponent<SpriteRenderer> ().enabled = true;
					//	 children[i].GameObject.GetComponent<SpriteRenderer>().enabled = false;
				}
			}
		}


		MainCamera.transform.SetParent (GameObject.Find ("Animations/Scene/").transform);
		PlayAnimation (anim);


	}



}

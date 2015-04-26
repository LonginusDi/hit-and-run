using UnityEngine;
using System.Collections;
public class UIManagerScript : MonoBehaviour {
//	private FB fb;


	void Start(){
//		SoomlaProfile.Initialize ();
//		FB.Init(onInitComplete:"", onHideUnity:null);
	}

	public void Menu(){
		Application.LoadLevel ("modeChoosingScene");
	}

	public void StartGame(){
		ArrowController.hearts = 3;
		ArrowController.time = 0;
		ArrowController.score = 0;
		BubbleCreator.numberOfBubbles = 0;
		Application.LoadLevel ("mainScene");
	}

}

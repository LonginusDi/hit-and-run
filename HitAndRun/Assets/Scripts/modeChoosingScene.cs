using UnityEngine;
using System.Collections;

public class modeChoosingScene : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}

	public void ChooseBase(){
		modeChooser = 1;
		Application.LoadLevel ("levelChoosingScene");
	}

	public void ChooseEndless(){
		modeChooser = 2;
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;



		Application.LoadLevel ("mainScene");
	}

	public void ChooseTimeRace(){
		modeChooser;
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;

		Application.LoadLevel ("mainScene");
	}

	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class modeChoosingScene : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}

	public void ChooseBase(){
		ArrowController.modeChooser = 1;
		Application.LoadLevel ("levelChoosingScene");
	}

	public void ChooseEndless(){
		ArrowController.modeChooser = 2;
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;

		Application.LoadLevel ("mainScene");
	}

	public void ChooseTimeRace(){
		ArrowController.modeChooser = 3;
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

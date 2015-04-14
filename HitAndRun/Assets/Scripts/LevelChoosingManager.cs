using UnityEngine;
using System.Collections;

public class LevelChoosingManager : MonoBehaviour {
	public void ChooseEasy(){
		RedDotController.timeCount = Mathf.Infinity;
		BlueDotController.timeCount = Mathf.Infinity;
		GreenDotController.timeCount = Mathf.Infinity;
		YellowDotController.timeCount = Mathf.Infinity;
		StartGame ();
	}

	public void ChooseMedium(){		
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;
		StartGame ();
	}

	public void ChooseHard(){
		RedDotController.timeCount = Mathf.Infinity;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;
		StartGame ();
	}

	public void StartGame(){
		Application.LoadLevel ("mainScene");
	}
}

using UnityEngine;
using System.Collections;

public class LevelChoosingManager : MonoBehaviour {
	public void ChooseEasy(){
		BubbleCreator.numberOfBubbles = 0;
		RedDotController.timeCount = 5.0f;
		BlueDotController.timeCount = Mathf.Infinity;
		GreenDotController.timeCount = Mathf.Infinity;
		YellowDotController.timeCount = Mathf.Infinity;
		StartGame ();
	}

	public void ChooseMedium(){		
		BubbleCreator.numberOfBubbles = 0;
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 30.0f;
		GreenDotController.timeCount = 30.0f;
		YellowDotController.timeCount = 30.0f;
		StartGame ();
	}

	public void ChooseHard(){
		BubbleCreator.numberOfBubbles = 0;
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

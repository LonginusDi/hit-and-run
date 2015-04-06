using UnityEngine;
using System.Collections;

public class LevelChoosingManager : MonoBehaviour {
	public void ChooseEasy(){
		RedDotController.timeCount = 5.0f;
		StartGame ();
	}

	public void ChooseMedium(){		
		RedDotController.timeCount = 30.0f;
		StartGame ();
	}

	public void ChooseHard(){
		RedDotController.timeCount = Mathf.Infinity;
		StartGame ();
	}

	public void StartGame(){
		Application.LoadLevel ("mainScene");
	}
}

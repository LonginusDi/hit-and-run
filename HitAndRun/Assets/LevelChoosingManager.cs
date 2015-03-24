using UnityEngine;
using System.Collections;

public class LevelChoosingManager : MonoBehaviour {
	public void ChooseEasy(){
		DotController.timeCount = 5.0f;
		StartGame ();
	}

	public void ChooseMedium(){		
		DotController.timeCount = 30.0f;
		StartGame ();
	}

	public void ChooseHard(){
		DotController.timeCount = Mathf.Infinity;
		StartGame ();
	}

	public void StartGame(){
		Application.LoadLevel ("mainScene");
	}
}

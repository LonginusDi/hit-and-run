using UnityEngine;
using System.Collections;

public class modeChoosingScene : MonoBehaviour {

	public AudioClip clickSound;

	// Use this for initialization
	void Start () {
	
	}

	public void ChooseBase(){
		audio.PlayOneShot(clickSound);

		ArrowController.modeChooser = 1;
		BubbleCreator.numberOfBubbles = 0;
		Application.LoadLevel ("levelChoosingScene");
	}

	public void ChooseEndless(){
		audio.PlayOneShot(clickSound);

		ArrowController.modeChooser = 2;
		BubbleCreator.numberOfBubbles = 0;
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;
		IceDotController.timeCount = 10.0f;
		ArrowController.hearts = 3;

		Application.LoadLevel ("mainScene");
	}

	public void ChooseTimeRace(){
		audio.PlayOneShot(clickSound);

		ArrowController.modeChooser = 3;
		ArrowController.startTime = Time.time;
		BubbleCreator.numberOfBubbles = 0;
		RedDotController.timeCount = 15.0f;
		BlueDotController.timeCount = 10.0f;
		GreenDotController.timeCount = 10.0f;
		YellowDotController.timeCount = 10.0f;
		IceDotController.timeCount = 10.0f;

		Application.LoadLevel ("mainScene");
	}

	// Update is called once per frame
	void Update () {
	
	}
}

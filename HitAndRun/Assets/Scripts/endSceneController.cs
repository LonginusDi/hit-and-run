using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class endSceneController : MonoBehaviour {

	private Text statementGT;	
	private Transform statementGO;
	private GameObject canvasGO;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		canvasGO = GameObject.Find ("Canvas");
		statementGO = canvasGO.transform.Find ("endStatement");
		statementGT = statementGO.GetComponent<Text> ();
		statementGT.text = "Congratulations! \nYou scored " + ArrowController.score + " points!";

	}
}

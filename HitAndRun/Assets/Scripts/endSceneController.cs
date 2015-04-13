using UnityEngine;
using System.Collections;

public class endSceneController : MonoBehaviour {

	private GUIText statementGT;	
	private GameObject statementGO;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		statementGO = GameObject.Find ("endStatement");
		statementGT = statementGO.GetComponent<GUIText> ();
		statementGT.text = "You scored " + ArrowController.score + " points!";
	}
}

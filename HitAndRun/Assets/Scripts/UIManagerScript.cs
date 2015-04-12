using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public void Menu(){
		Application.LoadLevel ("modeChoosingScene");
	}

	public void StartGame(){
		Application.LoadLevel ("mainScene");
	}
}

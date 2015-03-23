using UnityEngine;
using System.Collections;

public class BubbleUpdater : MonoBehaviour {
	private BubbleController bubbleController;
	// Use this for initialization
	void Start () {
		bubbleController = transform.parent.GetComponent<BubbleController> ();
	}
	
	// Update is called once per frame
	void BubbleDisappear()
	{
		bubbleController.BubbleDisappear();
	}
}

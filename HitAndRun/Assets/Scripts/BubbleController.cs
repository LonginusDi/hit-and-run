using UnityEngine;
using System.Collections;
//using System;

public class BubbleController : MonoBehaviour {
	private float moveSpeed = 0.1f; 
	private float turnSpeed = 0.3f;
	private float xMax;
	private float yMax;
	private Vector3 moveDirection;
	private Camera myCamera;
	private Vector3 moveToward;
	private Vector3 currentPosition;
	private Random random;

	public GameObject redDotPrefab;
	public GameObject greenDotPrefab;
	public GameObject yellowDotPrefab;
	public GameObject blueDotPrefab;
	// Use this for initialization
	void Start () {
		myCamera = Camera.main;
		moveToward = new Vector3(Random.Range(-xMax, xMax),
		                         Random.Range(-yMax, yMax), 
		                         transform.position.z );
		xMax = myCamera.aspect * myCamera.orthographicSize;
		yMax = myCamera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		currentPosition = transform.position;
		moveDirection = moveToward - currentPosition;
		if (Mathf.Abs(moveDirection.x) <= 0.3 || Mathf.Abs(moveDirection.y) <= 0.3) {
			moveToward = new Vector3(Random.Range(-xMax, xMax),
			                         Random.Range(-yMax, yMax), 
			                         transform.position.z );
			moveDirection = moveToward - currentPosition;
		}
		moveDirection.z = 0;
//		moveDirection.Normalize();

		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);
		
		float targetAngle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp (transform.rotation, 
			                  Quaternion.Euler (0, 0, targetAngle), 
			                  turnSpeed * Time.deltaTime);
	}

	public void TouchedByArrow(){
		Transform bubble = transform.FindChild("bubble");
		bubble.collider2D.enabled = false;
		bubble.GetComponent<Animator>().SetBool( "IsTouched", true );
	}

	public void BubbleDisappear(){
		CreateRedDot ();
		DestroyObject (gameObject);
	}

	void CreateRedDot(){
		Vector3 dotPos = transform.position;
		float targetAngle1 = Mathf.Atan2(0, 1) * Mathf.Rad2Deg;
		float targetAngle2 = Mathf.Atan2(0, -1) * Mathf.Rad2Deg;
		float targetAngle3 = Mathf.Atan2(1, 0) * Mathf.Rad2Deg;
		float targetAngle4 = Mathf.Atan2(-1, 0) * Mathf.Rad2Deg;
		float random = Random.Range (targetAngle1, targetAngle3);
		Instantiate(redDotPrefab, dotPos, Quaternion.Euler(0, 0, targetAngle1 + random));
		Instantiate(greenDotPrefab, dotPos, Quaternion.Euler(0, 0, targetAngle2 + random));
		Instantiate(blueDotPrefab, dotPos, Quaternion.Euler(0, 0, targetAngle3 + random));
		Instantiate(yellowDotPrefab, dotPos, Quaternion.Euler(0, 0, targetAngle4 + random));
	}


}

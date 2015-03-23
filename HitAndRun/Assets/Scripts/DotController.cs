using UnityEngine;
using System.Collections;

public class DotController : MonoBehaviour {
	public float speed = 0.1f;
	private float xMax;
	private float yMax;
	private Vector3 moveDirection;
	private Camera myCamera;
	private Vector3 moveToward;
	private Vector3 currentPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.right * speed * Time.deltaTime;
//		EnforceBounds();
//		currentPosition = transform.position;
//		moveDirection = moveToward - currentPosition;
//		if (Mathf.Abs(moveDirection.x) <= 0.3 || Mathf.Abs(moveDirection.y) <= 0.3) {
//			moveToward = new Vector3(Random.Range(-xMax, xMax),
//			                         Random.Range(-yMax, yMax), 
//			                         transform.position.z );
//			moveDirection = moveToward - currentPosition;
//		}
//		moveDirection.z = 0;
		//		moveDirection.Normalize();
		
//		Vector3 target = moveDirection * speed + currentPosition;
//		transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);
		
//		float targetAngle = Mathf.Atan2 (moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
//		transform.rotation = 
//			Quaternion.Slerp (transform.rotation, 
//			                  Quaternion.Euler (0, 0, targetAngle), 
//			                  turnSpeed * Time.deltaTime);
	}

	public void OnBecameInvisible() {
		Destroy( gameObject ); 
	}

//	private void EnforceBounds()
//	{
//		Vector3 newPosition = transform.position; 
//		Camera mainCamera = Camera.main;
//		Vector3 cameraPosition = mainCamera.transform.position;
//		
//		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
//		float xMax = cameraPosition.x + xDist;
//		float xMin = cameraPosition.x - xDist;
//		float yMax = mainCamera.orthographicSize;
//		
//		if ( newPosition.x < xMin || newPosition.x > xMax ) {
//			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
//			moveDirection.x = -moveDirection.x;
//		}
//		
//		if (newPosition.y < -yMax || newPosition.y > yMax) {
//			newPosition.y = Mathf.Clamp( newPosition.y, -yMax, yMax );
//			moveDirection.y = -moveDirection.y;
//		}
//		transform.position = newPosition;
//	}
}

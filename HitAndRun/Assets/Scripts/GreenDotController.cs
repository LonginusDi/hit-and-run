using UnityEngine;
using System.Collections;

public class GreenDotController : MonoBehaviour {
	public float speed = 0.05f;
	private float xMax;
	private float yMax;
	private Vector3 moveDirection;
	private Camera myCamera;
	private Vector3 moveToward;
	private Vector3 currentPosition;
	private Vector3 forward;
	
	public static float timeCount;
	
	private float startTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		forward = transform.right + transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime < timeCount) {
			transform.position += forward * speed * Time.deltaTime;
			EnforceBounds ();
		} else {
			Destroy (gameObject);
		}
	}
	
	public void OnBecameInvisible() {
		Destroy( gameObject ); 
	}
	
	private void EnforceBounds()
	{
		Vector3 newPosition = transform.position; 
		Camera mainCamera = Camera.main;
		Vector3 cameraPosition = mainCamera.transform.position;
		
		float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
		float xMax = cameraPosition.x + xDist;
		float xMin = cameraPosition.x - xDist;
		float yMax = mainCamera.orthographicSize;
		
		if ( newPosition.x < xMin || newPosition.x > xMax ) {
			newPosition.x = Mathf.Clamp( newPosition.x, xMin, xMax );
			forward.x = -forward.x;
		}
		
		if (newPosition.y < -yMax || newPosition.y > yMax) {
			newPosition.y = Mathf.Clamp( newPosition.y, -yMax, yMax );
			forward.y = -forward.y;
		}
		transform.position = newPosition;
	}
	void OnTriggerEnter2D( Collider2D other )
	{
		if (other.CompareTag ("arrow")) {
			Destroy (gameObject);
		}
	}
}

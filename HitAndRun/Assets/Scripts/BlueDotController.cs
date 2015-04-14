using UnityEngine;
using System.Collections;

public class BlueDotController : MonoBehaviour {
	public float speed = 0.05f;
	private float xMax;
	private float yMax;
	private Vector3 moveDirection;
	private Camera myCamera;
	private Vector3 moveToward;
	private Vector3 currentPosition;
	private Vector3 forward;

	private bool EliminateRedDots = false;
	
	public static float timeCount = Mathf.Infinity;
	
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
//			Destroy (gameObject);
//			renderer.enabled = false;
			GetComponent<Animator>().SetBool( "IsTouched", true );
			EliminateRedDots = true;
//			Vector3 scale = transform.localScale;
//			scale = scale * 10;
			transform.localScale = scale;
			speed = 0.0f;
//			StartCoroutine(EliminateRedDots(1.0f));
//			if (other.CompareTag("reddot")){
//				Destroy(other);
//			}
		}
		else if (EliminateRedDots == true && other.CompareTag ("reddot")) {
			Destroy (other.gameObject);

		}

	}

//	IEnumerator EliminateRedDots(float waitTime){
//		float endTime = Time.time + waitTime;
//		while (Time.time < endTime) {
//			renderer.enabled = false;
//			yield return new WaitForSeconds(0.1f);
//			renderer.enabled = true;
//			yield return new WaitForSeconds(0.1f);
//		}
////		Destroy (gameObject);
//	}

	void MissionComplete(){
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
	public float moveSpeed = 2.0f;
	private Vector3 moveDirection;
	public float turnSpeed ;
	private float smooth = 150.0f;

	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int currentColliderIndex = 0;

	private static float AccelerometerUpdateInterval = 1 / 60;
	private static float LowPassKernelWidthInSeconds = 1;
	private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; 
	private Vector3 lowPassValue = Vector3.zero;

	public static int score = 0;
	private GUIText scoreGT;

	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
		lowPassValue = Input.acceleration;

		score = 0;
		GameObject scoreGO = GameObject.Find ("score");
		scoreGT = scoreGO.GetComponent<GUIText> ();
		scoreGT.text = "0";
	}

	public Vector3 LowPassFilterAccelerometer() {
		lowPassValue = new Vector3(Mathf.Lerp(lowPassValue.x, Input.acceleration.y, LowPassFilterFactor), Mathf.Lerp(lowPassValue.y, -Input.acceleration.x, LowPassFilterFactor), 0);
		return lowPassValue;
	}


	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		
		Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		moveDirection = moveToward - currentPosition;
		moveDirection.z = 0;
		moveDirection.Normalize();

//			moveDirection = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
//			moveDirection.z = 0;
//			moveDirection.Normalize();
//		moveSpeed = Mathf.Sqrt (Mathf.Pow (Input.acceleration.x, 2) + Mathf.Pow (Input.acceleration.y, 2)) * smooth;
//		moveSpeed *= Time.deltaTime;
		Vector3 target = moveDirection * moveSpeed + currentPosition;
		transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);

		//Rotate Angle
		float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = 
			Quaternion.Slerp (transform.rotation, 
				                  Quaternion.Euler(0, 0, targetAngle),
			                 turnSpeed * Time.deltaTime);
		EnforceBounds();
	}

	public void SetColliderForSprite( int spriteNum )
	{
		colliders[currentColliderIndex].enabled = false;
		currentColliderIndex = spriteNum;
		colliders[currentColliderIndex].enabled = true;
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if (other.CompareTag ("bubble")) {
//			Destroy(other.gameObject);
			other.transform.parent.GetComponent<BubbleController>().TouchedByArrow();
			score += 5;
			scoreGT.text = "" + score;
			if (score >= 200) {
				Application.LoadLevel("winScene");
			}
		}
		else if (other.CompareTag("reddot")){
			Application.LoadLevel("failScene");
		}
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
			moveDirection.x = -moveDirection.x;
		}

		if (newPosition.y < -yMax || newPosition.y > yMax) {
			newPosition.y = Mathf.Clamp( newPosition.y, -yMax, yMax );
			moveDirection.y = -moveDirection.y;
		}
		transform.position = newPosition;
	}
}

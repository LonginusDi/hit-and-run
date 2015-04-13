using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
	public float moveSpeed = 2.0f;
	private Vector3 moveDirection;
	public float turnSpeed ;
	private float smooth = 150.0f;

	public static bool isInvincible = false;

	private float speedUpTime = 0.0f;
	[SerializeField]
	private PolygonCollider2D[] colliders;
	private int currentColliderIndex = 0;

	private static float AccelerometerUpdateInterval = 1 / 60;
	private static float LowPassKernelWidthInSeconds = 1;
	private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds; 
	private Vector3 lowPassValue = Vector3.zero;

	public static int hp = 0;
	private GUIText hpGT;
	public static int score = 0;
	private GUIText scoreGT;
	public static float time = 0;
	private GUIText timeGT;	

	public static double hearts = 3;
	private GUIText heartsGT;	
	private GameObject heartsGO;
	private static int countBubbles = 0;

	private GameObject hpGO;
	private GameObject scoreGO;
	private GameObject timeGO;

	private float lastUpdate;
	
	public static int modeChooser;

	// Use this for initialization
	void Start () {
		moveDirection = Vector3.right;
		lowPassValue = Input.acceleration;

		if (modeChooser == 1) {
			hpGO = GameObject.Find ("hp");
			hpGT = hpGO.GetComponent<GUIText> ();

			hp = 100;
			
			hpGT.text = "HP: 100";
		}

		if (modeChooser == 2) {
			heartsGO = GameObject.Find ("hearts");
			heartsGT = heartsGO.GetComponent<GUIText> ();
			heartsGT.text = "Hearts: " + hearts;

			scoreGO = GameObject.Find ("score");
			scoreGT = scoreGO.GetComponent<GUIText> ();

			score = 0;
			
			scoreGT.text = "Score: 0";
		}

		if (modeChooser == 3) {
			hpGO = GameObject.Find ("hp");
			hpGT = hpGO.GetComponent<GUIText> ();
			
			hp = 100;
			
			hpGT.text = "HP: 100";

			timeGO = GameObject.Find ("time");
			timeGT = timeGO.GetComponent<GUIText> ();

			time = 0;
			
			timeGT.text = "Time: 0.0";
		}
		
	}

	public Vector3 LowPassFilterAccelerometer() {
		lowPassValue = new Vector3(Mathf.Lerp(lowPassValue.x, Input.acceleration.y, LowPassFilterFactor), Mathf.Lerp(lowPassValue.y, -Input.acceleration.x, LowPassFilterFactor), 0);
		return lowPassValue;
	}


	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		
		if (speedUpTime != 0 && Time.time - speedUpTime > 5) {
			speedUpTime = 0.0f;
			moveSpeed = moveSpeed / 2;
		}

//		mouse
		Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		moveDirection = moveToward - currentPosition;
		moveDirection.z = 0;
		moveDirection.Normalize();


		//Accelerometer
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


		if (modeChooser == 1) {
			//hp will reduce according to time
			if(Time.time - lastUpdate >= 5f){
				hp = hp - 5;
				hpGT.text = "HP: " + hp;
				lastUpdate = Time.time;
			}

			if (hp >= 200) {
				Application.LoadLevel ("winScene");
			}
			if (hp <= 0) {
				Application.LoadLevel ("failScene");
			}
		}

		else if (modeChooser == 2) {
			if (hearts <= 0.0f) {
				Application.LoadLevel ("endSceneForEndless");
			}
		}

		else if (modeChooser == 3) {
			time = Time.time;
			var calTime = Mathf.Round(time * 100) / 100;
			timeGT.text = "Time: " + calTime;

			if(Time.time - lastUpdate >= 5f){
				hp = hp - 5;
				hpGT.text = "HP: " + hp;
				lastUpdate = Time.time;
			}
			
			if (hp <= 0) {
				Application.LoadLevel ("failScene");
			}
		}
		
		EnforceBounds();
	}

//	public void fixedUpdate(){
//		if (hearts <= 0.0f) {
//			Application.LoadLevel ("endSceneForEndless");
//		}
//	}

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
			if(modeChooser == 1){
				hp += 5;
				hpGT.text = "HP: " + hp;
			}
			if(modeChooser == 2){
				score += 5;
				scoreGT.text = "Score: " + score;

				countBubbles += 1;

				if(countBubbles >= 15){
					hearts += 1;
					heartsGT.text = "Hearts: " + hearts;
					countBubbles = 0;
				}
			}
			if(modeChooser == 3){
				if(hp <= 190){
					hp += 5;
					hpGT.text = "HP: " + hp;
				}
			}

//			if (hp >= 200) {
//				Application.LoadLevel("winScene");
//			}
		}
		else if (other.CompareTag("reddot") && isInvincible == false){
			StartCoroutine(Blink(1f));
			if(modeChooser == 1 || modeChooser == 3){
				hp -= 30;
				hpGT.text = "HP: " + hp;
			}
			else{
				hearts -= 1;
				heartsGT.text = "Hearts: " + hearts;
//				if (hearts <= 0) {
//					Application.LoadLevel ("endSceneForEndless");
//				}
			}
//			hpGT.text = "HP: " + hp;
//			if (hp <= 0){
//				Application.LoadLevel("failScene");
//			}
		}
		else if (other.CompareTag("yellowdot")){
			moveSpeed = moveSpeed * 2;
			speedUpTime = Time.time;
			Destroy (other);
		}
		else if (other.CompareTag("greendot")){

			if(modeChooser == 1){
				hp += 15;
				hpGT.text = "HP: " + hp;
			}
			if(modeChooser == 2){
				hearts += 0.5;
				heartsGT.text = "Hearts: " + hearts;
			}
			if(modeChooser == 3){
				hp += 15;
				hpGT.text = "HP: " + hp;
			}
//			if (hp >= 200){
//				Application.LoadLevel("winScene");
//			}
			Destroy (other);
		}
		else if (other.CompareTag("bluedot")){
			//hp -= 15;
			if(modeChooser == 1){
				hpGT.text = "HP: " + hp;
			}
			if(modeChooser == 2){
				
			}
			if(modeChooser == 3){
				hpGT.text = "HP: " + hp;
			}
			Destroy (other);
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

	IEnumerator Blink(float waitTime){
		float endTime = Time.time + waitTime;
		isInvincible = true;
		while (Time.time < endTime) {
			renderer.enabled = false;
			yield return new WaitForSeconds(0.1f);
			renderer.enabled = true;
			yield return new WaitForSeconds(0.1f);

		}
		isInvincible = false;
	}
}

using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {
	public float moveSpeed = 2.0f;
	private Vector3 moveDirection;
	public float turnSpeed ;
	private float smooth = 650.0f;

	public bool useMouse;

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
	public static float startTime;

	public static double hearts = 3;
	private GUIText heartsGT;	
	private GameObject heartsGO;
	private static int countBubbles = 0;

	private GameObject hpGO;
	private GameObject scoreGO;
	private GameObject timeGO;

	private float lastUpdate;
	
	public static int modeChooser;

	public GameObject effectPrefab;

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
			startTime = Time.time;
			time = 0;
			
			timeGT.text = "Time: 0.0";
		}
		
	}

	public Vector3 LowPassFilterAccelerometer() {
		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);;
		return lowPassValue;
	}


	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = transform.position;
		


//		mouse
		if (useMouse == true) {
			Vector3 moveToward = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			moveDirection = moveToward - currentPosition;
			moveDirection.z = 0;
			moveDirection.Normalize();
			if (speedUpTime != 0 && Time.time - speedUpTime > 5) {
				speedUpTime = 0.0f;
				moveSpeed = moveSpeed / 2;
			}
		} else{
		//Accelerometer
			moveDirection = new Vector3(Input.acceleration.x, Input.acceleration.y, 0);
			moveDirection.z = 0;
			moveDirection.Normalize();
			Vector3 filteredSpeed = LowPassFilterAccelerometer ();
			moveSpeed = Mathf.Sqrt (Mathf.Pow (filteredSpeed.x, 2) + Mathf.Pow (filteredSpeed.y, 2)) * smooth;
			moveSpeed *= Time.deltaTime;
			if (speedUpTime != 0 && Time.time - speedUpTime <= 5) {
				moveSpeed = moveSpeed * 2;
			}
			else if (speedUpTime != 0 && Time.time - speedUpTime > 5) {
				speedUpTime = 0.0f;
			}
		}


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
				hp = hp - 10;
				hpGT.text = "HP: " + hp;
				setEffectText("-10", Color.red, gameObject);
				lastUpdate = Time.time;
			}

			if (hp >= 200) {
				isInvincible = false;
				Application.LoadLevel ("winScene");
			}
			if (hp <= 0) {
				isInvincible = false;
				Application.LoadLevel ("failScene");
			}
		}

		else if (modeChooser == 2) {
			if (hearts <= 0.0f) {
				isInvincible = false;
				Application.LoadLevel ("endSceneForEndless");
			}
		}

		else if (modeChooser == 3) {
			time = Time.time - startTime;
			string minutes = Mathf.Floor(time / 60).ToString("00");
			string seconds = (time % 60).ToString("00");
			string miliseconds = Mathf.Floor((time*100) % 100).ToString("00");
			
//			if(minutes < 10) { minutes = "0" + minutes.ToString(); } 
//			if(seconds < 10) { seconds = "0" + Mathf.RoundToInt(seconds).ToString(); } 
//			GUI.Label(new Rect(10,10,250,100), minutes + ":" + seconds);
//			var calTime = Mathf.Round(time * 100) / 100;
			if(Mathf.Floor(time / 60) > 0 ){
				timeGT.text = "Time: " + minutes + ":" + seconds + "." + miliseconds;
			}
			else{
				timeGT.text = "Time: " + seconds + "." + miliseconds;
			}

			if(Time.time - lastUpdate >= 5f){
				hp = hp - 10;
				hpGT.text = "HP: " + hp;
				setEffectText("-10", Color.red, gameObject);
				lastUpdate = Time.time;
			}
			
			if (hp <= 0) {
				isInvincible = false;
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
			Vector3 dotPos = Camera.main.WorldToViewportPoint(other.transform.position);
			if(modeChooser == 1){
				hp += 10;
				hpGT.text = "HP: " + hp;
//				effectPrefab.guiText.text = "+10";
//				effectPrefab.guiText.color = Color.green;
//				Instantiate(effectPrefab, dotPos, Quaternion.identity);
				setEffectText("+10", Color.green, other.gameObject);
			}
			if(modeChooser == 2){
				score += 10;
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
					hp += 10;
					hpGT.text = "HP: " + hp;
					setEffectText("+10", Color.green, other.gameObject);
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
				setEffectText("-30", Color.red, other.gameObject);
			}
			else if(modeChooser == 2){
				hearts -= 1;
				heartsGT.text = "Hearts: " + hearts;
				setEffectText("Heart -1", Color.red, other.gameObject);
			}
//			hpGT.text = "HP: " + hp;
//			if (hp <= 0){
//				Application.LoadLevel("failScene");
//			}
		}
		else if (other.CompareTag("yellowdot")){
			moveSpeed = moveSpeed * 2;
			setEffectText("Speedup!", Color.yellow, other.gameObject);
			speedUpTime = Time.time;
			Destroy (other);
		}
		else if (other.CompareTag("greendot")){

			if(modeChooser == 1){
				hp += 30;
				hpGT.text = "HP: " + hp;
				setEffectText("+30", Color.green, other.gameObject);
			}
			if(modeChooser == 2){
				hearts += 1;
				heartsGT.text = "Hearts: " + hearts;
				setEffectText("Heart+1", Color.green, other.gameObject);
			}
			if(modeChooser == 3){
				hp += 30;
				hpGT.text = "HP: " + hp;
				setEffectText("+30", Color.green, other.gameObject);
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
//			Destroy (other);
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

	private void setEffectText(string text, Color color, GameObject go){
		
		effectPrefab.guiText.text = text;
		effectPrefab.guiText.color = color;
		Vector3 dotPos = Camera.main.WorldToViewportPoint (go.transform.position);
		Instantiate (effectPrefab, dotPos, Quaternion.identity);
	}
		
	}
	
using UnityEngine;

public class BubbleCreator: MonoBehaviour {
	public float minSpawnTime = 2f; 
	public float maxSpawnTime = 5f; 
	
	public GameObject bubblePrefab;
	
	void Start () 
	{
		Invoke("SpawnBubble",minSpawnTime);
	}
	
	void SpawnBubble() {
		Camera camera = Camera.main;
//		Vector3 cameraPos = camera.transform.position;
		float xMax = camera.aspect * camera.orthographicSize;
		float xRange = camera.aspect * camera.orthographicSize * 1.75f;
		float yMax = camera.orthographicSize - 0.5f;
		
		Vector3 bubblePos = 
			new Vector3(Random.Range(xMax - xRange, xMax),
			            Random.Range(-yMax, yMax),
			            bubblePrefab.transform.position.z);
		
		Instantiate(bubblePrefab, bubblePos, Quaternion.identity);
		
		Invoke("SpawnBubble", Random.Range(minSpawnTime, maxSpawnTime));
	}
}
using UnityEngine;
using System.Collections;

public class EffectGTController : MonoBehaviour{
//	public float fadeDuration = 1.0f;
//	
//	private float timeLeft = 0.0f;
	
	private float origRed = 0.0f;
	private float origGreen = 0.0f;
	private float origBlue = 0.0f;
//	
	private void Start()
	{
//		timeLeft = fadeDuration;
//		
		origBlue = gameObject.guiText.font.material.color.b;
		origGreen = gameObject.guiText.font.material.color.g;
		origRed = gameObject.guiText.font.material.color.r;
		
		//Set Text to transparent
		gameObject.guiText.font.material.color = new Color(origRed, origGreen, origBlue, 1);
	}
//	
//	private void Update()
//	{
//		if (timeLeft > 0)
//		{
//			//Fade in
//			DebugConsole.Log("Fading in " + timeLeft);
//			Fade(true);
//		}
//		else if (timeLeft > (-fadeDuration))
//		{
//			//Fade out
//			DebugConsole.Log("Fading out ");
//			Fade(false);
//		}
//		else
//		{
//			DebugConsole.Log("Boom!");
//			Destroy(gameObject);
//		}
//		timeLeft -= Time.deltaTime;
//		StartCoroutine(Fade ());
//	}
	
//	IEnumerator Fade()
//	{
//		if (fadeIn)
//		{
//			float a = guiText.font.material.color.a;
//			a = (timeLeft / fadeDuration);
//			if (a > 1) { a = 1; }
//			guiText.font.material.color = new Color(origRed, origGreen, origBlue, 1-a);
//		}
//		else
//		{
//			float a = guiText.font.material.color.a;
//			a = timeLeft / (-fadeDuration);
//			if (a < 0) { a = 0; }
//			guiText.font.material.color = new Color(origRed, origGreen, origBlue, 1-a);
//		}
		
//		float a = gameObject.guiText.font.material.color.a;
//		a = 1;
//		gameObject.guiText.font.material.color = new Color(origRed, origGreen, origBlue, a);
//		while (gameObject.guiText.material.color.a > 0) {
//			gameObject.guiText.font.material.color = new Color(origRed, origGreen, origBlue, a);
//			a -= 0.1f;
//			yield return new WaitForSeconds(1f);
//		}
//	}
	public void TextDisappear(){
		Destroy(gameObject);
	}
}
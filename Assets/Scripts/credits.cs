using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class credits : MonoBehaviour {

	public Vector2 targetPosition;
	public GameObject target;
	public float speed = 1;
	public float delay = 1;
	public bool creditsRoll = false;

	private Vector2 newPosition;
	private Vector2 originalPosition;
	private float startTime;


	public void Start(){
		originalPosition = target.GetComponent<RectTransform> () .anchoredPosition;
		newPosition = targetPosition + target.GetComponent<RectTransform> () .anchoredPosition;
		startTime = Time.time;
	}

	void Update ()
	{
		if (creditsRoll) 
		{
			rollCredits();
		}
	}
	public void reset(){
		target.GetComponent<RectTransform> () .anchoredPosition = originalPosition;
	}
	public void rollCredits ()
	{
		if (target.GetComponent<RectTransform> () .anchoredPosition.y < newPosition.y && Time.time - startTime > delay) {
			target.GetComponent<RectTransform> () .anchoredPosition += new Vector2 (0, speed);
		}
	}

	public void toggleCredits(bool toggle)
	{
		creditsRoll = toggle;
	}
}

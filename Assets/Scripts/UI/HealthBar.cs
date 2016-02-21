﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Image healthBar;				//Drag in an image
	public int delay = 4;				// Time until the ui will start to fade
	public int fadeSpeed = 2;			// How quickly the ui elements will fade
	public bool visible = false;		// If the UI is visible
	
	private CanvasGroup visibility;		// Referance to the canvasGroup component
	private Color startingColour;
	public playerCharacter player;	//Referance to the player

	private float newHealth;
	private float changeInHealth;
	private float previousHealth;

	void Start()
	{
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		startingColour = healthBar.color;
		visibility = GetComponent<CanvasGroup>();
		visible = true;
	}

	void Update () 
	{
		newHealth = player.health;
		changeInHealth = newHealth - previousHealth;
		previousHealth = player.health;

		if(changeInHealth != 0)
		{
			visible = true;
		}

		if(visible == true)
		{
			StartCoroutine( showHealthBar());
			
		}

		float percentage = (float)player.health / 100;

		healthBar.fillAmount = percentage;

		startingColour.r = 1 - percentage;
		startingColour.g = percentage;
		startingColour.b = 0;
		healthBar.color = startingColour;
		// Lerp the light's intensity towards the current target.
	//	healthBar.color = new Color ( myImage.color.r, Mathf.Lerp(myImage.color.g, targetIntensity, fadeSpeed * Time.deltaTime), Mathf.Lerp(myImage.color.b, targetIntensity, fadeSpeed * Time.deltaTime));

	}


	private IEnumerator showHealthBar()
	{
		visibility.alpha = 1;
		yield return new WaitForSeconds(delay);
		visibility.alpha =  Mathf.Lerp(visibility.alpha, 0, fadeSpeed * Time.deltaTime);
		visible = false;
		
	}

}

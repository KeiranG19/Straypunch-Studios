using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Image healthBar;				//Drag in an image
	public int delay = 4;				//Time until the ui will start to fade
	public int fadeSpeed = 2;			//How quickly the ui elements will fade
	public bool visible = false;		//If the UI is visible
	private bool doneOnce = false;

	private CanvasGroup visibility;		// Referance to the canvasGroup component
	private Color startingColour;
	public playerCharacter player;		//Referance to the player
	public int playerNumber = 0;
	private gameController manager;
	void Start()
	{
		manager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController>();
		GameObject tempPlayer;

		if (manager.settings == null) {
			if (playerNumber == 1) {
				tempPlayer = GameObject.Find ("Player 1(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 1 doesn't exist");
				}
			} else if (playerNumber == 2) {
				tempPlayer = GameObject.Find ("Player 2(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 2 doesn't exist");
				}
			} else if (playerNumber == 3) {
				tempPlayer = GameObject.Find ("Player 3(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 3 doesn't exist");
				}
			} else {
				tempPlayer = GameObject.Find ("Player 4(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 4 doesn't exist");
				}
			}
			healthBar = GetComponent<Image> ();
			startingColour = player.transform.FindChild ("characterModel").FindChild ("goo_man").renderer.material.color;
		} 
		else 
		{
			if (playerNumber == 1) {
				 tempPlayer = GameObject.Find ("Player 1(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 1 doesn't exist");
				}
			} else if (playerNumber == 2) {
				 tempPlayer = GameObject.Find ("Player 2(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 2 doesn't exist");
				}
			} else if (playerNumber == 3) {
				 tempPlayer = GameObject.Find ("Player 3(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 3 doesn't exist");
				}
			} else {
				 tempPlayer = GameObject.Find ("Player 4(Clone)");
				if (tempPlayer != null) {
					player = tempPlayer.GetComponent<playerCharacter> ();
					
				} else {
					Debug.Log ("player 4 doesn't exist");
				}
			}

		}
		healthBar = GetComponent<Image> ();
		if(player != null)
		{
			startingColour = player.transform.FindChild ("characterModel").FindChild ("goo_man").renderer.material.color;
		}
		else
		{
			healthBar.color = new Color(0,0,0,0);
			gameObject.SetActive(false);
		}
	}

	void Update () 
	{
		if (player != null) {
			if (!doneOnce) {

				doneOnce = true;
			}
			float percentage = (float)player.health / 100;

			healthBar.fillAmount = percentage;
		
			healthBar.color = startingColour * 0.95f;
		}
	}
}

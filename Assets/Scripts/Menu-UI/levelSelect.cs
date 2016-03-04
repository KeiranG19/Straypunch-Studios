using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class levelSelect : MonoBehaviour 
{
	public Text levelName;				// Text box for showing the level name
	public Image levelImage;			// Image for displaying level preview
	public Text lives;					// Referance to the lives selected in match options
	public Text timer;					// Referance to the time limit selected in match options
	public Text hazards;				// Referance to the hazards selected in match options
	public bool levelSelected;			// If true then enable the button which takes the user to the next menu
	public GameObject nextMenuButton;	// Button which takes the user to the next menu

	private MatchOptions settings;		// Referance to level options which get passed to next scene

	void Start () 
	{
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<MatchOptions>();
	}

	void Update () 
	{
		//displayLevel();
		//showLevelName();

		if(settings.level !=0)
		{
			levelSelected = true;
		}

		if(levelSelected)
		{
			nextMenuButton.SetActive(true);
			levelImage.gameObject.SetActive(true);
			levelName.gameObject.SetActive(true);
		}
		else
		{
			nextMenuButton.SetActive(false);
			levelImage.gameObject.SetActive(false);
			levelName.gameObject.SetActive(false);
		}
	}
	
	public void displayLevel(Sprite map)
	{
		levelImage.sprite = map;
	}

	public void setLevelName(string name )
	{
		levelName.text = name;
	}

}

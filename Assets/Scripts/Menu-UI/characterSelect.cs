﻿using UnityEngine;
using System.Collections;

public class characterSelect : MonoBehaviour {
	public MenuManager manager;
	public Menu myMenu;
	public bool player3Enabled = false;
	public bool player4Enabled = false;
	public GameObject player3JoinButton;
	public GameObject player4JoinButton;

	private MatchOptions settings;			// Referance to level options which get passed to next scene
	
	void Start () 
	{
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<MatchOptions>();
	}

	void Update () 
	{
		if (manager.currentMenu == myMenu) 
		{
			if (Input.GetButtonDown ("P2Jump")) 
			{
				player3SetActive (true);
			}
			if (Input.GetButtonDown ("P2Sprint")) 
			{
				player3SetActive (false);
			}
			if (Input.GetButtonDown ("P3Jump"))
			{
				player4SetActive (true);
			}
			if (Input.GetButtonDown ("P3Sprint")) 
			{
				player4SetActive (false);
			}
		}

	}

	public void player3SetActive(bool active)
	{
		if(active && player3Enabled == false)
		{
			player3Enabled = true;
			settings.playerCount++;
			player3JoinButton.SetActive(false);
		}
		else if(active == false && player3Enabled == true)
		{
			player3Enabled = false;
			settings.playerCount--;
			player3JoinButton.SetActive(true);

		}

	}

	public void player4SetActive(bool active)
	{
		if(active && player4Enabled == false)
		{
			player4Enabled = true;
			settings.playerCount++;
			player4JoinButton.SetActive(false);
		}
		else if(active == false && player4Enabled == true)
		{
			player4Enabled = false;
			settings.playerCount--;
			player4JoinButton.SetActive(true);
			
		}
		
	}

}

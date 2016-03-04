﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MatchOptions : MonoBehaviour {

	public int level = 0;
	public int timeLimit;
	public int lives;
	public int playerCount = 2;

	//string[8] livesSettings = new string["1","2","3","4","5","6","7","∞"];
	void Awake()
	{
		// search for another existing match options
		// if one exists
		// destroy self
		// else
		DontDestroyOnLoad(this.gameObject);
	}
	public void setLevel(int levelNumber)
	{
		level = levelNumber;
	}

	public void setTime(int limit)
	{
		timeLimit = limit;
	}

	public void setLives(int numberOfLives)
	{
		lives = numberOfLives;
	}

	public void LoadScene()
	{
		Application.LoadLevel(level);
	}
}	
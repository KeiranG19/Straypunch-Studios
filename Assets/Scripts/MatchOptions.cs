using UnityEngine;
using System.Collections;

public class MatchOptions : MonoBehaviour {

	private int level;
	private int timeLimit;
	private int lives;

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

	public void LoadScene(int level2)
	{
		Application.LoadLevel(level2);
	}
}	
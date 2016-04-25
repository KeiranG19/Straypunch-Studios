using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class gameController : MonoBehaviour 
{
	public List<playerCharacter> players = new List<playerCharacter>();		// List of all players
	public List<playerCharacter> alivePlayers = new List<playerCharacter>();// List of all live players
	public List<playerSpawn> spawnPoints = new List<playerSpawn>();			// List of all spawn points
	public Image leCurton;

	public MatchOptions settings;											// Referance to level options which have been passed in from menu

	void Start()
	{
		GameObject tempSettings = GameObject.Find ("level_settings");
			
		if(tempSettings != null)
		{
			settings = tempSettings.GetComponent<MatchOptions>();
			for(int i = 0; i<settings.playerCount;i++)
			{
				foreach(playerSpawn spawn in spawnPoints)
				{
					if(spawn.playerStartSpawn == i+1)
					{
						spawn.spawnPlayer(i+1);
					}
				}
			}
		}
		else
		{
			for(int i = 0; i<4;i++)
			{
				foreach(playerSpawn spawn in spawnPoints)
				{
					if(spawn.playerStartSpawn == i+1)
					{
						spawn.spawnPlayer(i+1);
					}
				}
			}
			Debug.Log("Menu settings not found, using defaults");
		}

	}

	void Update()
	{
		if (alivePlayers.Count == 1) 
		{
			settings.winningPlayer = alivePlayers[0].ID;
			StartCoroutine(endGame());
		}
	}

	private IEnumerator endGame()
	{
		//healthBar.color = new Color ( myImage.color.r, Mathf.Lerp(myImage.color.g, targetIntensity, fadeSpeed * Time.deltaTime), Mathf.Lerp(myImage.color.b, targetIntensity, fadeSpeed * Time.deltaTime));
		leCurton.color = new Color(leCurton.color.r,leCurton.color.g,leCurton.color.b,Mathf.Lerp (leCurton.color.a, 1, 0.8f * Time.deltaTime));
		yield return new WaitForSeconds (1.5f);
		Application.LoadLevel (2);
	}

	public playerCharacter getPlayer(int id)
	{
		playerCharacter tempPlayer = new playerCharacter();
		foreach (playerCharacter player in players) 
		{

			if(player.ID == id)
			{
				tempPlayer = player;
				return tempPlayer;
			}

		}
		return null;
	}
}

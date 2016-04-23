using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class gameController : MonoBehaviour 
{
	public List<playerCharacter> players = new List<playerCharacter>();		// List of all players
	public List<playerSpawn> spawnPoints = new List<playerSpawn>();			// List of all spawn points

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


}

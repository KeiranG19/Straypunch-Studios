using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buffsManager : MonoBehaviour 
{
	private gameController manager; 	// Referance to the game controller
	
	void Awake()
	{
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
	}

	void Update () 
	{
		foreach(playerCharacter player in manager.players)
		{
			if(player.buffs.Count > 0)
			{
				foreach(buff effect in player.buffs)
				{
					effect.applyEffect(player);
				}
			}
			if(player.debuffs.Count > 0)
			{
				foreach(debuff effect in player.debuffs)
				{
					effect.applyEffect(player);
				}
			}
		}
	}

}

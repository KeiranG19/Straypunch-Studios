using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class buffsManager : MonoBehaviour 
{
	// we need somthing to instanciate a buff once, then monitor that object fill amount without creating it again
	// player.buffSlot
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
				foreach(buff effect in player.buffs.ToArray())
				{
					effect.applyEffect(player);
				}
			}

			float highestDamage = float.NegativeInfinity;
			float highestSlow = float.NegativeInfinity;
			// find the slow with the highest slow amount and remove the other slow debuffs, as they are redundant 
			// if a debuff is not a slow apply its effect straight away
			if(player.debuffs.Count > 0)
			{
				foreach(debuff effect in player.debuffs.ToArray())
				{
					if(effect.Type == debuff.type.slow)
					{
						if(effect.slowSpeed > highestSlow)
						{
							highestSlow = effect.slowSpeed;
						}
						else
						{
							player.debuffs.Remove(effect);
						}

					}
					else if(effect.Type == debuff.type.DOT)
					{
						if(effect.DOTdamage > highestDamage)
						{
							highestDamage = effect.DOTdamage;
						}
						else
						{
							player.debuffs.Remove(effect);
						}
					}
					else
					{
						effect.applyEffect(player);
					}
				}
			}
			if(player.debuffs.Count > 0)
			{
				foreach(debuff effect in player.debuffs.ToArray())
				{
					if(effect.Type == debuff.type.slow)
					{
						effect.applyEffect(player);
					}
					if(effect.Type == debuff.type.DOT)
					{
						effect.applyEffect(player);
					}
				}
			}
		}
	}

}

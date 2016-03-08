using UnityEngine;
using System.Collections;

public class debuff : MonoBehaviour 
{
	public float DoTCooldown;	// Cooldown between dot damage being applied
	public float DOTdamage;		// Amount of damage per dot tick
	public int slowSpeed;		// Amount of speed taken from the player

	public enum type{			
		DOT,				 	// damage over time
		slow,				 	// slowed speed
		stun,		 		 	// unable to act
		dazed};		 		 	// controlls inversed
	public type Type;			

	private float lifeTime;		// How long the debuff will last ( given by hazard )
	private float DoTCD = 0;	// Cooldown between dot damage being applied
	
	void Update () 
	{
		if(DoTCD >0)
		{
			DoTCD -= Time.deltaTime;
		}
		lifeTime -= Time.deltaTime;
	}

	public void giveDebuff( playerCharacter player, float time, type debuffType) 
	{
		Type = debuffType;
		lifeTime = time;
		player.debuffs.Add(this);
	}

	public void applyEffect(playerCharacter player)
	{
		if(Type == type.DOT)
		{
			if(DoTCD <= 0)
			{
				player.health -= DOTdamage;
				DoTCD += DoTCooldown;
			}

			if(lifeTime < 0)
			{
				player.debuffs.Remove(this);
			}
		}
		else if(Type == type.slow)
		{
			player.GetComponent<RigidBodyControls>().speed = player.GetComponent<RigidBodyControls>().maxSpeed;
			player.GetComponent<RigidBodyControls>().speed -= slowSpeed;

			if(lifeTime < 0)
			{
				player.GetComponent<RigidBodyControls>().speed = player.GetComponent<RigidBodyControls>().maxSpeed;
				player.debuffs.Remove(this);
			}
		}
		else if(Type == type.stun)
		{
			player.stunned = true;
			
			if(lifeTime < 0)
			{
				player.stunned = false;
				player.debuffs.Remove(this);
			}
		}
		else
		{

		}
	}

}

using UnityEngine;
using System.Collections;

public class buff : MonoBehaviour 
{
	public float HoTCooldown;	// Cooldown between hot tick being applied
	public float HoTamount;		// Amount of heal per hot tick
	public int speedIncrease;	// Amount of speed increase
	public enum type{
		HOT,		 			// heal over time
		Speed		 			// speed buff
		};		 
	public type Type;			
	
	public float lifeTime;		// How long the debuff will last ( given by powerup )
	private float HoTCD = 0;	// Cooldown between HoT being applied
	
	
	void Update () 
	{
		if(HoTCD >0)
		{
			HoTCD -= Time.deltaTime;
		}
		lifeTime -= Time.deltaTime;
	}
	
	public void giveBuff( playerCharacter player, float time, type buffType) 
	{
		Type = buffType;
		lifeTime = time;
		player.buffs.Add(this);

	}
	
	public void applyEffect(playerCharacter player)
	{
		if(Type == type.HOT)
		{
			if(HoTCD <= 0)
			{
				player.health += HoTamount;
				HoTCD += HoTCooldown;
			}
			if(lifeTime < 0)
			{
				player.buffs.Remove(this);
			}
		}

		else if(Type == type.Speed)
		{
			player.GetComponent<RigidBodyControls>().speed = player.GetComponent<RigidBodyControls>().maxSpeed;
			player.GetComponent<RigidBodyControls>().speed += speedIncrease;
			
			if(lifeTime < 0)
			{
				player.GetComponent<RigidBodyControls>().speed = player.GetComponent<RigidBodyControls>().maxSpeed;
				player.buffs.Remove(this);
			}
		}
		else
		{

		}

	}
}

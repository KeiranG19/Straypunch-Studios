using UnityEngine;
using System.Collections;

public class debuff : MonoBehaviour 
{
	public float DOTdamage;
	public enum type{
		DOT,		 // damage over time
		slow,		 // slowed speed
		stun,		 // unable to act
		dazed};		 // controlls inversed

	private float lifeTime;
	private type myType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void giveDebuff( playerCharacter player, float time, type debuffType) 
	{
		myType = debuffType;
		lifeTime = time;
		player.debuffs.Add(this);
	}

	public void applyEffect(playerCharacter player)
	{
		if(myType == type.DOT)
		{
			player.health -= DOTdamage;
			lifeTime -= Time.deltaTime;

			if(lifeTime < 0)
			{
				player.debuffs.Remove(this);
			}
		}
		else if(myType == type.slow)
		{
			//player.rigidBodyControls.speed -= slowSpeed;
		}
		else if(myType == type.stun)
		{
			player.stunned = true;
			lifeTime -= Time.deltaTime;
			
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

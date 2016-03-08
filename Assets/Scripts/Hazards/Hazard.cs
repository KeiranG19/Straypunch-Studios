using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{
	/*
	 * if bool active = true ( set true by button with public Hazard variable)
	 * then do triggerHazard, triggerHazard will get the type of Hazard it is from public enum 'type'
	 * and preform an action based on the type.
	 * 
	 */
	public enum type{
		area_damage, 
		area_slow,
		area_DOT
	};
	public type Type;
	public int damage;
	public int DoTDamage;
	public bool activated = false;
	public int slowAmount;
	public float waitTimer = 0.5f;
	public float debuffTime = 3;
	public debuff effect;
	private float timer = 0.5f;

	void Start () 
	{

	}

	void Update () 
	{
		if(activated == true)
		{
			Component[] children = GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem childParticleSystem in children)
			{
				childParticleSystem.enableEmission = true;
			}
		}
		else
		{
			Component[] children = GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem childParticleSystem in children)
			{
				childParticleSystem.enableEmission = false;
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		if(activated == true)
		{
			if(other.gameObject.tag == "Player")
			{
				playerCharacter myPlayer = other.GetComponent<playerCharacter>();
				RigidBodyControls myRigidBody = other.GetComponent<RigidBodyControls>();

				if(Type == type.area_damage)
				{
					if(timer >= waitTimer)
					{
						myPlayer.health -= damage;
						timer = 0.0f;
					}
					else
					{
						timer += Time.deltaTime;
					}
				}
				if(Type == type.area_slow)
				{
					if(timer >= waitTimer)
					{
						effect.slowSpeed = slowAmount;
						effect.giveDebuff(myPlayer,debuffTime,debuff.type.slow);
						timer = 0.0f;
					}
					else
					{
						timer += Time.deltaTime;
					}
				}
				if(Type == type.area_DOT)
				{
					if(timer >= waitTimer)
					{
						effect.DOTdamage = DoTDamage;
						effect.giveDebuff(myPlayer,debuffTime,debuff.type.DOT);
						timer = 0.0f;
					}
					else
					{
						timer += Time.deltaTime;
					}
				}
			}
		}
	}

//	void OnTriggerExit(Collider other)
//	{
//		if(activated == true)
//		{
//			if(other.gameObject.tag == "Player")
//			{
//				RigidBodyControls myRigidBody = other.GetComponent<RigidBodyControls>();
//				if(Type == type.area_slow)
//				{
//					myRigidBody.speed = 10;
//				}
//			}
//		}
//
//	}


}

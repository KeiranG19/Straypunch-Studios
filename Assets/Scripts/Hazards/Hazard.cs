using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {
	/*
	 * if bool active = true ( set true by button with public Hazard variable)
	 * then do triggerHazard, triggerHazard will get the type of Hazard it is from public enum 'type'
	 * and preform an action based on the type.
	 * 
	 */
	public enum type{
		area_damage, 
		area_slow};


	public type Type;
	public int damage;
	public bool activated = false;
	public int slowAmount;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(activated == true)
		{

			Component[] children = GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem childParticleSystem in children)
			{
				
				childParticleSystem.enableEmission = true;

			}

		}
		else{
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
				if(Type == type.area_damage)
				{
					myPlayer.rotationMultiplier = 30;
				}
				if(Type == type.area_slow)
				{
					myPlayer.walkingSpeed = 1;
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(activated == true)
		{
			if(other.gameObject.tag == "Player")
			{
				playerCharacter myPlayer = other.GetComponent<playerCharacter>();

				if(Type == type.area_slow)
				{
					myPlayer.walkingSpeed = 10;
				}
			}
		}

	}


}

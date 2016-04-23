using UnityEngine;
using System.Collections;

public class powerup : MonoBehaviour 
{

	public enum Type{
		heal, 
		speed
	};

	public Type type;

	public float healTime;
	public float speedTime;

	public float respawnTime;
	private float timer;
	public bool ready = true;

	private GameObject particles;
	private Animation fadeOut;
	void Start()
	{
		particles = transform.FindChild ("particles").gameObject;
		fadeOut = GetComponent<Animation> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && ready) 
		{
			if(type == Type.heal)
			{
				other.gameObject.GetComponent<PlayerBuffs>().healCooldown = healTime;

			}
			else if(type == Type.speed)
			{
				other.gameObject.GetComponent<PlayerBuffs>().speedCooldown = speedTime;
			}
			particles.SetActive(false);
			timer = 0;
			ready = false;
			fadeOut.Play("healPowerupPickup");
		}
	}

	void Update()
	{
		if (timer > respawnTime && !ready) 
		{
			//respawn
			if(type==Type.heal)
			{

			}
			particles.SetActive (true);
			ready = true;
			fadeOut.Play("healPowerupIdle");
		} 
		else 
		{
			timer += Time.deltaTime;
		}
	}
}

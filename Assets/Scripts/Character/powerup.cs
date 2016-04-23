using UnityEngine;
using System.Collections;

public class powerup : MonoBehaviour 
{

	public enum Type{
		heal, 
		speed,
		grow,
		attack
	};

	public Type type;

	public float healTime;
	public float speedTime;
	public float growTime;
	public float attackTime;

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
			else if(type == Type.grow)
			{
				other.gameObject.GetComponent<PlayerBuffs>().growCooldown = growTime;
			}
			else if(type == Type.attack)
			{
				other.gameObject.GetComponent<PlayerBuffs>().attackCooldown = attackTime;
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

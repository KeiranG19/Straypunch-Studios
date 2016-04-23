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

	private GameObject particles;

	void Start()
	{
		particles = transform.FindChild ("particles").gameObject;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			if(type == Type.heal)
			{
				other.gameObject.GetComponent<PlayerBuffs>().healCooldown = healTime;
			}
			if(type == Type.speed)
			{
				other.gameObject.GetComponent<PlayerBuffs>().speedCooldown = speedTime;
			}
			gameObject.renderer.enabled = false;
			particles.SetActive(false);
			timer = 0;
		}
	}

	void Update()
	{
		if (timer > respawnTime) 
		{
			//respawn
		}
	}
}

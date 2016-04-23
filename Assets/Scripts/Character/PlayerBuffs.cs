using UnityEngine;
using System.Collections;

public class PlayerBuffs : MonoBehaviour {

	private playerCharacter player;
	private RigidBodyControls rigid;

	private GameObject fire;
	private GameObject heal;
	private GameObject speed;
	public float fireCooldown;
	public float healCooldown;
	public float speedCooldown;

	public float fireDamagePerTick;
	public float healPerTick;
	public float speedMultiplier;

	
	// Use this for initialization
	void Start () {
		player = GetComponent<playerCharacter> ();
		rigid = GetComponent<RigidBodyControls> ();
		fire = transform.FindChild ("fire").gameObject;
		heal = transform.FindChild ("heal").gameObject;
		speed = transform.FindChild ("trail").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (fireCooldown <= 0) 
		{
			fire.SetActive(false);
		} 
		else 
		{
			fire.SetActive(true);
			fireCooldown -= Time.deltaTime;
			player.health -= fireDamagePerTick;
		}

		if (healCooldown <= 0) 
		{
			heal.SetActive(false);
		} 
		else 
		{
			heal.SetActive(true);
			healCooldown -= Time.deltaTime;
			player.health += healPerTick;
		}

		if (speedCooldown <= 0) 
		{
			speed.SetActive(false);
			rigid.speedMultiplier = 1;
		} 
		else 
		{
			speed.SetActive(true);
			speedCooldown -= Time.deltaTime;
			rigid.speedMultiplier = speedMultiplier;
		}
	}

	public void clear()
	{
		fireCooldown = 0;
		healCooldown = 0;
		speedCooldown = 0;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerBuffs : MonoBehaviour {

	private GameObject fire;
	private GameObject heal;
	private GameObject speed;
	private GameObject stun;
	public float fireCooldown;
	public float healCooldown;
	public float speedCooldown;
	public float stunCooldown;
	
	// Use this for initialization
	void Start () {
		fire = transform.FindChild ("fire").gameObject;
		heal = transform.FindChild ("heal").gameObject;
		speed = transform.FindChild ("trail").gameObject;
		stun = transform.FindChild ("stun").gameObject;
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
		}

		if (healCooldown <= 0) 
		{
			heal.SetActive(false);
		} 
		else 
		{
			heal.SetActive(true);
			healCooldown -= Time.deltaTime;
		}

		if (speedCooldown <= 0) 
		{
			speed.SetActive(false);
		} 
		else 
		{
			speed.SetActive(true);
			speedCooldown -= Time.deltaTime;
		}

		if (stunCooldown <= 0) 
		{
			stun.SetActive(false);
		} 
		else 
		{
			stun.SetActive(true);
			stunCooldown -= Time.deltaTime;
		}
	}
}

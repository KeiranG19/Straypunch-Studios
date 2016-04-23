using UnityEngine;
using System.Collections;

public class hazard : MonoBehaviour {

	public bool isActive = false;
	public float burnTime;
	private GameObject particles;
	
	void Start () {
		particles = transform.FindChild ("particles").gameObject;
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag== "Player" && isActive) 
		{
			other.gameObject.GetComponent<PlayerBuffs>().fireCooldown = burnTime;
		}
	}

	public void toggle()
	{
		if (isActive) 
		{
			isActive = false;
			particles.SetActive(false);
		} 
		else 
		{
			isActive = true;
			particles.SetActive(true);
		}
	}
}

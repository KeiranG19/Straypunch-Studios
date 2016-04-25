using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {

	public GameObject ripples;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			other.gameObject.GetComponent<PlayerBuffs>().fireCooldown=0;
			Vector3 position = other.transform.position;
			position.y = 7.8f;
			Instantiate(ripples,position,Quaternion.Euler(-90,0,0));
		}
	}
}

using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			other.gameObject.GetComponent<PlayerBuffs>().fireCooldown=0;
		}
	}
}

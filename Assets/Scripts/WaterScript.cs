using UnityEngine;
using System.Collections;

public class WaterScript : MonoBehaviour {

	public GameObject ripples;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			other.gameObject.GetComponent<PlayerBuffs>().fireCooldown=0;
			Instantiate(ripples,other.transform.position+ new Vector3(0,0.5f,0),Quaternion.Euler(-90,0,0));
		}
	}
}

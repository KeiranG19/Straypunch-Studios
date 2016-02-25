using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class hitBox : MonoBehaviour {

	public List<GameObject> targets = new List<GameObject>();

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			targets.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			targets.Remove(other.gameObject);
		}
	}
}

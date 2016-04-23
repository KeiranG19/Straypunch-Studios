using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {

	public hazard Hazard;

	public bool buttonOn=false;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			buttonOn = !buttonOn;

			Hazard.toggle();
		}
	}
}

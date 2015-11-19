using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public Hazard Trigger;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Trigger.active = true;
			Vector3 newY = new Vector3(0,-0.15f,0);
			this.transform.position += newY;
		}
			
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
		Trigger.active = false;
		Vector3 newY = new Vector3(0,0.15f,0);
		this.transform.position += newY;
		}
	}
}

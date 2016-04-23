using UnityEngine;
using System.Collections;

public class hazard : MonoBehaviour {

	public bool isActive = false;

	private GameObject particles;
	
	void Start () {
		particles = transform.FindChild ("particles").gameObject;
	}
	
	void OnTriggerEnter(Collider other)
	{
	
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

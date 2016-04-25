using UnityEngine;
using System.Collections;

public class Destructable : MonoBehaviour {

	void Update () {
		if (particleSystem.isStopped) 
		{
			Destroy (gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class hammerController : MonoBehaviour {

	public playerCharacter pcOwner;

	public hammerPhysics slowHit;
	public fastHammerPhysics fastHit;
	// Use this for initialization
	void Start () {
		pcOwner = transform.root.GetComponent<playerCharacter> ();
		slowHit = GetComponentInChildren<hammerPhysics> ();
		fastHit = transform.root.GetComponentInChildren<fastHammerPhysics> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (pcOwner.rotationMultiplier > 30) {
			slowHit.isEnabled = false;
			fastHit.isEnabled = true;
		} 
		else 
		{
			slowHit.isEnabled = true;
			fastHit.isEnabled = false;
		}
	}
}

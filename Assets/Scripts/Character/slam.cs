using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class slam : MonoBehaviour {

	private playerCharacter player;

	List<Collider> hitList = new List<Collider> ();

	public float damage = 60;
	public float force = 10;
	public float maxDamageThreshold = 1;
	public float damageThreshold = 7;
	public bool isEnabled = false;

	// Use this for initialization
	void Start () 
	{
		player = GetComponentInParent<playerCharacter> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && other.gameObject != this.gameObject && isEnabled && !hitList.Contains(other)) 
		{
			hitList.Add(other);
			playerCharacter target = other.GetComponent<playerCharacter> ();
			Vector3 position = other.transform.position;
			float distance = Vector3.Distance(position,transform.position);
			Vector3 direction = position-transform.position;
			direction.y = 0;
			if(distance <= maxDamageThreshold)
			{
				//max damage
				target.health -= damage * player.damageMultiplier;
				target.Ragdoll = true;
				target.previousRotation = target.transform.rotation;
			}
			else if(distance <= damageThreshold)
			{
				//scaled damage
				target.health -= (damage/distance) * player.damageMultiplier;
				target.transform.position += new Vector3(0,0.05f,0);
				target.rigidbody.AddForce(direction*force/distance);
				target.Ragdoll = true;
				target.previousRotation = target.transform.rotation;
			}
			else
			{
				//no damage
				target.transform.position += new Vector3(0,0.05f,0);
				target.rigidbody.AddForce(direction*force/distance);
			}
		}
	}

	public void clear()
	{
		hitList.Clear ();
	}

	// Update is called once per frame
	void Update () 
	{
	}
}

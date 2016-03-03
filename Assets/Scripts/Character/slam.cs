using UnityEngine;
using System.Collections;

public class slam : MonoBehaviour {

	public float damage = 60;
	public float force = 10;
	public float maxDamageThreshold = 1;
	public float damageThreshold = 7;
	public bool isEnabled = false;
	
	private gameController manager;
	// Use this for initialization
	void Start () {
		manager = GetComponentInParent<playerCharacter> ().manager;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && other.gameObject != this.gameObject && isEnabled) 
		{
			playerCharacter target = other.GetComponent<playerCharacter> ();
			Vector3 position = other.transform.position;
			float distance = Vector3.Distance(position,transform.position);
			Vector3 direction = position-transform.position;
			direction.y = 0;
			if(distance <= maxDamageThreshold)
			{
				//max damage
				target.health -= damage;
				target.Ragdoll = true;
				target.previousRotation = target.transform.rotation;
			}
			else if(distance <= damageThreshold)
			{
				//scaled damage
				target.health -= damage/distance;
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

	// Update is called once per frame
	void Update () 
	{
//		if(isEnabled)
//		{
//		foreach(playerCharacter other in manager.players)
//		{
//			MeshCollider area =  GetComponent<MeshCollider>();
//
//			if (area.bounds.Contains(other.transform.position) && other.gameObject != transform.parent.gameObject) 
//			{
//				Vector3 position = other.transform.position;
//				float distance = Vector3.Distance(position,transform.position);
//				if(distance <= maxDamageThreshold)
//				{
//					//max damage
//					other.health -= damage;
//						Debug.Log("max damage applied");
//				}
//				else if(distance <= damageThreshold)
//				{
//					//scaled damage
//					other.health -= damage/distance;
//					other.rigidbody.AddForce((transform.position-position)*force/distance);
//						Debug.Log("mid damage applied");
//				}
//				else
//				{
//					//no damage
//					other.rigidbody.AddForce((transform.position-position)*force/distance);
//						Debug.Log("slow applied");
//				}
//
//			}
//		}
//			isEnabled = false;
//		}
	}
}

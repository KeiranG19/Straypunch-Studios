﻿using UnityEngine;
using System.Collections;

public class fastHammerPhysics : MonoBehaviour {

	public playerCharacter pcOwner;

	public bool isEnabled = false;

	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<MeshCollider>(), transform.root.collider); 

		pcOwner = transform.root.GetComponent<playerCharacter> ();
		
	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Player" && isEnabled) 
		{
			playerCharacter	enemyPC = hit.gameObject.GetComponent<playerCharacter>();

			float hammerOffset = 0.7f;
			float heightOffset = enemyPC.transform.position.y - transform.position.y;
			Vector3 contactPoint = enemyPC.transform.position;
			contactPoint.y += hammerOffset + heightOffset;

			enemyPC.Ragdoll = true;
			enemyPC.previousRotation = enemyPC.transform.rotation;
			//enemyPC.rigidbody.constraints = RigidbodyConstraints.None;
			Vector3 direction = Vector3.Cross((enemyPC.transform.position - transform.position),Vector3.up);
			direction.y = Random.Range(-0.3f,0.3f);
			Vector3 forceVec = (direction.normalized * (pcOwner.rotationMultiplier/2));
			enemyPC.health -= forceVec.magnitude * pcOwner.damageMultiplier;
			hit.rigidbody.AddForce(forceVec,ForceMode.Impulse);
			hit.rigidbody.AddTorque(Vector3.Cross(forceVec , contactPoint)*5,ForceMode.Impulse);
			pcOwner.rotationMultiplier /= 2;
		}
	}
} 

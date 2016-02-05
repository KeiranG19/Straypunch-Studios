using UnityEngine;
using System.Collections;
/*
 * Last time we were doing ragdoll, need to make it rotate back to normal after a couple seconds
 * 
 * ideas
 * if near max hammer rotation
 * move player towards hammer head
 */
public class hammerPhysics : MonoBehaviour {

	public float damageThreshold = 15.0f;

	public playerCharacter pcOwner;

	public bool isEnabled = true;

	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), transform.root.collider); 
		pcOwner = transform.root.GetComponent<playerCharacter> ();

	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Player" && isEnabled) 
		{
			playerCharacter	enemyPC = hit.gameObject.GetComponent<playerCharacter>();

			float hammerOffset = 0.8f;
			float heightOffset = enemyPC.transform.position.y - transform.position.y;
			Vector3 contactPoint = enemyPC.transform.position;
			contactPoint.y += hammerOffset + heightOffset;

			Vector3 direction = (enemyPC.transform.position - transform.position);
			Vector3 forceVec = (direction.normalized * (pcOwner.rotationMultiplier));
			if(forceVec.magnitude > damageThreshold)
				enemyPC.health -= forceVec.magnitude;
			hit.rigidbody.AddForce(forceVec,ForceMode.Impulse);
			hit.rigidbody.AddTorque(Vector3.Cross(forceVec , contactPoint)*5,ForceMode.Impulse);
			pcOwner.rotationMultiplier = -pcOwner.rotationMultiplier/2;
		

		}
	}




}

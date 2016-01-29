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



			Vector3 forceVec = ((pcOwner.rigidbody.velocity.normalized + enemyPC.rigidbody.velocity.normalized) * (pcOwner.rotationMultiplier)) / 3;
			Vector3 reducedForceY = new Vector3(forceVec.x,forceVec.y / 6,forceVec.z); //the Y force was to great
			hit.rigidbody.AddForce(reducedForceY,ForceMode.Impulse);
			hit.rigidbody.AddTorque(forceVec * 5,ForceMode.Impulse);

		}
	}




}

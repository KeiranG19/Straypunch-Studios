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
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), transform.root.collider); 
//		rigidbody.Sleep ();
		pcOwner = transform.root.GetComponent<playerCharacter> ();

	}

	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Player") 
		{
			playerCharacter	enemyPC = hit.gameObject.GetComponent<playerCharacter>();
			//	float multiplier =  20;
//			float multiplier =  pcOwner.rotationMultiplier;
//			Vector3 push2 = new Vector3 (3f+pcOwner.velocityChange.x, 0.1f+pcOwner.velocityChange.y, 3f+pcOwner.velocityChange.z)*multiplier; 
//			enemyPC.addedVel = push2;


			Vector3 forceVec = ((pcOwner.rigidbody.velocity.normalized + enemyPC.rigidbody.velocity.normalized) * (pcOwner.rotationMultiplier)) / 3;
			Vector3 reducedForceY = new Vector3(forceVec.x,forceVec.y / 6,forceVec.z); //the Y force was to great
			hit.rigidbody.AddForce(reducedForceY,ForceMode.Impulse);
			hit.rigidbody.AddTorque(forceVec * 5,ForceMode.Impulse);
			//enemyCC.Move(push);
		}
	}




}

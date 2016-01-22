using UnityEngine;
using System.Collections;

public class fastHammerPhysics : MonoBehaviour {

	public playerCharacter pcOwner;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<MeshCollider>(), transform.root.collider); 
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
			float hammerOffset = 0.8f;
			float heightOffset = enemyPC.transform.position.y - transform.position.y;
			Vector3 contactPoint = enemyPC.transform.position;
			contactPoint.y += hammerOffset + heightOffset;

			enemyPC.Ragdoll = true;
			enemyPC.rigidbody.freezeRotation = false;
			Vector3 direction = Vector3.Cross((enemyPC.transform.position - transform.position),Vector3.up);
			direction.y = Random.Range(-0.3f,0.3f);
			Vector3 forceVec = (direction.normalized * (pcOwner.rotationMultiplier));
			hit.rigidbody.AddForce(forceVec,ForceMode.Impulse);
			hit.rigidbody.AddTorque(Vector3.Cross(forceVec , contactPoint)*5,ForceMode.Impulse);
			pcOwner.rotationMultiplier /= 2;
			//enemyCC.Move(push);
		}
	}
} 

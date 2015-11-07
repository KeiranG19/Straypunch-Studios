using UnityEngine;
using System.Collections;

public class hammerPhysics : MonoBehaviour {


	public playerCharacter pcOwner;
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(gameObject.GetComponent<CapsuleCollider>(), transform.root.collider); 
//		rigidbody.Sleep ();
		pcOwner = transform.root.GetComponent<playerCharacter> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnControllerColliderHit  (ControllerColliderHit collision) {

		// Debug-draw all contact points and normals

		
		// Play a sound if the colliding objects had a big impact.		
		/*if (collision.relativeVelocity.magnitude > 2) {
			//audio.Play();
		}
		*/

		if (collision.rigidbody != null && !collision.collider.attachedRigidbody.isKinematic) 
		{
			float multiplier =  pcOwner.rotationMultiplier;
			Rigidbody RB = collision.gameObject.GetComponent<Rigidbody>();
			//Grab the rigidbody of what we've collided with, if it exists and isn't kinematic, continue
			
			//if (hit.moveDirection.y < -0.3) { return; } //don't affect objects under player
			
			Vector3 push = new Vector3 (collision.moveDirection.x, collision.moveDirection.y, collision.moveDirection.z)*multiplier;  //Make a vector of velocity to push the rigidbody
			//RB.velocity = RB.velocity + push * 1f; //Apply the force


			/*foreach (ContactPoint contact in collision.contacts) {
				Debug.DrawRay(contact.point, contact.normal, Color.white,10.0f);
			//	RB.AddForceAtPosition(push, contact.point);
				RB.AddRelativeForce(push);
				RB.AddRelativeTorque(push);
			}
			*/

			if (collision.gameObject.tag == "Player") 
			{
				playerCharacter	enemyPC = collision.gameObject.GetComponent<playerCharacter>();
			//	float multiplier =  20;
				Vector3 push2 = new Vector3 (collision.moveDirection.x, collision.moveDirection.y, collision.moveDirection.z)*multiplier; 
				enemyPC.addedVel = push2;
				
				//enemyCC.Move(push);
			}


		}



	} 


}

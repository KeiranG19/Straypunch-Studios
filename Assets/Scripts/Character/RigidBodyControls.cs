﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidBodyControls : MonoBehaviour {
	public float maxSpeed = 10.0f;				// player speed before debuffs
	public float speed = 10.0f;					// player speed after debuffs
	public float dashSpeed = 20.0f;
	public float gravity = 30.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public bool grounded = false;
	public playerCharacter player;
	private XboxControls controllerInput;
	public int maxDashes = 3;
	public int remainingDashes = 3;
	public float dashUseCooldown = 0.5f;
	public float regenDashCooldown = 5;
	private float regenDashCD;
	private float dashUseCD ;
	private float myMaxSpeed;
	void Awake () 
	{
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	void Start()
	{
		player = GetComponent<playerCharacter> ();
		controllerInput = GetComponent<XboxControls>();
		
	}

	void FixedUpdate ()
	{

		
		if(remainingDashes < maxDashes)
		{

			if(regenDashCD >0)
			{
				regenDashCD -= Time.deltaTime;
			}
			else
			{
				remainingDashes ++;
				regenDashCD = regenDashCooldown;
			}
		}
		if(dashUseCD > 0)
		{
			dashUseCD -= Time.deltaTime;
		}

		if (player.isAlive && !player.Ragdoll)
		{
			if (grounded) {
				if(Input.GetButtonUp(controllerInput.buttons.B))
				{
					if(dashUseCD <= 0)
					{
						if(remainingDashes > 0)
						{
							//transform.position = Vector3.Lerp(transform.position, transform.position+transform.right*dashSpeed,0.5f);
							transform.position += new Vector3(0,0.1f,0);
							rigidbody.AddForce(transform.right*dashSpeed);
							remainingDashes --;
							dashUseCD = dashUseCooldown;
						}
					}
				}
				else
				{
				// Calculate how fast we should be moving
				Vector3 direction = new Vector3 (Input.GetAxis (controllerInput.buttons.movementHorizontalAxis), 0, Input.GetAxis (controllerInput.buttons.movementVerticalAxis));
				Vector3 targetVelocity = direction * speed;
			
				// Apply a force that attempts to reach our target velocity
				Vector3 velocity = rigidbody.velocity;
				Vector3 velocityChange = (targetVelocity - velocity);
				velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
				velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
				velocityChange.y = 0;
				rigidbody.AddForce (velocityChange, ForceMode.VelocityChange);
			
				// Jump
				if (canJump && Input.GetButton (controllerInput.buttons.A)) {
					rigidbody.velocity = new Vector3 (velocity.x, CalculateJumpVerticalSpeed (), velocity.z);
				}
				}
			}
		}
			// We apply gravity manually for more tuning control
			rigidbody.AddForce (new Vector3 (0, -gravity * rigidbody.mass, 0));
		
			grounded = false;
		
	}
	
	void OnCollisionStay () {
		grounded = true;    
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

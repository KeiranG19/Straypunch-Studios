﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class RigidBodyControls : MonoBehaviour {			
	public float speed = 10.0f;					
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
	public float speedMultiplier = 1;
	public Animator animator;
	public GameObject trailEffect;
	public float jumpUseCD = 0;
	public float jumpUseCooldown = 0.5f;
	void Awake () 
	{
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		player = GetComponent<playerCharacter> ();
		controllerInput = GetComponent<XboxControls>();
		animator = GetComponentInChildren<Animator> ();
		trailEffect = transform.FindChild("dashTrail").gameObject;
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

			trailEffect.SetActive(true);
			dashUseCD -= Time.deltaTime;
		}
		else
		{
			player.dashAudio = false;
			trailEffect.SetActive(false);
		}

		if (jumpUseCD > 0) 
		{
			jumpUseCD -= Time.deltaTime;
		} 
		else 
		{
			player.jumpAudio = false;
		}

		if (player.isAlive && !player.Ragdoll)
		{
			if(Input.GetButtonUp(controllerInput.buttons.B))
			{
				if(dashUseCD <= 0 && grounded)
				{
					if(remainingDashes > 0)
					{
						//transform.position += new Vector3(0,0.1f,0);

						rigidbody.AddForce(transform.right*dashSpeed);
						remainingDashes --;
						dashUseCD = dashUseCooldown;
						player.myAudio.clip = player.soundEffect.dash;
						player.myAudio.Play();
						player.dashAudio = true;
					}
				}
			}
			else
			{
				Vector3 velocity = rigidbody.velocity;
				if(!player.slam)
				{
					// Calculate how fast we should be moving
					Vector3 direction = new Vector3 (Input.GetAxis (controllerInput.buttons.movementHorizontalAxis), 0, Input.GetAxis (controllerInput.buttons.movementVerticalAxis));
					Vector3 targetVelocity = direction * speed* speedMultiplier;
					Vector3 planeVelocity = targetVelocity;
					planeVelocity.y=0;
					animator.SetFloat("Speed", planeVelocity.magnitude);
					// Apply a force that attempts to reach our target velocity
				
					Vector3 velocityChange = (targetVelocity - velocity);
					velocityChange.x = Mathf.Clamp (velocityChange.x, -maxVelocityChange, maxVelocityChange);
					velocityChange.z = Mathf.Clamp (velocityChange.z, -maxVelocityChange, maxVelocityChange);
					velocityChange.y = 0;
					rigidbody.AddForce (velocityChange, ForceMode.VelocityChange);
				}


				if (grounded) 
				{
					// Jump
					if (canJump && Input.GetButton (controllerInput.buttons.A)) 
					{
						rigidbody.velocity = new Vector3 (velocity.x, CalculateJumpVerticalSpeed (), velocity.z);
						animator.SetTrigger("jumpTrigger");
						player.myAudio.clip = player.soundEffect.jump;
						player.myAudio.Play();
						jumpUseCD = jumpUseCooldown;
						player.jumpAudio = true;
					}
				}

			}
		}
		// We apply gravity manually for more tuning control
		rigidbody.AddForce (new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
		if (rigidbody.velocity.sqrMagnitude > 0.01) 
		{
			player.idleTime = 0;
		}
	}
	
	void OnCollisionStay(Collision other) {
		if (rigidbody.velocity.y < 0.1 && other.gameObject.tag != "Wall") 
		{
			grounded = true;    
		} 
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}

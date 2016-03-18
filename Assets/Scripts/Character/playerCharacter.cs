using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class playerCharacter : MonoBehaviour {
	private Rigidbody RB;

	public float health = 200;
	public bool isAlive = true;
	public bool stunned = false;						// set by debuffs, used to lock the player's movement and attacks
	public List<buff> buffs = new List<buff>();			// List of all positive effects on the player, used by buffsManager
	public List<debuff> debuffs = new List<debuff>();	// List of all negative effects on the player, used by buffsManager

	public GameObject buffSlots;						// Grid layout group to show an image for each active buff (set by showBuffs )

	public float uppercutDamage;
	public float uppercutForce;
	public float uppercutCooldown;

	public float slamSpeed;
	public float slamCooldown;
	
	public bool dontStopSpin = false;
	public bool Ragdoll = false;
	public float rotationMultiplier = 1;
	public int recoveryTime = 2;
	Quaternion currentRotation;
	public Quaternion previousRotation;
	private XboxControls controllerInput;
	private float cooldown;
	private float uppercutCD = 0;
	private float slamCD = 0;
	private hitBox Box;
	private slam Slam;
	public  gameController manager;
	public Vector3 angularVelocity;
	void Start () 
	{
		manager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController>();
		controllerInput = GetComponent<XboxControls> ();
		Box = GetComponentInChildren<hitBox>();
		Slam = GetComponentInChildren<slam> ();
		manager.players.Add (this);
	}

	void Update () 
	{
		angularVelocity = rigidbody.angularVelocity;
		if (Mathf.Abs(rotationMultiplier) < 0.01f) 
		{
			rotationMultiplier = 0;
		}
		if(!stunned)
		{
			if (isAlive)
			{

				if (Ragdoll)
				{
					rigidbody.constraints = RigidbodyConstraints.None;
					recover();
				}
				else
				{
					Movement ();
				}
			}
		}

		if (health <= 0 && isAlive) 
		{
			isAlive = false;
			Ragdoll = true;
			Transform hammer = transform.FindChild("Hammer");
			hammer.gameObject.AddComponent<Rigidbody>();
			hammer.GetComponent<CapsuleCollider>().enabled = true;
			hammer.FindChild("HammerHead").GetComponent<CapsuleCollider>().isTrigger = false;
			hammer.transform.parent = null;
			Debug.Log("dead");
		}

		if(uppercutCD >0)
		{
			uppercutCD -= Time.deltaTime;
		}
		if(slamCD >0)
		{
			slamCD -= Time.deltaTime;
		}

		if(GetComponent<Animation>().IsPlaying("upperCut"))
		{
			if(animation["upperCut"].time > 0.30f && animation["upperCut"].time< 0.50f)
			{
				if(uppercutCD <= 0)
				{
					foreach(GameObject target in Box.targets)
					{
						punt(target);
						target.GetComponent<playerCharacter>().rotationMultiplier = 0;
						target.GetComponent<playerCharacter>().health -= uppercutDamage;
					}
					uppercutCD = uppercutCooldown;
				}
			}
		}
		if(GetComponent<Animation>().IsPlaying("slam"))
		{
			if(animation["slam"].time > 0.5f && !GetComponent<RigidBodyControls>().grounded)
			{
				animation.enabled = false;
			}
			Slam.isEnabled = true;
			if(GetComponent<RigidBodyControls>().grounded)
			{
				Slam.isEnabled = false;
				slamCD = slamCooldown;
				animation.enabled = true;
			}
		}
		
	}
	 
	private float sensitivityX=90;
	private float sensitivityY=90;
	float aim_angle = 0.0f;

	void Movement()
	{ 
		float lThumbX = Input.GetAxis(controllerInput.buttons.movementHorizontalAxis)*sensitivityX;
		float lThumbY = -Input.GetAxis(controllerInput.buttons.movementVerticalAxis)*sensitivityY;
		
		// CLAMP THE SPIN SPEED
		// UPPER BOUND OF SPIN SPEED
		if(rotationMultiplier <= 80)
		{
				// IF RIGHT TRIGGER IS DOWN
			if (Input.GetAxis (controllerInput.buttons.rTrigger) >= 1 && GetComponent<RigidBodyControls>().grounded) 
			{ 
				rotationMultiplier+= 0.2f;
				transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime * rotationMultiplier);
			}
			else
			{
				//DECREASE ROTATION OVER TIME IF NOT SPINNING
				if(!dontStopSpin)
				{
					rotationMultiplier -= 0.0001f + (rotationMultiplier / 20);
					transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime * rotationMultiplier);
				}
				if(Mathf.Abs(rotationMultiplier) <= 1)
				{
					if(lThumbX != 0 || lThumbY != 0)
					{
						if(!GetComponent<Animation>().isPlaying)
						{
							GetComponent<Animation>().Play("walk");
						}
						aim_angle = Mathf.Atan2(lThumbY, lThumbX) * Mathf.Rad2Deg;
						transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.AngleAxis (aim_angle, Vector3.up), .2f);
					}
				}
			}
		}
		else
		{
			rotationMultiplier =80;
		}

		if(Input.GetButtonUp(controllerInput.buttons.rBumper))
		{
			//attack
			if(GetComponent<RigidBodyControls>().grounded)
			{
				if(uppercutCD <= 0)
				{
					GetComponent<Animation>().Play("upperCut");
					//uppercut
				}
			}
			else
			{
				if(slamCD <= 0)
				{
					rigidbody.velocity = Vector3.zero;
					rigidbody.AddForce(Vector3.down*slamSpeed);
					GetComponent<Animation>().Play("slam");
				}
			}
		}
	}

	public void punt(GameObject target)
	{
		target.transform.position += new Vector3(0,0.05f,0);
		Vector3 direction = (target.transform.position - transform.position);
		direction.y += direction.sqrMagnitude;
		direction.x *= 0.5f;
		direction.z *= 0.5f;
		direction.Normalize();
		target.rigidbody.AddForce(direction*uppercutForce);
	}

	public void recover()
	{
		//rigidbody.freezeRotation = false;
		//yield return new WaitForSeconds (5);
		if (cooldown > recoveryTime) 
		{
			currentRotation = transform.rotation;
			transform.rotation = Quaternion.Lerp (currentRotation, previousRotation, recoveryTime);
			rotationMultiplier = 0;
			Ragdoll = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			rigidbody.angularVelocity = Vector3.zero;
			cooldown = 0;
		} 
		else 
		{
			cooldown += Time.deltaTime;
		}
	}

//	void OnCollisionStay () {
//		currentRotation = rigidbody.rotation;
//		Ragdoll = false;
//		Quaternion.Slerp(currentRotation,DefaultRotation,0.5f);
//	}


}


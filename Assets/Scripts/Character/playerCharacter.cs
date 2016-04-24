using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class playerCharacter : MonoBehaviour {

	public GameObject slamParticles;
	public GameObject stunParticles;

	private fastHammerPhysics spinning;
	private RigidBodyControls rigidBody;
	private float gravityValue;

	private Rigidbody RB;
	public Animator animator;
	public float health = 200;
	private float healthMax;
	public bool isAlive = true;
	public int lives = 1;
	public float damageMultiplier = 1.0f;
	public int ID = 0;
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
	private bool uppercut = false;
	private float uppercutDelay = 0.3f;
	private float uDelay = 0;

	private float slamCD = 0;
	private bool slam = false;
	private float slamDelay = 0.3f;
	private float sDelay = 0;

	private hitBox Box;
	public slam Slam;
	public  gameController manager;
	public Vector3 angularVelocity;

	private float idleTimer = 3;
	public float idleTime = 0;
	void Start () 
	{
		manager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<gameController>();
		controllerInput = GetComponent<XboxControls> ();
		Box = GetComponentInChildren<hitBox>();
		Slam = GetComponentInChildren<slam> ();
		manager.players.Add (this);
		animator = GetComponentInChildren<Animator> ();
		spinning = GetComponentInChildren<fastHammerPhysics> ();
		rigidBody = GetComponent<RigidBodyControls> ();
		gravityValue = rigidBody.gravity;
		if (manager.settings != null) 
		{
			lives = manager.settings.lives;
		}
		healthMax = health;
		stunParticles = transform.FindChild ("stun").gameObject;
		manager.alivePlayers.Add (this);
	}

	void Update () 
	{
		if (health > healthMax) 
		{
			health = healthMax;
		}
		if (rotationMultiplier > 10) {
			spinning.isEnabled = true;
			animator.SetBool("Spinning",true);
		} 
		else 
		{
			spinning.isEnabled = false;
			animator.SetBool("Spinning",false);
		}

		if (idleTime > idleTimer) 
		{
			animator.SetBool ("longIdleTrigger", true);
			idleTime=0;
		} 
		else 
		{
			animator.SetBool ("longIdleTrigger", false);
		}
		idleTime += Time.deltaTime;

		angularVelocity = rigidbody.angularVelocity;
		if (Mathf.Abs(rotationMultiplier) < 0.01f) 
		{
			rotationMultiplier = 0;
		}
		if (Ragdoll) 
		{
			rigidbody.constraints = RigidbodyConstraints.None;
			stunParticles.SetActive(true);
		}
		if (isAlive)
		{
			if (Ragdoll)
			{
				recover();
			}
			else
			{
				Movement ();
			}
		}

		if (health <= 0 && isAlive) 
		{
			lives--;
			if(lives > 0)
			{
				GetComponent<PlayerBuffs>().clear();
				int randomSpawn = Random.Range(0,3);
				manager.spawnPoints[randomSpawn].respawn(this);
			}
			else
			{
				isAlive = false;
				Ragdoll = true;
				animator.SetBool("Dead",true);
				rigidbody.AddTorque(new Vector3(0,0,100));
				manager.alivePlayers.Remove(this);
			}
		}
		 
		if(uppercutCD >0)
		{
			uppercutCD -= Time.deltaTime;
		}
		if(slamCD >0)
		{
			slamCD -= Time.deltaTime;
		}

		if(uppercut)
		{
			if(uppercutCD <= 0)
			{
				if(uDelay >= uppercutDelay)
				{
					foreach(GameObject target in Box.targets)
					{
						punt(target);
						target.GetComponent<playerCharacter>().rotationMultiplier = 0;
						target.GetComponent<playerCharacter>().health -= uppercutDamage* damageMultiplier;
					}
					uppercutCD = uppercutCooldown;
					uppercut = false;
				}
				else
				{
					uDelay += Time.deltaTime;
				}
			}
		}

		if (slam) {
			if(sDelay >= slamDelay)
			{
				Slam.isEnabled = true;
				rigidBody.gravity = gravityValue;
				if (GetComponent<RigidBodyControls> ().grounded) {
					Slam.clear();
					Slam.isEnabled = false;
					slamCD = slamCooldown;
					slam = false;
					animator.SetBool("slamTrigger",false);
					Instantiate(slamParticles,transform.position+new Vector3(0,0.3f,0),Quaternion.Euler(90,0,0));
				}
			}
			else
			{
				sDelay += Time.deltaTime;
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
		if(rotationMultiplier <= 30)
		{
			if(rotationMultiplier >= 0.01f)
			{
				idleTime = 0;
			}
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
						aim_angle = Mathf.Atan2(lThumbY, lThumbX) * Mathf.Rad2Deg;
						transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.AngleAxis (aim_angle, Vector3.up), .2f);
					}
				}
			}
		}
		else
		{
			rotationMultiplier =30;
		}

		if(Input.GetButtonUp(controllerInput.buttons.rBumper))
		{
			idleTime = 0;
			//attack
			if(GetComponent<RigidBodyControls>().grounded)
			{
				if(uppercutCD <= 0)
				{
					animator.SetTrigger("uppercutTrigger");
					uppercut = true;
					uDelay = 0;
					//uppercut
				}
			}
			else
			{
				if(slamCD <= 0)
				{
					rigidbody.velocity = Vector3.zero;
					animator.SetBool("slamTrigger",true);
					slam = true;
					sDelay = 0;
					rigidBody.gravity=0;
				}
			}
		}
	}

	public void punt(GameObject target)
	{
		target.transform.position += new Vector3(0,0.05f,0);
		Vector3 direction = (target.transform.position - transform.position);
		direction.y += direction.magnitude/2;
//		direction.x *= 0.5f;
//		direction.z *= 0.5f;
		direction.Normalize();
		target.rigidbody.AddForce(direction*uppercutForce);
		target.rigidbody.AddTorque (direction * uppercutForce);
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
			stunParticles.SetActive(false);
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


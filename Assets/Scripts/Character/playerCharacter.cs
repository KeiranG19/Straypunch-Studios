using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class playerCharacter : MonoBehaviour {
	private Rigidbody RB;

	public float health = 200;
	public bool isAlive = true;

	public float uppercutDamage;
	public float uppercutForce;
	public float uppercutCooldown;

	//IDEA!!!  slam gameobject which handles itself 
	public float slamDamage;
	public float slamRadius;
	public bool dontStopSpin = false;
	public bool Ragdoll = false;
	public float rotationMultiplier = 1;
	public int recoveryTime = 2;
	Quaternion currentRotation;
	public Quaternion previousRotation;
	private XboxControls controllerInput;
	private float cooldown;
	public float uppercutCD = 0;
	private hitBox Box;
	
	void Start () 
	{
		controllerInput = GetComponent<XboxControls> ();
		Box = GetComponentInChildren<hitBox>();
	}

	void Update () 
	{
		if (isAlive)
		{

			if (Ragdoll)
			{
				rigidbody.constraints = RigidbodyConstraints.None;
				recover();
			}
			else{
				Movement ();
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
	}

	private bool started_spinning = false;
	private float sensitivityX=90;
	private float sensitivityY=90;
	float aim_angle = 0.0f;

	void Movement()
	{ 
		float rThumbX = Input.GetAxis(controllerInput.buttons.rotationHorizontalAxis)*sensitivityX;
		float rThumbY = Input.GetAxis(controllerInput.buttons.rotationVerticalAxis)*sensitivityY;

		float lThumbX = Input.GetAxis(controllerInput.buttons.movementHorizontalAxis)*sensitivityX;
		float lThumbY = -Input.GetAxis(controllerInput.buttons.movementVerticalAxis)*sensitivityY;
		
		// CLAMP THE SPIN SPEED
		// UPPER BOUND OF SPIN SPEED
		if(rotationMultiplier <= 80)
		{
				// IF RIGHT TRIGGER IS DOWN
			if (Input.GetAxis (controllerInput.buttons.rTrigger) >= 1) 
				{ 
					// HOLDING, BUT NOT SPINING THE THUMBSTICK, INCREASE SLOWLY
				if(rThumbX != 0 && rThumbY == 0)
					{
						rotationMultiplier+= 0.2f;
						started_spinning = true;
					transform.Rotate(new Vector3(0, rThumbX, 0) * Time.deltaTime * rotationMultiplier);
					}
					else
					{
						//	SPINNING THE THUMBSTICK, INCREASE FASTER
					if (rThumbX != 0.0f || rThumbY != 0.0f) 
						{
							started_spinning = true;
						aim_angle = Mathf.Atan2(rThumbY, rThumbX) * Mathf.Rad2Deg;

							// CALCULATE ANGLE AND ROTATE
							transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(aim_angle, Vector3.up),.2f) ;

							if(started_spinning == true)
							{
								rotationMultiplier += 0.0000001f + (rotationMultiplier / 40);
								transform.Rotate(new Vector3(0,(transform.eulerAngles.y/90),0)* rotationMultiplier);
							}
						}
						else 
						{
							started_spinning = false;
						}
					}
				}
				else
				{
					started_spinning = false;
				}
				if(started_spinning == false)
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
			rotationMultiplier =80;
		}

		if(Input.GetButton(controllerInput.buttons.rBumper))
		{

			//attack
			if(GetComponent<RigidBodyControls>().grounded)
			{
				if(uppercutCD <= 0)
				{
					//uppercut
					foreach(GameObject target in Box.targets)
					{
						punt(target);
						target.GetComponent<playerCharacter>().rotationMultiplier = 0;
					}
					uppercutCD = uppercutCooldown;
				}
			}
			else
			{
				//slam
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
		//direction.Normalize();
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
			Ragdoll = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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


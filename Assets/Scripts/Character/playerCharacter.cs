using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class playerCharacter : MonoBehaviour {
		

	private Rigidbody RB;

	public float health = 200;
	public bool isAlive = true;

	[Header("Controller variables")]

	[Header("Movement variables")]

	[Tooltip("Character walking speed")]
	public float walkingSpeed = 10.0f;
	
	[Tooltip("Character running speed")]
	public float runningSpeed = 20.0f;
	
	[Tooltip("How quickly the character can jump")]
	public float jumpSpeed = 20f;
	
	[Tooltip("How quickly the character can turn")]
	public float rotationSpeed = 4f;

	[Tooltip("How much gravity to apply to the character")]
	public float gravityStrength = 20.0f;

	[Tooltip("How many extra jumps a character can do (0 is a single jump, 1 is a double jump)")]
	public int additionalJumps = 1; 

	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	public bool Ragdoll = false;
	public float currentSpeed = 10.0f;
	private int jumpsRemaining;
	public float rotationMultiplier = 1;
	public Vector3 addedVel = new Vector3 (0,0,0);
	public Vector3 velocityChange;
	Quaternion currentRotation;
	Quaternion DefaultRotation;
	private XboxControls controllerInput;

	void Start () 
	{
		controllerInput = GetComponent<XboxControls> ();
		DefaultRotation = rigidbody.rotation;
	}
	

	void Update () 
	{
		if (isAlive)
		{
			Movement ();
		}

		if (health <= 0 && isAlive) 
		{
			isAlive = false;
			rigidbody.freezeRotation = false;
			Transform hammer = transform.FindChild("Hammer");
			hammer.gameObject.AddComponent<Rigidbody>();
			hammer.GetComponent<CapsuleCollider>().enabled = true;
			hammer.FindChild("HammerHead").GetComponent<CapsuleCollider>().isTrigger = false;
			hammer.transform.parent = null;
			Debug.Log("dead");
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
					rotationMultiplier -= 0.0001f + (rotationMultiplier / 20);
					transform.Rotate(new Vector3(0, -90, 0) * Time.deltaTime * rotationMultiplier);
					
					if(Mathf.Abs(rotationMultiplier) <= 1)
					{
					aim_angle = Mathf.Atan2(lThumbY, lThumbX) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.AngleAxis (aim_angle, Vector3.up), .2f);
					}
				}
		}
		else
		{
			rotationMultiplier =80;
		}
	}


	void OnCollisionStay () {
		currentRotation = rigidbody.rotation;
		Ragdoll = false;
		Quaternion.Slerp(currentRotation,DefaultRotation,0.5f);
	}

	void OnControllerColliderHit (ControllerColliderHit hit) 
	{

		if (hit.rigidbody != null && !hit.collider.attachedRigidbody.isKinematic) 
		{
			RB = hit.collider.attachedRigidbody;

			Vector3 push = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);  //Make a vector of velocity to push the rigidbody

			RB.AddForceAtPosition(push, hit.point);
		}


		if (hit.gameObject.tag == "Player") 
		{
			playerCharacter	enemyPC = hit.gameObject.GetComponent<playerCharacter>();
			float multiplier =  rotationMultiplier;
			Vector3 push = new Vector3 (hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z)*multiplier; 
			enemyPC.addedVel = push;
		}
	} 
}


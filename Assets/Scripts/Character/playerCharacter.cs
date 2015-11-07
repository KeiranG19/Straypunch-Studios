using UnityEngine;
using System.Collections;

public class playerCharacter : MonoBehaviour {

	
	////////////////////////////////////////////////////////////////////	
	/// Input Structure
	///////////////////////////////////////////////////////////////////

	struct inputButtons 
	{
		/* Left Stick */
		public	string movementHorizontalAxis;
		public string movementVerticalAxis;

		/* Right Stick */
		public string rotationHorizontalAxis;
		public string rotationVerticalAxis;

		/* Face buttons */
		public string jumpButton;       //A
		public string sprintButton;    //B
		public string attackButton;   //X
		public string altButton;	 //Y

		/* Bumpers */
		public string lBumper;
		public string rBumper;

		/* Back/Start */
		public string backButton;
		public string startButton;

		/* Stick clicks */
		public string lStickClick;
		public string rStickClick;

		/*Triggers*/
		public string lTrigger;
		public string rTrigger;




	};
	private inputButtons currentButtons;

	////////////////////////////////////////////////////////////////////	
	/// Component References
	///////////////////////////////////////////////////////////////////

	private CharacterController CC;
	private Rigidbody RB;


	////////////////////////////////////////////////////////////////////	
	/// Public Inspector Variables
	///////////////////////////////////////////////////////////////////
	[Header("Controller variables")]
	
[Tooltip("Current Controller Use ID  (0-3) ")]
	public int controllerInUse = 0;

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
	
	////////////////////////////////////////////////////////////////////	
	/// Private Variables
	////////////////////////////////////////////////////////////////////

	public float currentSpeed = 10.0f;
	private Vector3 moveDir = Vector3.zero;
	private float vSpeed = 0;
	private Vector3 groundNormal;
	private Vector3 OriginalPosition;
	private int jumpsRemaining;
	public float rotationMultiplier = 6;
	public Vector3 addedVel = new Vector3 (0,0,0);

	void Start () 
	{

		SetupButtons();
		OriginalPosition = transform.position;
		if(CC == null)
		{
			CC = GetComponent<CharacterController> ();
		}

	
	}
	void SetupButtons()
	{
		currentButtons.movementHorizontalAxis = "P"+ controllerInUse.ToString() + "Horizontal";
		currentButtons.movementVerticalAxis =   "P"+ controllerInUse.ToString() + "Vertical";
		currentButtons.rotationHorizontalAxis = "P"+ controllerInUse.ToString() + "RotX";
		currentButtons.rotationVerticalAxis =   "P"+ controllerInUse.ToString() + "RotY"; 

		currentButtons.sprintButton = "P"+ controllerInUse.ToString() + "Sprint";
		currentButtons.jumpButton =   "P"+ controllerInUse.ToString() + "Jump";
		currentButtons.attackButton = "P" + controllerInUse.ToString() + "Attack";
		currentButtons.altButton = "P" + controllerInUse.ToString() + "Alt";

		currentButtons.backButton =  "P" + controllerInUse.ToString() + "Back";
		currentButtons.startButton = "P" + controllerInUse.ToString() + "Start";

		currentButtons.lBumper = "P" + controllerInUse.ToString() + "Lbumper";
		currentButtons.rBumper = "P" + controllerInUse.ToString() + "Rbumper";
	
		currentButtons.lStickClick = "P" + controllerInUse.ToString() + "LstickClick";
		currentButtons.rStickClick = "P" + controllerInUse.ToString() + "RstickClick";

		currentButtons.lTrigger = "P" + controllerInUse.ToString() + "Ltrigger";
		currentButtons.rTrigger = "P" + controllerInUse.ToString() + "Rtrigger";



	}

	void Update () 
	{
		Movement();
	}

	void Movement()
	{
		Vector3 vec = new Vector3 (Input.GetAxis (currentButtons.movementHorizontalAxis), 0f, Input.GetAxis (currentButtons.movementVerticalAxis));


		if (Input.GetAxis (currentButtons.rTrigger) >= 1) 
		{

			if(Input.GetAxis(currentButtons.rotationHorizontalAxis) != 0)
			{
				rotationMultiplier+= 0.4f;
			}
			else
			{
				rotationMultiplier  = 6;
			}

			transform.Rotate(new Vector3(0, Input.GetAxis(currentButtons.rotationHorizontalAxis), 0) * Time.deltaTime * rotationSpeed * rotationMultiplier);
		} 
		else 
		{
			rotationMultiplier  = 6;

			transform.Rotate(new Vector3(0, Input.GetAxis(currentButtons.rotationHorizontalAxis), 0) * Time.deltaTime * rotationSpeed * rotationMultiplier);
		}


		if (Input.GetButtonDown (currentButtons.sprintButton)) {
			currentSpeed = runningSpeed;
		}
		if (Input.GetButtonUp (currentButtons.sprintButton)) {
			currentSpeed = walkingSpeed;
		}

		moveDir = new Vector3 (-Input.GetAxis (currentButtons.movementHorizontalAxis), 0, -Input.GetAxis (currentButtons.movementVerticalAxis));
		//moveDir = transform.TransformDirection(moveDir);  //This line makes you move in the direction you face
		moveDir *= currentSpeed;
		moveDir += addedVel; 
		addedVel = addedVel / 2;

		//Debug.Log (jumpsRemaining.ToString());

		float jumpSleep = 0;
		if (CC.isGrounded) 
		{
			vSpeed = 0; 
			jumpsRemaining = additionalJumps;
			
			if (Input.GetButton (currentButtons.jumpButton) && !TooSteep ()) 
			{
				vSpeed = jumpSpeed;
			}
			if (TooSteep ()) {
				//moveDir.y = -jumpSpeed / 2;
			}
			
		}
		else 
		{

			if (Input.GetButtonDown(currentButtons.jumpButton)) 
			{
			

				if(jumpsRemaining > 0)
				{
					vSpeed = jumpSpeed;
					jumpsRemaining--;
				}
				
				
			}
			if (TooSteep ()) {
				//moveDir.y = -jumpSpeed / 2;
			}
			
		}

		vSpeed -= gravityStrength * Time.deltaTime;
		moveDir.y = vSpeed;
		CC.Move (moveDir * Time.deltaTime);


	}
	private bool TooSteep () 
	{
		return (groundNormal.y <= Mathf.Cos (CC.slopeLimit * Mathf.Deg2Rad));
		//gets the slope and returns boolean true or false for if it should be traverseable
	}
	void OnControllerColliderHit (ControllerColliderHit hit) 
	{
		
		groundNormal = hit.normal;
		
		if (hit.rigidbody != null && !hit.collider.attachedRigidbody.isKinematic) 
		{
			RB = hit.collider.attachedRigidbody;
			//Grab the rigidbody of what we've collided with, if it exists and isn't kinematic, continue
			
			//if (hit.moveDirection.y < -0.3) { return; } //don't affect objects under player
			
			Vector3 push = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);  //Make a vector of velocity to push the rigidbody
			//RB.velocity = RB.velocity + push * 1f; //Apply the force
			RB.AddForceAtPosition(push, hit.point);


		}


		if (hit.gameObject.tag == "Player") 
		{
			playerCharacter	enemyPC = hit.gameObject.GetComponent<playerCharacter>();
			float multiplier =  rotationMultiplier;
			Vector3 push = new Vector3 (hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z)*multiplier; 
			enemyPC.addedVel = push;
			
			//enemyCC.Move(push);
		}



	} 

}


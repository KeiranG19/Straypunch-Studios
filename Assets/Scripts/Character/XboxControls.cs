using UnityEngine;
using System.Collections;

public class XboxControls : MonoBehaviour 
{

	public struct inputButtons 
	{
		/* Left Stick */
		public string movementHorizontalAxis;
		public string movementVerticalAxis;
		
		/* Right Stick */
		public string rotationHorizontalAxis;
		public string rotationVerticalAxis;
		
		/* Face buttons */
		public string A; 
		public string B;
		public string X;
		public string Y;
		
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

	[Tooltip("Controller Use ID  (0-3) ")]
	public int controllerInUse = 0;

	public inputButtons buttons;

	void Awake(){
		buttons.movementHorizontalAxis = "P"+ controllerInUse.ToString() + "Horizontal";
		buttons.movementVerticalAxis =   "P"+ controllerInUse.ToString() + "Vertical";
		buttons.rotationHorizontalAxis = "P"+ controllerInUse.ToString() + "RotX";
		buttons.rotationVerticalAxis =   "P"+ controllerInUse.ToString() + "RotY"; 
		
		buttons.A = "P"+ controllerInUse.ToString() + "Jump";
		buttons.B = "P"+ controllerInUse.ToString() + "Sprint";
		buttons.X = "P" + controllerInUse.ToString() + "Attack";
		buttons.Y = "P" + controllerInUse.ToString() + "Alt";
		
		buttons.backButton =  "P" + controllerInUse.ToString() + "Back";
		buttons.startButton = "P" + controllerInUse.ToString() + "Start";
		
		buttons.lBumper = "P" + controllerInUse.ToString() + "Lbumper";
		buttons.rBumper = "P" + controllerInUse.ToString() + "Rbumper";
		
		buttons.lStickClick = "P" + controllerInUse.ToString() + "LstickClick";
		buttons.rStickClick = "P" + controllerInUse.ToString() + "RstickClick";
		
		buttons.lTrigger = "P" + controllerInUse.ToString() + "Ltrigger";
		buttons.rTrigger = "P" + controllerInUse.ToString() + "Rtrigger";
	}
}

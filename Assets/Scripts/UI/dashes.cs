using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Attach it to player and set player number to be 1-4

public class dashes : MonoBehaviour 
{

	public int playerNumber;				// which player this will track e.g. 1 = " player 1"

	private Text dash;						// the text field which will output the remaining dashes
	private RigidBodyControls player;		//Referance to the player

	void Start()
	{
		player = GetComponent<RigidBodyControls>();
		playerNumber = GetComponent<playerCharacter> ().ID;
		// Not the best way of doing this, but couldn't think of a clean, fast, robust way, that also accounted for 
		// possible null refs if it tried to find the player before they spawned in
		if(playerNumber == 1)
		{
			dash = GameObject.Find("Txt_p1_Dashes").GetComponent<Text>();
			dash.GetComponent<Text>().enabled = true;
		}
		else if(playerNumber == 2)
		{
			dash = GameObject.Find("Txt_p2_Dashes").GetComponent<Text>();
			dash.GetComponent<Text>().enabled = true;
		}
		else if(playerNumber == 3)
		{
			dash = GameObject.Find("Txt_p3_Dashes").GetComponent<Text>();
			dash.GetComponent<Text>().enabled = true;
		}
		else
		{
			dash = GameObject.Find("Txt_p4_Dashes").GetComponent<Text>();
			dash.GetComponent<Text>().enabled = true;
		}
	}
	
	void Update () 
	{

		dash.text = player.remainingDashes.ToString ();

	}
}

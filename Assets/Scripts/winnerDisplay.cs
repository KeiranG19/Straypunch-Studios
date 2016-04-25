using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class winnerDisplay : MonoBehaviour 
{
	public int winnerID;
	public GameObject winner;
	public GameObject looser1;
	public GameObject looser2;
	public GameObject looser3;
	public GameObject looser3OtherHalf;
	public Text playerWonText;
	public Material red;
	public Material blue;
	public Material yellow;
	public Material green;

	private MatchOptions levelSettings;
	void Start () 
	{
		GameObject tempSettings = GameObject.Find ("level_settings");
		if (tempSettings != null) 
		{
			levelSettings = tempSettings.GetComponent<MatchOptions>();
			winnerID = levelSettings.winningPlayer;

			if (winnerID == 1) 
			{
				winner.renderer.material = red;
				looser1.renderer.material = blue;
				looser2.renderer.material = yellow;
				looser3.renderer.material = green;
				looser3OtherHalf.renderer.material = green;

			} 
			else if (winnerID == 2) 
			{
				winner.renderer.material = blue;
				looser1.renderer.material = red;
				looser2.renderer.material = yellow;
				looser3.renderer.material = green;
				looser3OtherHalf.renderer.material = green;
			} 
			else if (winnerID == 3) 
			{
				winner.renderer.material = green;
				looser1.renderer.material = blue;
				looser2.renderer.material = red;
				looser3.renderer.material = yellow;
				looser3OtherHalf.renderer.material = yellow;
			} 
			else 
			{
				winner.renderer.material = yellow;
				looser1.renderer.material = blue;
				looser2.renderer.material = green;
				looser3.renderer.material = red;
				looser3OtherHalf.renderer.material = red;
			}

			playerWonText.text = "Player "+winnerID.ToString()+" \n     Victory";
			playerWonText.color = winner.renderer.material.color;
		}
	}
	
}

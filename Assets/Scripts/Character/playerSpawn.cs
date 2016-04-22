using UnityEngine;
using System.Collections;

public class playerSpawn : MonoBehaviour 
{
	public GameObject player1;			// player 1 prefab, drag and drop in inspector
	public GameObject player2;			// player 2 prefab, drag and drop in inspector
	public GameObject player3;			// player 3 prefab, drag and drop in inspector
	public GameObject player4;			// player 4 prefab, drag and drop in inspector
	public int playerStartSpawn;		// This number sets which player will spawn here at the start of the round

	private gameController manager; 	// Referance to the game controller

	void Awake()
	{
		manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
		
		manager.spawnPoints.Add(this);
	}

	public void respawn(playerCharacter player)
	{
		player.health = 100;
		player.transform.position = transform.position;
	}

	public void spawnPlayer(int player)
	{
		if(player == 1)
		{
			GameObject tempPlayer = GameObject.Find("Player 1");
			if(tempPlayer == null)
			{
				Instantiate(player1,transform.position, transform.rotation);

			}
			else
			{
				Debug.Log("player 1 already exists");
			}
		}
		else if(player == 2)
		{
			GameObject tempPlayer = GameObject.Find("Player 2");
			if(tempPlayer == null)
			{
				Instantiate(player2,transform.position, transform.rotation);
			}
			else
			{
				Debug.Log("player 2 already exists");
			}
		}
		else if(player == 3)
		{
			GameObject tempPlayer = GameObject.Find("Player 3");
			if(tempPlayer == null)
			{
				Instantiate(player3,transform.position, transform.rotation);
			}
			else
			{
				Debug.Log("player 3 already exists");
			}
		}
		else
		{
			GameObject tempPlayer = GameObject.Find("Player 4");
			if(tempPlayer == null)
			{
				Instantiate(player4,transform.position, transform.rotation);
			}
			else
			{
				Debug.Log("player 4 already exists");
			}
		}
		
	}
}

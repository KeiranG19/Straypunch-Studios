using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float cameraRotationSpeed=1;
	public float cameraDist=1;
	public float mapWidth = 100;

	public float verticalZoomMultiplier=1;
	public float ignoreDist = 10;

	private Vector3 midpoint;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		setLookat ();
	}


	void setLookat() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		float maxZ = 0.0f;
		float minZ = 0.0f;
		foreach (GameObject player in players) {
			if (player.GetComponent<playerCharacter>().isAlive)
			{
				if(player.transform.position.z > maxZ)
				{
					maxZ = player.transform.position.z;
				}
				else if(player.transform.position.z < minZ)
				{
					minZ = player.transform.position.z;
				}
			}
		}
		float zPosition = (maxZ + minZ)/2;
		float maxDist = Mathf.Abs (Mathf.Abs (maxZ) - Mathf.Abs (minZ));

		 

		
		//transform.position = Vector3.Lerp (transform.position, Position, Time.deltaTime * cameraZoomSpeed);
	}
} 
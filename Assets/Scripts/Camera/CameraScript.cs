using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float cameraRotationSpeed=1;
	public float cameraZoomSpeed=1;
	public float cameraZoomDist=1;
	public float mapWidth = 100;
	public float verticalZoomMultiplier=1;
	private Vector3 midpoint;
	private float closestX = 0;
	private Vector3 startposition;
	// Use this for initialization
	void Start () {
		startposition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		setLookat ();
	}


	void setLookat() {
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		int numPlayers = 0;
		midpoint = new Vector3 (0, 0, 0);
		foreach (GameObject player in players) {
			//if (player.isAlive) {
			numPlayers++;
			midpoint += player.transform.position;
			if(closestX > player.transform.position.x)
			{
				closestX = player.transform.position.x;
			}
			//}
		}

		midpoint = midpoint / numPlayers;

		Vector3 Position = transform.position;
		Position.x = closestX + cameraZoomDist;
		float heightModifier = Mathf.Abs (closestX / mapWidth)*verticalZoomMultiplier;
		Position.y = startposition.y + heightModifier;
		transform.position = Vector3.Lerp (transform.position, Position, Time.deltaTime * cameraZoomSpeed);

		Vector3 Direction = midpoint-transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Direction), Time.deltaTime * cameraRotationSpeed);

	}
} 
using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float cameraRotationSpeed;
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
		int numPlayers = 0;
		midpoint = new Vector3 (0, 0, 0);
		foreach (GameObject player in players) {
			//if (player.isAlive) {
			numPlayers++;
			midpoint += player.transform.position;
			//}
		}

		midpoint = midpoint / numPlayers;

		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (midpoint-transform.position), Time.deltaTime * cameraRotationSpeed);
	}
}
using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float cameraRotationSpeed;
	private Vector3 midpoint;
	private float prevMaxDist = 0;
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
		float maxDist = 0;
		foreach (GameObject player in players) {
			float dist = Vector3.Distance(midpoint,player.transform.position);
			if(dist>maxDist)
			{
				maxDist = dist;
			}
		}
		Vector3 Direction = midpoint-transform.position;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Direction), Time.deltaTime * cameraRotationSpeed);
		transform.position = Vector3.Lerp(transform.position,(transform.forward * (prevMaxDist - maxDist))+transform.position,Time.deltaTime);
		prevMaxDist = maxDist;
	}
}
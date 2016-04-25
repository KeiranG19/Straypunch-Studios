using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float maxDist = 100;
	public float zoomSpeed = 5;
	private float zoomAmount = 0;
	private Vector3 startPosition;
	private Vector3 forwardPosition;
	public gameController manager;
	// Use this for initialization
	void Awake () {

		startPosition = transform.position;
		forwardPosition = transform.position + (transform.forward * 10);
	}
	
	// Update is called once per frame
	void Update () {
		setLookat ();
	}


	void setLookat() {
		float maxZ = float.NegativeInfinity;
		float minZ = float.PositiveInfinity;
		float minX = float.PositiveInfinity;
		foreach (playerCharacter player in manager.alivePlayers) {
			if(player.transform.position.z > maxZ)
			{
				maxZ = player.transform.position.z;
			}
			if(player.transform.position.z < minZ)
			{
				minZ = player.transform.position.z;
			}
			if(player.transform.position.x<minX)
			{
				minX = player.transform.position.x;
			}
		}

		float Dist = Mathf.Abs (maxZ - minZ);
		float zPosition = (maxZ + minZ)/2;

		zoomAmount = 1 - (Dist*Dist / (maxDist * 2));

		startPosition.z = zPosition;

		Vector3 endPosition = forwardPosition;

		endPosition.z = zPosition;
		//endPosition.x += minX + 10;
		Vector3 targetPosition = Vector3.Lerp (startPosition, endPosition, zoomAmount);

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * zoomSpeed);
	}
} 
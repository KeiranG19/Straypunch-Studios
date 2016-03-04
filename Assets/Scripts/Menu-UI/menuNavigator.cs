using UnityEngine;
using System.Collections;

public class menuNavigator : MonoBehaviour {

	XboxControls controller;
	private MatchOptions settings;		// Referance to level options which get passed to next scene

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<XboxControls>();
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<MatchOptions>();
		controller.controllerInUse = settings.playerCount - 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

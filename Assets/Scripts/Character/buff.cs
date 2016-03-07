using UnityEngine;
using System.Collections;

public class buff : MonoBehaviour {

	public float lifeTime = 1;
	public enum type{
		HOT,		 // heal over time
		Speed		 // speed buff
		};		 
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void giveBuff( playerCharacter player, float time, type buffType) 
	{
		// buffsManager.debuffs.Add(this);
	}
	
	public void applyEffect(playerCharacter player)
	{
		
	}
}

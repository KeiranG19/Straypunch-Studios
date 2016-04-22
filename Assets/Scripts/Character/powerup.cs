using UnityEngine;
using System.Collections;

public class powerup : MonoBehaviour 
{

	public enum type{
		heal, 
		speed,
		HoT
	};

	public type Type;
	public int healAmount;
	public int HoTamount;
	public int speedAmount;

	public float buffTime = 3;
	public buff effect;
	
	void OnTriggerEnter(Collider other)
	{

			if(other.gameObject.tag == "Player")
			{
				playerCharacter myPlayer = other.GetComponent<playerCharacter>();
				RigidBodyControls myRigidBody = other.GetComponent<RigidBodyControls>();
				
				if(Type == type.heal)
				{
					myPlayer.health += healAmount;
					Destroy(this.gameObject);
				}
				if(Type == type.speed)
				{
					effect.speedIncrease = speedAmount;
					effect.giveBuff(myPlayer,buffTime,buff.type.Speed);
					Destroy(this.gameObject);
				}
				if(Type == type.HoT)
				{
					effect.HoTamount = HoTamount;
					effect.giveBuff(myPlayer,buffTime,buff.type.HOT);
					Destroy(this.gameObject);
				}
			}

	}
	
	//	void OnTriggerExit(Collider other)
	//	{
	//		if(activated == true)
	//		{
	//			if(other.gameObject.tag == "Player")
	//			{
	//				RigidBodyControls myRigidBody = other.GetComponent<RigidBodyControls>();
	//				if(Type == type.area_slow)
	//				{
	//					myRigidBody.speed = 10;
	//				}
	//			}
	//		}
	//
	//	}
	
	
}

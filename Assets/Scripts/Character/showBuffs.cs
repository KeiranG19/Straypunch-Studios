using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class showBuffs : MonoBehaviour 
{
//	public int playerNumber;
//	public GameObject HoT;
//	public GameObject DoT;
//
//	private GameObject slots;
//	private playerCharacter player;		//Referance to the player
//	void Start () 
//	{
//		player = GetComponent<playerCharacter>();
//
//		if(playerNumber == 1)
//		{
//			slots = GameObject.Find("p1_buff_slots");
//		}
//		else if(playerNumber == 2)
//		{
//			slots = GameObject.Find("p2_buff_slots");
//		}
//		else if(playerNumber == 4)
//		{
//			slots = GameObject.Find("p3_buff_slots");
//		}
//		else
//		{
//			slots = GameObject.Find("p4_buff_slots");
//		}
//		player.buffSlots = slots;
//	}
//
//	void Update () 
//	{
//		if(player.debuffs.Count > 0)
//		{
//			foreach(debuff effect in player.debuffs.ToArray())
//			{
//				GameObject childObject = Instantiate(HoT) as GameObject;
//				childObject.transform.parent = slots.transform;
//				childObject.GetComponent<Image>().fillAmount = effect.lifeTime;
//
//			}
//		}
//		if(player.buffs.Count > 0)
//		{
//			foreach(buff effect in player.buffs.ToArray())
//			{
//				GameObject childObject = Instantiate(HoT) as GameObject;
//				childObject.transform.parent = slots.transform;
//				childObject.GetComponent<Image>().fillAmount = effect.lifeTime;
//			}
//		}
//
//	}
}

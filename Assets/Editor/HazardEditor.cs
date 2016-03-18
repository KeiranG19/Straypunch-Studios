using UnityEngine;
using System.Collections;
using UnityEditor;

//[CustomEditor(typeof(Hazard))]
//public class HazardEditor : Editor {
//	//bool damage_hazard;
//	//bool slowing_hazard;
//
//	public override void OnInspectorGUI()
//	{
//		Hazard myHazard = (Hazard)target;
//		myHazard.Type = (Hazard.type)EditorGUILayout.EnumPopup("Hazard type: ",myHazard.Type);
//
//		/*
//		 * 	DAMAGE HAZARD GUI
//		 */
//		if(myHazard.Type == Hazard.type.area_damage)
//		{
//			EditorGUI.indentLevel++;
//			myHazard.damage = EditorGUILayout.IntField("Damage: ",myHazard.damage);
//			myHazard.activated = EditorGUILayout.Toggle("Is Active: ",myHazard.activated);
//			myHazard.waitTimer = EditorGUILayout.FloatField("Wait Timer:",myHazard.waitTimer);
//		}
//		else
//		{
//			//damage_hazard = false;
//			//myHazard.damage = 0;
//		}
//
//		/*
//		 * 	SLOWING HAZARD GUI
//		 */
//		if(myHazard.Type == Hazard.type.area_slow)
//		{
//			EditorGUI.indentLevel++;
//			myHazard.slowAmount = EditorGUILayout.IntField("slow speed to %: ",myHazard.slowAmount);
//			myHazard.activated = EditorGUILayout.Toggle("Is Active: ",myHazard.activated);
//		}
//		else
//		{
//			//slowing_hazard = false;
//			//myHazard.slowAmount = 0;
//
//		}
//	}
//}

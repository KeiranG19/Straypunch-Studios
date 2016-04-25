using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(powerup))]
public class powerupEditor : Editor {

	public override void OnInspectorGUI()
	{
		powerup Powerup = (powerup)target;
		Powerup.type = (powerup.Type)EditorGUILayout.EnumPopup("Powerup Type: ",Powerup.type);

		if (Powerup.type == powerup.Type.heal) 
		{
			EditorGUI.indentLevel++;
			Powerup.healTime = EditorGUILayout.FloatField("Buff Duration:",Powerup.healTime);
			Powerup.ready = EditorGUILayout.Toggle("Active",Powerup.ready);
		} 
		else 
		{

		}

		if (Powerup.type == powerup.Type.speed) 
		{
			EditorGUI.indentLevel++;
			Powerup.speedTime = EditorGUILayout.FloatField("Buff Duration:",Powerup.speedTime);
			Powerup.ready = EditorGUILayout.Toggle("Active",Powerup.ready);
		} 
		else 
		{
			
		}

		if (Powerup.type == powerup.Type.grow) 
		{
			EditorGUI.indentLevel++;
			Powerup.growTime = EditorGUILayout.FloatField("Buff Duration:",Powerup.growTime);
			Powerup.ready = EditorGUILayout.Toggle("Active",Powerup.ready);
		} 
		else 
		{

		}

		if (Powerup.type == powerup.Type.attack) 
		{
			EditorGUI.indentLevel++;
			Powerup.attackTime = EditorGUILayout.FloatField("Buff Duration:",Powerup.attackTime);
			Powerup.ready = EditorGUILayout.Toggle("Active",Powerup.ready);
		} 
		else 
		{

		}
	}
}
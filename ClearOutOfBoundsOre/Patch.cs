using HarmonyLib;
using UnityEngine;
using TMPro;
using System.Collections.Generic;


[HarmonyPatch(typeof(DebugManager))]
class Patch
{

	[HarmonyPatch(typeof(DebugManager),"ClearAllPhysicsOrePieces")]
	[HarmonyPrefix]
	static bool ClearAllPhysicsOrePieces_Prefix()
	{
		int deleteCount = 0;
		int nullCount = 0;
		
		foreach (OrePiece orePiece in Object.FindObjectsByType<OrePiece>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
		{
			if (orePiece.transform.position.y <= -100f)
			{
				orePiece.Delete();
				deleteCount++;
			}
		}
		ConveyorBelt[] array3 = Object.FindObjectsByType<ConveyorBelt>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].ClearNullObjectsOnBelt();
			nullCount++;
		}

		Debug.Log($"{PluginInfo.Name}: Deleted {deleteCount} ore below the map and {nullCount} null objects on belts");

		return false;
	}
	
}


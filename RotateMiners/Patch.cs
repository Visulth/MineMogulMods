using HarmonyLib;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Reflection;


[HarmonyPatch(typeof(BuildingManager))]
class Patch
{
	static AccessTools.FieldRef<BuildingManager, BuildingObject> GhostObjectRef = AccessTools.FieldRefAccess<BuildingManager, BuildingObject>("_ghostObject");

	static Quaternion preRotation = Quaternion.identity;
	//static void Postfix(PauseMenu __instance)

	//public CanPlaceBuilding CanPlaceObject(Vector3Int position, BuildingObject objPrefab, Quaternion rotation, bool requiresFlatGround, PlacementNodeRequirement placementNodeRequirement, out BuildingPlacementNode AttachedNode, ToolBuilder activeTool)

	static MethodBase TargetMethod()
	{
		return AccessTools.Method(
			typeof(BuildingManager),
			"CanPlaceObject",
			new[]
			{
				typeof(Vector3Int),
				typeof(BuildingObject),
				typeof(Quaternion),
				typeof(bool),
				typeof(PlacementNodeRequirement),
				typeof(BuildingPlacementNode).MakeByRefType(), // out
				typeof(ToolBuilder)
			}
		);
	}

	[HarmonyPrefix]
	static bool CanPlaceBuilding_Prefix(BuildingManager __instance, PlacementNodeRequirement placementNodeRequirement)
	{
		if (placementNodeRequirement == PlacementNodeRequirement.None)
			return true;

		preRotation = GhostObjectRef(__instance).transform.rotation;

		return true;
	}

	[HarmonyPostfix]
	static void CanPlaceBuilding_Postfix(BuildingManager __instance, PlacementNodeRequirement placementNodeRequirement)
	{
		if (placementNodeRequirement == PlacementNodeRequirement.None)
			return;

		GhostObjectRef(__instance).transform.rotation = preRotation;

	}

}
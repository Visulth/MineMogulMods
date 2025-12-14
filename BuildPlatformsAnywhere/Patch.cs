using HarmonyLib;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Reflection;

namespace NoSupports
{
	[HarmonyPatch(typeof(BuildingManager))]
	class Patch
    {
		//static AccessTools.FieldRef<PauseMenu, TMP_Text> TotalOrePiecesTextRef = AccessTools.FieldRefAccess<PauseMenu, TMP_Text>("TotalOrePiecesText");
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
		static bool CanPlaceBuilding_Prefix(BuildingManager __instance, ref CanPlaceBuilding __result)
		{
			var objectID = __instance.GetGhostObject().GetSavableObjectID();

			if (!ShouldAllowObject(objectID))
				return true;
			
			__result = CanPlaceBuilding.Valid;

			return false;
		}

		static bool ShouldAllowObject(SavableObjectID objectID)
		{
			switch (objectID)
			{
				case SavableObjectID.Scaffold2x2:
				case SavableObjectID.Scaffold2x2_Ramp:
					return true;
			}
			return false;
		}

	}
}

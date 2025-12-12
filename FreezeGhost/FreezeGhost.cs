using HarmonyLib;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

namespace NoSupports
{
	[HarmonyPatch(typeof(ToolBuilder))]
	class FreezeGhost
    {
		static AccessTools.FieldRef<ToolBuilder, float> UseRangeRef = AccessTools.FieldRefAccess<ToolBuilder, float>("UseRange");

		static float UseRange = 3f;

		//static Vector3 SuggestedPosition = Vector3.zero;
		static Vector3 LastBuildPosition;
		//static Vector3Int LastBuildPosition;
		static Vector3 NudgeDirection = Vector3.zero;

		static bool ShouldFreezeGhost = false;

		[HarmonyPatch(typeof(ToolBuilder), "Update")]
		[HarmonyPostfix]
		static void Update_Postfix(ToolBuilder __instance)
		{
			if (!Singleton<BuildingManager>.Instance.IsInBuildingMode())
				return;

			if (Input.GetKeyDown(KeyCode.H))
			{
				ShouldFreezeGhost = !ShouldFreezeGhost;
				Singleton<UIManager>.Instance.UpdateOnScreenControls(__instance);
			}

			if (!ShouldFreezeGhost)
				return;

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				NudgeDirection += Vector3.forward;
				Debug.Log($"Nudging ${NudgeDirection}");
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				NudgeDirection += Vector3.back;
				Debug.Log($"Nudging ${NudgeDirection}");
			}
			//else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4))
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				NudgeDirection += Vector3.left;
				Debug.Log($"Nudging ${NudgeDirection}");
			}
			//else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6))
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				NudgeDirection += Vector3.right;
				Debug.Log($"Nudging ${NudgeDirection}");
			}
			//else if (Input.GetKeyDown(KeyCode.Keypad8))
			else if (Input.GetKeyDown(KeyCode.PageUp))
			{
				NudgeDirection += Vector3.up;
				Debug.Log($"Nudging ${NudgeDirection}");
			}
			//else if (Input.GetKeyDown(KeyCode.Keypad2))
			else if (Input.GetKeyDown(KeyCode.PageDown))
			{
				NudgeDirection += Vector3.down;
				Debug.Log($"Nudging ${NudgeDirection}");
			}
		}

		[HarmonyPatch(typeof(ToolBuilder), "GetControlsText")]
		[HarmonyPostfix]
		static void GetControlsText_Postfix(ref string __result)
		{
			string newControls = string.Empty;
			if (!ShouldFreezeGhost)
			{
				newControls += "\nFreeze Hologram - H";
			}
			else
			{
				newControls += "\nUnfreeze Hologram - H";
				newControls += "\nHorizontal Nudge - Arrow Keys";
				newControls += "\nVertical Nudge - Page Up/Down";
			}
			__result += newControls;
		}

		[HarmonyPatch(typeof(ToolBuilder), "GetBuildPosition")]
		[HarmonyPostfix]
		static void GetBuildPosition_Postfix(Camera playerCamera, ref Vector3 __result)
		{
			//Vector3 newBuildPosition = __result;

			if (ShouldFreezeGhost)
			{
				__result = LastGhostPosition(playerCamera);
			}
			else
			{

				//LastBuildPosition = Singleton<BuildingManager>.Instance.GetClosestGridPosition(__result);
				LastBuildPosition = __result;
			}
		}

		//[HarmonyPatch(typeof(ToolBuilder), "GetBuildPosition")]
		//[HarmonyPrefix]
		//static bool GetBuildPosition_Prefix(ToolBuilder __instance, Camera playerCamera, ref Vector3 __result)
		//{

		//	UseRange = UseRangeRef(__instance);

		//	Vector3 newBuildPosition = GetBuildPosition(playerCamera);

		//	if (ShouldFreezeGhost)
		//	{
		//		__result = LastGhostPosition(playerCamera);	
		//	}
		//	else
		//	{
		//		__result = newBuildPosition;
		//		LastBuildPosition = newBuildPosition;
		//	}

		//	return false;
		//}

		//static Vector3 GetBuildPosition(Camera playerCamera)
		//{
		//	RaycastHit raycastHit;
		//	if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out raycastHit, UseRange, Singleton<BuildingManager>.Instance.BuildingPlacementRaycastLayer))
		//	{
		//		return raycastHit.point;
		//	}
		//	return playerCamera.transform.position + playerCamera.transform.forward * UseRange;
		//}

		static Vector3 LastGhostPosition(Camera playerCamera)
		{
			if (NudgeDirection == Vector3.zero)
				return LastBuildPosition;

			//Vector3 localNudge = playerCamera.transform.parent.TransformDirection(NudgeDirection);
			//Vector3 localNudge = Singleton<BuildingManager>.Instance.GhostObjectTransform.position + NudgeDirection;


			//LastBuildPosition += localNudge;
			LastBuildPosition += GetConstrainedNudge(playerCamera);
			//LastBuildPosition += Vector3Int.FloorToInt(GetConstrainedNudge(playerCamera));
			return LastBuildPosition;

			//Vector3 nudgedPosition = LastBuildPosition + localNudge;
			//NudgeDirection = Vector3.zero;

			//return nudgedPosition;
		}

		static Vector3 GetConstrainedNudge(Camera playerCamera)
		{
			Vector3 nudge = Vector3.zero;
			Vector3 horizontalNudge = NudgeDirection;
			horizontalNudge.y = 0f;

			if (horizontalNudge != Vector3.zero)
			{
				Vector3 localNudge = playerCamera.transform.parent.TransformDirection(horizontalNudge);
				//Vector3 cameraRelNudge = playerCamera.transform.parent

				Transform ghost = Singleton<BuildingManager>.Instance.GhostObjectTransform;

				float forwardAmount = Vector3.Dot(localNudge, ghost.forward);
				float rightAmount = Vector3.Dot(localNudge, ghost.right);

				if (Mathf.Abs(forwardAmount) >= Mathf.Abs(rightAmount))
				{
					nudge = ghost.forward * Mathf.Sign(forwardAmount);
				}
				else
				{
					nudge = ghost.right * Mathf.Sign(rightAmount);
				}
			}		

			//Vector3 nudge = objForward * forwardAmount + objRight * rightAmount;
			if (Mathf.Abs(NudgeDirection.y) >= 1.0f)
			{
				nudge += Vector3.up * Mathf.Sign(NudgeDirection.y);
			}			
			//nudge += ghost.up * Mathf.Sign(NudgeDirection.y);

			NudgeDirection = Vector3.zero;

			return nudge;
		}

		static void ResetGhostSetting()
		{
			ShouldFreezeGhost = false;
			NudgeDirection = Vector3.zero;
		}

		[HarmonyPatch(typeof(ToolBuilder), "PrimaryFire")]
		[HarmonyPostfix]
		static void PrimaryFire_PostFix()
		{
			ResetGhostSetting();
		}

		[HarmonyPatch(typeof(ToolBuilder), "OnDisable")]
		[HarmonyPostfix]
		static void OnDisable_PostFix()
		{
			ResetGhostSetting();
		}
	}
}

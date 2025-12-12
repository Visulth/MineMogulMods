using HarmonyLib;
using UnityEngine;

namespace DeletePickaxes
{
	[HarmonyPatch(typeof(PauseMenu))] 
	[HarmonyPatch("OnClearAllPhysicsPressed")]
	class DeletePickaxes
    {
		static void Postfix(PauseMenu __instance)
		{
			//ToolPickaxe[] pickaxes = FindObjectsByType<ToolPickaxe>(UnityEngine.FindObjectsInactive.Include, UnityEngine.FindObjectsSortMode.None);
			ToolPickaxe[] pickaxes = Object.FindObjectsOfType<ToolPickaxe>(true);

			foreach (var pickaxe in pickaxes)
			{
				Object.Destroy(pickaxe.gameObject);
			}
		}
	}
}

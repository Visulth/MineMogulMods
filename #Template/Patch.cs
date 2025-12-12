using HarmonyLib;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

namespace NoSupports
{
	[HarmonyPatch(typeof(ModularBuildingSupports))] 
	[HarmonyPatch("SpawnSupports")]
	class NoSupports
    {
		//static AccessTools.FieldRef<PauseMenu, TMP_Text> TotalOrePiecesTextRef = AccessTools.FieldRefAccess<PauseMenu, TMP_Text>("TotalOrePiecesText");
		//static void Postfix(PauseMenu __instance)
		static bool Prefix(PauseMenu __instance)
		{
			return false;
		}

	}
}

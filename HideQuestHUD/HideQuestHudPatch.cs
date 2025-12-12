using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace HideQuestHUD
{
	[HarmonyPatch(typeof(QuestHud))]
	[HarmonyPatch("AddQuest")]
	class HideQuestHudPatch
    {
		static bool Prefix(QuestHud __instance)
		{
			// Returning false skips the original method
			return false;
		}
	}
}

using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace HideQuestHUD
{

	[HarmonyPatch(typeof(QuestHud))]
	class HideQuestHudPatch
    {

		static AccessTools.FieldRef<QuestHud, List<QuestInfoUI>> questInfoUIRef = AccessTools.FieldRefAccess<QuestHud, List<QuestInfoUI>>("_questInfoUIs");
		//Called by questInfoUIRef(__instance)

		//static Action<QuestHud, Quest> _addQuest;
		//	_addQuest = HarmonyLib.AccessTools.MethodDelegate<Action<QuestHud, Quest>>(HarmonyLib.AccessTools.Method(typeof(QuestHud), "AddQuest"));

		[HarmonyPatch("AddQuest")]
		[HarmonyPrefix]
		static bool AddQuest_Prefix()
		{
			// Returning false skips the original method
			return false;
		}

		[HarmonyPatch("Update")]
		[HarmonyPrefix]
		static bool Update_Prefix()
		{
			// Returning false skips the original method
			return false;
		}

		//[HarmonyPatch("OnEnable")]
		//[HarmonyPostfix]
		//static void OnEnable_Postfix(QuestHud __instance)
		//{
		//	QuestInfoUI questInfoUI = Object.Instantiate<QuestInfoUI>(__instance.QuestInfoUIPrefab, __instance.QuestInfoUIsContainer);
		//	questInfoUIRef(__instance).Add(questInfoUI);

		//	//__instance.AddQuest
		//	//HarmonyLib.AccessTools.Method
		//	//_addQuest(__instance, quest);
		//}

		//[HarmonyPatch("Awake")]
		//[HarmonyPostfix]
		//static void Awake_Postfix()
		//{
		//	if (_addQuest != null)
		//		return;

		//	_addQuest = HarmonyLib.AccessTools.MethodDelegate<Action<QuestHud, Quest>>(HarmonyLib.AccessTools.Method(typeof(QuestHud), "AddQuest"));
		//}

		[HarmonyPatch("RegenerateQuestList")]
		[HarmonyPostfix]
		static void RQL_Postfix()
		{
			Debug.Log($"{PluginInfo.Name}: Attempting to RegenerateQuestList!");
		}
	}
}

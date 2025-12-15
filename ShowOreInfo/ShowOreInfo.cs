using HarmonyLib;
using UnityEngine;
//using UnityEngine.Physics;
using TMPro;
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace ShowOreInfo
{

	[HarmonyPatch(typeof(PauseMenu))]
	class ShowOreInfo
    {

		static float lastUpdateTime = 0f;
		static string lastOreUpdate = "";

		//const float VELOCITY_THRESHOLD = 0.01f;
		const float VELOCITY_THRESHOLD = 0.05f;

		const string MARKED = "MarkedForDestruction";

		//static AccessTools.FieldRef<PauseMenu, TMP_Text> TotalOrePiecesTextRef = AccessTools.FieldRefAccess<PauseMenu, TMP_Text>("TotalOrePiecesText");
		//static void Postfix(PauseMenu __instance)

		[HarmonyPatch(typeof(PauseMenu),"OnEnable")]
		[HarmonyPostfix]
		static void Start_Postfix(PauseMenu __instance)
		{
			//__instance.TotalOrePiecesText.horizontalAlignment = HorizontalAlignmentOptions.Center;
			//__instance.TotalOrePiecesText.alignment = TextAlignmentOptions.Center;
			//__instance.TotalOrePiecesText.autoSizeTextContainer = true;
			__instance.TotalOrePiecesText.enableAutoSizing = true;
			__instance.TotalOrePiecesText.fontSizeMin = 10;
			//Debug.Log("ShowOreInfo: Adjusting TotalOrePieces Text!");
		}

		//[HarmonyPatch("Update")]
		[HarmonyPatch(typeof(PauseMenu), "Update")]
		[HarmonyPrefix]
		static bool Prefix(PauseMenu __instance)
		{
			string oreUpdateText = lastOreUpdate;
			
			if (Time.realtimeSinceStartup - lastUpdateTime < 1f)
				return false;
			
			lastUpdateTime = Time.realtimeSinceStartup;

			//Debug.Log("ShowOreInfo: Updating Ore Count!");

			//oreUpdateText = UpdateOreCounts();
			oreUpdateText = BuildResourceString();

			//for (int i = 0; i < OrePiece.AllOrePieces.Count; i++)
			//{

			//}

			//TotalOrePiecesTextRef(__instance).text = " (Active Ore Physics Objects: " + OrePiece.AllOrePieces.Count + " )";

			//__instance.TotalOrePiecesText.text = $"<size=12>{oreUpdateText}</size>";
			__instance.TotalOrePiecesText.text = oreUpdateText;

			lastOreUpdate = oreUpdateText;
			return false;
		}

		//static string UpdateOreCounts()
		//{
		//	string result = string.Empty;

		//	//result += "ResourceType Counts:\n";

		//	foreach (ResourceType rt in Enum.GetValues(typeof(ResourceType)))
		//	{
		//		if (rt == ResourceType.INVALID) continue; // skip INVALID if needed
		//		int count = OrePiece.AllOrePieces.Count(op => op.ResourceType == rt);
		//		result += $"{rt}: {count}, ";
		//	}
		//	return result;
		//}

		private static readonly StringBuilder _sb = new StringBuilder(128);
		
		static string BuildResourceString()
		{
			_sb.Clear();
			bool first = true;
			int resourceCount = 0;

			foreach (ResourceType rt in Enum.GetValues(typeof(ResourceType)))
			{
				if (rt == ResourceType.INVALID) 
					continue;

				//int count = OrePiece.AllOrePieces.Count(op => op.ResourceType == rt);
				List<OrePiece> matching = new List<OrePiece>();
				
				int count = 0;
				int sleepingCount = 0;
				
				foreach (var op in OrePiece.AllOrePieces)
				{
					if (op.ResourceType != rt)
						continue;

					//Rigidbody rb = op.GetComponentInChildren<Rigidbody>();
					//op.rigi

					//op.

					if (op.Rb)
					{
						if (IsUntouched(op))
						{
							sleepingCount++;
						}
					}
					
					count++;
				}

				resourceCount++;

				if (!first)
					_sb.Append(", ");

				if (resourceCount > 0 && resourceCount % 4 == 0)
				{
					//_sb.AppendLine();
					//_sb.Append("\n");
					resourceCount = 0;
				}

				_sb.Append(rt);
				_sb.Append(": ");
				_sb.Append(count);
				_sb.Append(" (Static: ");
				_sb.Append(sleepingCount);
				_sb.Append(")");

				first = false;
			}

			return _sb.ToString();
		}

		static bool IsUntouched(OrePiece op)
		{
			if (op.gameObject.tag == MARKED)
				return false;

			if (op.Rb.IsSleeping())
				return true;

			if (op.Rb.isKinematic) //Used when ore is being processed I think?
				return false;

			if (op.Rb.linearVelocity.sqrMagnitude <= VELOCITY_THRESHOLD)
				return true;

			return false;
		}

	}
}

using HarmonyLib;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Text;

namespace ShowOreInfo
{
	[HarmonyPatch(typeof(PauseMenu))] 
	[HarmonyPatch("Update")]
	class ShowOreInfo
    {

		static float lastUpdateTime = 0f;
		static string lastOreUpdate = "";

		static AccessTools.FieldRef<PauseMenu, TMP_Text> TotalOrePiecesTextRef = AccessTools.FieldRefAccess<PauseMenu, TMP_Text>("TotalOrePiecesText");
		//static void Postfix(PauseMenu __instance)
		static bool Prefix(PauseMenu __instance)
		{
			string oreUpdateText = lastOreUpdate;
			
			if (Time.realtimeSinceStartup - lastUpdateTime < 1f)
				return false;
			
			lastUpdateTime = Time.realtimeSinceStartup;

			//oreUpdateText = UpdateOreCounts();
			oreUpdateText = BuildResourceString();

			//for (int i = 0; i < OrePiece.AllOrePieces.Count; i++)
			//{

			//}

			//TotalOrePiecesTextRef(__instance).text = " (Active Ore Physics Objects: " + OrePiece.AllOrePieces.Count + " )";
			TotalOrePiecesTextRef(__instance).text = oreUpdateText;

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
			//This runs in Update() so I'm being pointlessly careful about memory allocation
			_sb.Clear();
			bool first = true;

			foreach (ResourceType rt in Enum.GetValues(typeof(ResourceType)))
			{
				if (rt == ResourceType.INVALID) 
					continue;

				int count = OrePiece.AllOrePieces.Count(op => op.ResourceType == rt);

				if (!first)
					_sb.Append(", ");

				_sb.Append(rt);
				_sb.Append(": ");
				_sb.Append(count);

				first = false;
			}

			return _sb.ToString();
		}

	}
}

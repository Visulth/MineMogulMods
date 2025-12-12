using BepInEx;
using UnityEngine;
using HarmonyLib;

[BepInPlugin("com.visulth.hidequesthud", "HideQuestHUD", "1.0.0")]
public class Plugin : BaseUnityPlugin
{

	private Harmony _harmony;

	void Awake()
	{
		_harmony = new Harmony("com.visulth.hidequesthud");
		_harmony.PatchAll();  // Applies all [HarmonyPatch] annotations in your assembly

		Logger.LogInfo("HideQuestHUD loaded.");
	}

	private void OnDestroy()
	{
		//_harmony.UnpatchAll("com.visulth.hidequesthud");
		_harmony.UnpatchSelf();
	}
}
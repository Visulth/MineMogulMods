using BepInEx;
using UnityEngine;
using HarmonyLib;

[BepInPlugin("com.visulth.ShowOreInfo", "ShowOreInfo", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
	private Harmony _harmony;

	void Awake()
	{
		_harmony = new Harmony("com.visulth.ShowOreInfo");
		_harmony.PatchAll();  // Applies all [HarmonyPatch] annotations in your assembly
		Logger.LogInfo("ShowOreInfo loaded.");
	}

	private void OnDestroy()
	{
		_harmony.UnpatchSelf();
	}
}
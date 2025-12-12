using BepInEx;
using UnityEngine;
using HarmonyLib;

[BepInPlugin("com.visulth.NoSupports", "NoSupports", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
	private Harmony _harmony;

	void Awake()
	{
		_harmony = new Harmony("com.visulth.NoSupports");
		_harmony.PatchAll();  // Applies all [HarmonyPatch] annotations in your assembly
		Logger.LogInfo("NoSupports loaded.");
	}

	private void OnDestroy()
	{
		_harmony.UnpatchSelf();
	}
}
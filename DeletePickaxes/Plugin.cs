using BepInEx;
using UnityEngine;
using HarmonyLib;

[BepInPlugin("com.visulth.DeletePickaxes", "DeletePickaxes", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
	private Harmony _harmony;

	void Awake()
	{
		_harmony = new Harmony("com.visulth.DeletePickaxes");
		_harmony.PatchAll();  // Applies all [HarmonyPatch] annotations in your assembly
		Logger.LogInfo("DeletePickaxes loaded.");
	}

	private void OnDestroy()
	{
		_harmony.UnpatchSelf();
	}
}
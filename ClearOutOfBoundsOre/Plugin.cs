using BepInEx;
using UnityEngine;
using HarmonyLib;

internal static class PluginInfo
{
	public const string Author = "Visulth";
	public const string Name = "ClearOutOfBoundsOre";
	public const string GUID = "com." + Author + "." + Name;
	public const string Version = "1.0.0";
}

[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
public class Plugin : BaseUnityPlugin
{
	private Harmony _harmony;

	void Awake()
	{
		_harmony = new Harmony(PluginInfo.GUID);
		_harmony.PatchAll();  // Applies all [HarmonyPatch] annotations in your assembly
		Logger.LogInfo($"{PluginInfo.Name} loaded.");
	}

	private void OnDestroy()
	{
		_harmony.UnpatchSelf();
	}
}
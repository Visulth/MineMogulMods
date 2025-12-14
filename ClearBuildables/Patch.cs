using HarmonyLib;
using UnityEngine;
using TMPro;


class Patch
{

	//static AccessTools.FieldRef<ToolBuilder, float> UseRangeRef = AccessTools.FieldRefAccess<ToolBuilder, float>("UseRange");

	[HarmonyPatch(typeof(PauseMenu), nameof(PauseMenu.OnClearAllPhysicsPressed))]
	static void Postfix(PauseMenu __instance)
	{
		BuildingObject[] buildings = Object.FindObjectsByType<BuildingObject>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

		int destroyCount = 0;

		foreach (var building in buildings)
		{
			if (!ShouldDestroyObject(building.SavableObjectID))
				continue;

			Debug.Log($"{PluginInfo.Name}: Deleting {building.gameObject.name}, located at {building.transform.position}");
			Object.Destroy(building.gameObject);
			destroyCount++;
		}

		Debug.Log($"{PluginInfo.Name}: Deleted {destroyCount} buildings!");
	}

	static bool ShouldDestroyObject(SavableObjectID objectID)
	{

		switch (objectID)
		{
			//Early Access v0.1.1.0
			case SavableObjectID.CannonTarget:
			case SavableObjectID.Conveyor90:
			case SavableObjectID.Conveyor90_mirror:
			case SavableObjectID.ConveyorBlocker:
			case SavableObjectID.ConveyorCannon:
			case SavableObjectID.ConveyorDown:
			case SavableObjectID.ConveyorSorter:
			case SavableObjectID.ConveyorSorter_Mirror:
			case SavableObjectID.ConveyorStraight:
			case SavableObjectID.ConveyorTCombiner:
			case SavableObjectID.ConveyorTSplitter:
			case SavableObjectID.ConveyorUp:
			case SavableObjectID.LCombiner:
			case SavableObjectID.LCombiner_Mirror:
			case SavableObjectID.RollerStraight:
			case SavableObjectID.RollerUp:
			case SavableObjectID.RobotGrabberArm:
			case SavableObjectID.ConveyorLedge:
			case SavableObjectID.RollerConveyor90:
			case SavableObjectID.RollerConveyor90_Mirror:
			case SavableObjectID.ConveyorRouting_Left:
			case SavableObjectID.ConveyorRouting_Right:
			case SavableObjectID.RollerConveyorRouting_Left:
			case SavableObjectID.RollerConveyorRouting_Right:
			case SavableObjectID.ConveyorBlockerT2:
			case SavableObjectID.ConveyorSplitterT2:
			case SavableObjectID.Sorter_Lid:
			case SavableObjectID.WallConveyor_Straight:
			case SavableObjectID.WallConveyor_Up:
			case SavableObjectID.WallConveyor_Down:
			case SavableObjectID.WallConveyor_Ledge:
			case SavableObjectID.WallConveyor_90:
			case SavableObjectID.WallConveyor_90_Mirror:
			case SavableObjectID.OreOverflowSplitter:
			case SavableObjectID.RollerConveyorUp:
			case SavableObjectID.RollerConveyorDown:
			case SavableObjectID.VerticalBulkSorter:
			case SavableObjectID.WallConveyor_Straight_LeftOnly:
			case SavableObjectID.WallConveyor_Straight_RightOnly:
			case SavableObjectID.RollerSorter:
			case SavableObjectID.RollerSorter_Mirror:
			case SavableObjectID.BlastFurnace:
			case SavableObjectID.Grinder:
			case SavableObjectID.Hopper:
			case SavableObjectID.OreAnalyzer:
			case SavableObjectID.OreAnalyzer_Mirror:
			case SavableObjectID.PolishingMachine:
			case SavableObjectID.RollingMill:
			case SavableObjectID.PipeRoller:
			case SavableObjectID.Packager:
			case SavableObjectID.RodExtruder:
			case SavableObjectID.ShakerTable:
			case SavableObjectID.Scaffold2x2:
			case SavableObjectID.Scaffold2x2_Ramp:

			case SavableObjectID.Chute:
			case SavableObjectID.Chute_Window:
			case SavableObjectID.Chute_Top:
			case SavableObjectID.Chute_Angled:
			case SavableObjectID.Chute_Splitter:
			case SavableObjectID.Chute_Bottom:
			case SavableObjectID.Chute_Hopper:
			case SavableObjectID.Chute_Top_Angle:
			case SavableObjectID.Chute_Top_Angle_Window:
			case SavableObjectID.Chute_Hatch:
				return true;

			case SavableObjectID.INVALID:
				break;
			case SavableObjectID.RapidAutoMiner:
				break;
			case SavableObjectID.LampPole:
				break;
			case SavableObjectID.AutoMinerMk1:
				break;
			case SavableObjectID.Chest1x2x1:
				break;
			case SavableObjectID.Arrow_Sign:
				break;
			case SavableObjectID.Arrow_Sign_Mirror:
				break;
			case SavableObjectID.ToolBuilder:
				break;
			case SavableObjectID.HammerBasic:
				break;
			case SavableObjectID.Lantern:
				break;
			case SavableObjectID.MagnetTool:
				break;
			case SavableObjectID.PickaxeBasic:
				break;
			case SavableObjectID.ResourceScannerTool:
				break;
			case SavableObjectID.RapidAutoMinerStandardDrillBit:
				break;
			case SavableObjectID.RapidAutoMinerTurboDrillBit:
				break;
			case SavableObjectID.RapidAutoMinerHardenedDrillBit:
				break;
			case SavableObjectID.WrenchTool:
				break;
			case SavableObjectID.BuildingCrate1x1x1:
				break;
			case SavableObjectID.BuildingCrate1x1x2:
				break;
			case SavableObjectID.BuildingCrate1x2x1:
				break;
			case SavableObjectID.BuildingCrateAutoMiner:
				break;
			case SavableObjectID.BuildingCrate1x2x2:
				break;
			case SavableObjectID.Box1x1x1:
				break;
			case SavableObjectID.HalloweenPumpkin:
				break;
			case SavableObjectID.DiamondHalloweenPumpkin:
				break;
			case SavableObjectID.HolidayLampPole1:
				break;
			case SavableObjectID.HolidayLampPole2:
				break;
			case SavableObjectID.ChristmasTree:
				break;
			case SavableObjectID.Trophy_CoalMogul:
				break;
			case SavableObjectID.Trophy_CopperMogul:
				break;
			case SavableObjectID.Trophy_GoldMogul:
				break;
			case SavableObjectID.Trophy_IronMogul:
				break;
			case SavableObjectID.Trophy_MineMogul:
				break;
			case SavableObjectID.Upgrade_DepositBoxT2:
				break;
			case SavableObjectID.DevTestShopItems:
				break;
			default:
				break;
		}

		return false;

	}
}


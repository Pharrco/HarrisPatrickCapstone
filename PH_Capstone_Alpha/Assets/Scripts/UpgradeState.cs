using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeState
{
	public enum UpgradeType { HealthMaxPlus, PowerMaxPlus, MapFeaturePlus, WhiteCostDown, InfrdCostDown, UlvltCostDown, None }

	private Queue<UpgradeType> available_upgrades;	// The list of all upgrades that have not been collected or been available in the store
	private UpgradeType[] store_upgrades;			// The list of the upgrades currently for sale in the store
	private List<UpgradeType> purchased_upgrades;   // The list of the upgrades that the player has purchased

	// A dictionary of the titles of upgrades for display in the store
	public static Dictionary<UpgradeType, string> upgrade_titles = new Dictionary<UpgradeType, string>() {
		{ UpgradeType.HealthMaxPlus, "Max Health +" },
		{ UpgradeType.PowerMaxPlus , "Max Power +" },
		{ UpgradeType.MapFeaturePlus , "" },
		{ UpgradeType.WhiteCostDown , "" },
		{ UpgradeType.InfrdCostDown , "" },
		{ UpgradeType.UlvltCostDown , "" },
		{ UpgradeType.None , "" }
	};
	// A dictionary of the descriptions of upgrades for display in the store
	public static Dictionary<UpgradeType, string> upgrade_description = new Dictionary<UpgradeType, string>() {
		{ UpgradeType.HealthMaxPlus, "" },
		{ UpgradeType.PowerMaxPlus , "" },
		{ UpgradeType.MapFeaturePlus , "" },
		{ UpgradeType.WhiteCostDown , "" },
		{ UpgradeType.InfrdCostDown , "" },
		{ UpgradeType.UlvltCostDown , "" },
		{ UpgradeType.None , "" }
	};
	// A dictionary of the cost of the first level of an upgrade for calculation of cost
	private static Dictionary<UpgradeType, int> upgrade_cost_base = new Dictionary<UpgradeType, int>() {
		{ UpgradeType.HealthMaxPlus, 500 },
		{ UpgradeType.PowerMaxPlus , 1000 },
		{ UpgradeType.MapFeaturePlus , 0 },
		{ UpgradeType.WhiteCostDown , 0 },
		{ UpgradeType.InfrdCostDown , 0 },
		{ UpgradeType.UlvltCostDown , 0 },
		{ UpgradeType.None , 0 }
	};
	// A dictionary of the amount the cost of an upgrade increases with each level purchased for calculation of cost
	private static Dictionary<UpgradeType, int> upgrade_cost_increment = new Dictionary<UpgradeType, int>() {
		{ UpgradeType.HealthMaxPlus, 200 },
		{ UpgradeType.PowerMaxPlus , 400 },
		{ UpgradeType.MapFeaturePlus , 0 },
		{ UpgradeType.WhiteCostDown , 0 },
		{ UpgradeType.InfrdCostDown , 0 },
		{ UpgradeType.UlvltCostDown , 0 },
		{ UpgradeType.None , 0 }
	};

	public UpgradeState()
	{
		// Initialize the empty lists
		available_upgrades = new Queue<UpgradeType>();
		store_upgrades = new UpgradeType[3];
		purchased_upgrades = new List<UpgradeType>();

		// Fill the list of available upgrades with all the upgrades in the game
		available_upgrades.Enqueue(UpgradeType.HealthMaxPlus);
		available_upgrades.Enqueue(UpgradeType.PowerMaxPlus);
		available_upgrades.Enqueue(UpgradeType.PowerMaxPlus);
		available_upgrades.Enqueue(UpgradeType.PowerMaxPlus);

		// Get the first set of upgrades
		store_upgrades[0] = available_upgrades.Dequeue();
		store_upgrades[1] = available_upgrades.Dequeue();
		store_upgrades[2] = available_upgrades.Dequeue();
	}

	// Purchase an upgrade and get a new item from the queue of available upgrades
	// Occurs after cash transaction has taken place
	public void PurchaseStoreUpgrade(int index)
	{
		// If the provided index is valid
		if (index == Mathf.Clamp(index, 0, 2))
		{
			// If an upgrade exists in this position
			if (store_upgrades[index] != UpgradeType.None)
			{
				// Add the upgrade to purchased
				purchased_upgrades.Add(store_upgrades[index]);

				// If there are items left in the available upgrades lift
				if (available_upgrades.Count > 0)
				{
					// Get the next upgrade from the list of available upgrades
					store_upgrades[index] = available_upgrades.Dequeue();
				}
				else
				{
					// No more upgrades, this space is empty
					store_upgrades[index] = UpgradeType.None;
				}
			}
		}
	}

	// Get the number of HealthMaxPlus upgrades the player has purchased
	public int GetHealthMaxPlusCount()
	{
		return purchased_upgrades.FindAll(el => el == UpgradeType.HealthMaxPlus ).Count;
	}

	// Get the number of PowerMaxPlus upgrades the player has purchased
	public int GetPowerMaxPlusCount()
	{
		return purchased_upgrades.FindAll(el => el == UpgradeType.PowerMaxPlus).Count;
	}

	// Get the number of MapFeaturePlus upgrades the player has purchased
	public int GetMapFeaturePlusCount()
	{
		return purchased_upgrades.FindAll(el => el == UpgradeType.MapFeaturePlus).Count;
	}

	// Get the number of WhiteCostDown upgrades the player has purchased
	public int GetWhiteCostDownCount()
	{
		return purchased_upgrades.FindAll(el => el == UpgradeType.WhiteCostDown).Count;
	}

	// Get the number of InfrdCostDown upgrades the player has purchased
	public int GetInfrdCostDownCount()
	{
		return purchased_upgrades.FindAll(el => el == UpgradeType.InfrdCostDown).Count;
	}

	// Get the number of UlvltCostDown upgrades the player has purchased
	public int GetUlvltCostDownCount()
	{
		return purchased_upgrades.FindAll(el => el == UpgradeType.UlvltCostDown).Count;
	}

	// Get the type of upgrade in a position in the store
	public UpgradeType GetStoreItemUpgradeType(int index)
	{
		if ((index < 3) && (index > -1))
		{
			return store_upgrades[index];
		}
		else
		{
			return UpgradeType.None;
		}
	}

	// Get the cost of a particular upgrade type based on the base cost and the number already purchased
	public int GetUpgradeCost(UpgradeType type)
	{
		int count = purchased_upgrades.FindAll(el => el == type).Count;
		int base_cost = upgrade_cost_base[type];
		int increment_cost = upgrade_cost_increment[type] * count;
		return base_cost + increment_cost;
	}
}

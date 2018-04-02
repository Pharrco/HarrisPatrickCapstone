using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour {

	[SerializeField]
	GameObject upgradePanel_A, upgradePanel_B, upgradePanel_C;
	Text cashText_store;
	GameObject[] upgradePanels;

	// Use this for initialization
	void Start () {
		// Create and fill the panel arrays
		upgradePanels = new GameObject[3] { upgradePanel_A, upgradePanel_B, upgradePanel_C };

		// Set the player cash display
		cashText_store = GameObject.Find("PlayerCashText").GetComponent<Text>();
		cashText_store.text = "$" + GameSave.loaded_save.GetPlayerCash().ToString();

		// For each of the upgrade panels
		for (int i = 0; i < upgradePanels.Length; i++)
		{
			upgradePanels[i].transform.Find("Title/TitleText").GetComponent<Text>().text = UpgradeState.upgrade_titles[GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(i)];
			upgradePanels[i].transform.Find("DescripText").GetComponent<Text>().text = UpgradeState.upgrade_description[GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(i)];
			upgradePanels[i].transform.Find("Button/Text").GetComponent<Text>().text = "Buy ($" + GameSave.loaded_save.Player_upgradeState.GetUpgradeCost(GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(i)) + ")";
		}
	}
	
	// Update the UI
	public void UpdateStoreUI()
	{
		cashText_store.text = "$" + GameSave.loaded_save.GetPlayerCash().ToString();

		// For each of the upgrade panels
		for (int i = 0; i < upgradePanels.Length; i++)
		{
			upgradePanels[i].transform.Find("Title/TitleText").GetComponent<Text>().text = UpgradeState.upgrade_titles[GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(i)];
			upgradePanels[i].transform.Find("DescripText").GetComponent<Text>().text = UpgradeState.upgrade_description[GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(i)];
			upgradePanels[i].transform.Find("Button/Text").GetComponent<Text>().text = "Buy ($" + GameSave.loaded_save.Player_upgradeState.GetUpgradeCost(GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(i)) + ")";
		}
	}

	// Purchase an upgrade
	public void PurchaseStoreUpgrade(int n_index)
	{
		// If the transaction goes through successfully
		if (GameSave.loaded_save.SpendPlayerCash(GameSave.loaded_save.Player_upgradeState.GetUpgradeCost(GameSave.loaded_save.Player_upgradeState.GetStoreItemUpgradeType(n_index))))
		{
			// Purchase the upgrade
			GameSave.loaded_save.Player_upgradeState.PurchaseStoreUpgrade(n_index);

			// Save the game state
			MasterGameController.SaveCurrent();

			// Update the store UI
			UpdateStoreUI();
		}
		else
		{
			Debug.Log("Insufficient funds");
		}

	}
}

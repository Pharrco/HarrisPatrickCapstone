using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievePageController : MonoBehaviour {

	[SerializeField]
	GameObject achieve_prefab;
	GameObject achieve_port;
	AchievementType[] achievements = new AchievementType[] { AchievementType.GoldCollect, AchievementType.GoldHoard };

	// Use this for initialization
	void Start () {
		achieve_port = GameObject.Find("AchievementContent");

		foreach (AchievementType type in achievements)
		{
			string prefix = "";

			switch (GameSave.loaded_save.Player_achieveState.AchievementDict[type].GetAchievementLevel())
			{
				case AchievementLevel.None:
					prefix = "Warning for ";
					break;
				case AchievementLevel.Misdemeanor:
					prefix = "Misdemeanor Charge of ";
					break;
				case AchievementLevel.Felony:
					prefix = "Felony Charge of ";
					break;
				case AchievementLevel.Capital:
					prefix = "Capital Charge of ";
					break;
			}

			GameObject n_entry = GameObject.Instantiate(achieve_prefab, achieve_port.transform);
			n_entry.transform.Find("AchieveTitle").GetComponent<Text>().text = prefix + AchieveState.AchieveTitleDict[type];
			n_entry.transform.Find("AchieveDesc").GetComponent<Text>().text = AchieveState.AchieveDescDict[type];
			n_entry.transform.Find("ProgFill").GetComponent<Image>().fillAmount = (float)GameSave.loaded_save.Player_achieveState.AchievementDict[type].GetCurrAchievementSuccess() / (float)GameSave.loaded_save.Player_achieveState.AchievementDict[type].GetCurrAchievementTarget();
			n_entry.transform.Find("ProgFrame/ProgText").GetComponent<Text>().text = GameSave.loaded_save.Player_achieveState.AchievementDict[type].GetCurrAchievementSuccess().ToString() + " / " + GameSave.loaded_save.Player_achieveState.AchievementDict[type].GetCurrAchievementTarget().ToString();
		}
	}
}

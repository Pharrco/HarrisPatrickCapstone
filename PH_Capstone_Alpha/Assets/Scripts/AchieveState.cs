using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType { GoldCollect, GoldHoard }
public enum AchievementLevel { None, Misdemeanor, Felony, Capital }

[System.Serializable]
public class AchieveState {

	public Dictionary<AchievementType, Achievement> AchievementDict { get; private set; }
	public static Dictionary<AchievementType, string> AchieveTitleDict = new Dictionary<AchievementType, string>
	{
		{ AchievementType.GoldCollect, "Theft of Gold" },
		{ AchievementType.GoldHoard, "Hoarding of Gold"}
};

	public static Dictionary<AchievementType, string> AchieveDescDict = new Dictionary<AchievementType, string>
	{
		{ AchievementType.GoldCollect, "For the theft of gold from our lady and her vassals" },
		{ AchievementType.GoldHoard, "For the withholding of funds from the economy" }
	};

	public AchieveState()
	{
		AchievementDict = new Dictionary<AchievementType, Achievement>();

		// Gold Collect Achievement
		AddAchievement(AchievementType.GoldCollect, true, 10000, 100000, 1000000);

		// Gold Hoard Achievement
		AddAchievement(AchievementType.GoldHoard, false, 500000, 5000000, 50000000);
	}

	void AddAchievement(AchievementType n_type, bool n_increment, int n_low, int n_med, int n_high)
	{
		Achievement n_achievement = new Achievement(n_type, n_increment, n_low, n_med, n_high);
		AchievementDict.Add(n_type, n_achievement);

	}
}
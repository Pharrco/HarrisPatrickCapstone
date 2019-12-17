using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement {

	public AchievementType achieveType;
	bool incrementSuccess;
	int currSuccess;
	int lowSuccess, medSuccess, highSuccess;

	public Achievement(AchievementType n_type, bool n_increment, int n_low, int n_med, int n_high)
	{
		achieveType = n_type;
		incrementSuccess = n_increment;
		lowSuccess = n_low;
		medSuccess = n_med;
		highSuccess = n_high;
		currSuccess = 0;
	}

	public AchievementLevel GetAchievementLevel()
	{
		if (currSuccess >= highSuccess)
		{
			return AchievementLevel.Capital;
		}
		else if (currSuccess >= medSuccess)
		{
			return AchievementLevel.Felony;
		}
		else if (currSuccess >= lowSuccess)
		{
			return AchievementLevel.Misdemeanor;
		}

		return AchievementLevel.None;
	}

	public int GetCurrAchievementSuccess()
	{
		return currSuccess;
	}

	public int GetCurrAchievementTarget()
	{
		switch (GetAchievementLevel())
		{
			case (AchievementLevel.None):
				return lowSuccess;
			case (AchievementLevel.Misdemeanor):
				return medSuccess;
			default:
				return highSuccess;
		}
	}

	public void UpdateAchievementSuccess()
	{
		if (incrementSuccess)
		{
			currSuccess += 1;
		}
		else
		{
			Debug.Log("Call of increment achievement update for non-incremented achievement " + achieveType);
		}
	}

	public void UpdateAchievementSuccess(int n_value)
	{
		if (incrementSuccess)
		{
			currSuccess += n_value;
		}
		else
		{
			currSuccess = n_value;
		}
	}
}

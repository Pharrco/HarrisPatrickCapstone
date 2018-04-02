using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObjectController : BaseEnvironmentController {

	int position_x, position_y;
	bool invertLightState;
	bool currState;
	Enumerators.SwitchColorType switch_color;

	public bool SwitchOn
	{
		get
		{
			if (invertLightState)
			{
				return !currState;
			}
			else
			{
				return currState;
			}
		}
	}

	// Use this for initialization
	public void Initialize (int n_posX, int n_posY, bool n_invert, Enumerators.SwitchColorType n_switch_color)
	{
		position_x = n_posX;
		position_y = n_posY;
		invertLightState = n_invert;
		switch_color = n_switch_color;
		float x_offset = 1.25f * Mathf.Sign(Random.Range(-1f, 1f));
		float y_offset = 1.25f * Mathf.Sign(Random.Range(-1f, 1f));

		transform.position = new Vector3(x_offset + (position_x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(position_x, position_y) - 1.0f, y_offset + (position_y - (BuildBoard.GetArrayWidth() / 2)) * 5);

		SwitchNetworkController.AddToSwitchList(this, switch_color);
	}

	public override void GetMove() {
		MoveComplete = true;
	}

	public override void Reset() {
		UpdateSwitchState();
	}

	public void UpdateSwitchState()
	{
		if ((LightEffectControl.GetLightPoint(position_x, position_y) == LightStatus.White) || (LightEffectControl.GetLightPoint(position_x, position_y) == LightStatus.Ulvlt))
		{
			currState = true;
		}
		else
		{
			currState = false;
		}

		if (SwitchOn)
		{
			transform.Find("SwitchCore").GetComponent<ParticleSystem>().Play();
		}
		else
		{
			transform.Find("SwitchCore").GetComponent<ParticleSystem>().Stop();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEndPoint : MonoBehaviour {

	bool invertLightState;
	bool require_all;
	bool currState;
	GameObject EndPointMarker;
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
	public void Initialize (int n_posX, int n_posY, bool n_invert, bool n_all, Enumerators.SwitchColorType n_switch_color)
	{
		invertLightState = n_invert;
		switch_color = n_switch_color;
		require_all = n_all;

		EndPointMarker = GameObject.Instantiate(GetEndPointPrefab(n_switch_color));

		SwitchNetworkController.AddToEndpointList(this);

		float x_offset = 1.25f * Mathf.Sign(Random.Range(-1f, 1f));
		float y_offset = 1.25f * Mathf.Sign(Random.Range(-1f, 1f));

		EndPointMarker.transform.position = new Vector3(x_offset + (n_posX - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue(n_posX, n_posY) - 1.0f, y_offset + (n_posY - (BuildBoard.GetArrayWidth() / 2)) * 5);
	}

	public void UpdateSwitchState()
	{
		if (require_all)
		{
			currState = SwitchNetworkController.GetNetworkState_And(switch_color);
		}
		else
		{
			currState = SwitchNetworkController.GetNetworkState_Or(switch_color);
		}

		if (SwitchOn)
		{
			EndPointMarker.transform.Find("SwitchCore").GetComponent<ParticleSystem>().Play();
		}
		else
		{
			EndPointMarker.transform.Find("SwitchCore").GetComponent<ParticleSystem>().Stop();
		}

		GetComponent<BaseEnvironmentController>().ForceUpdate();
	}

	GameObject GetEndPointPrefab(Enumerators.SwitchColorType n_switch_color)
	{
		switch (n_switch_color)
		{
			case Enumerators.SwitchColorType.Blue:
				return EnemySingleton.singleton.blue_endPoint;
			case Enumerators.SwitchColorType.Red:
				return EnemySingleton.singleton.red_endPoint;
			case Enumerators.SwitchColorType.Green:
				return EnemySingleton.singleton.green_endPoint;
			default:
				return EnemySingleton.singleton.purple_endPoint;
		}
	}
}

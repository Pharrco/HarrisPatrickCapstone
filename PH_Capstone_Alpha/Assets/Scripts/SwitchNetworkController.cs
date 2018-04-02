using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchNetworkController {

	static List<SwitchObjectController> purple_switches = new List<SwitchObjectController>();
	static List<SwitchObjectController> red_switches = new List<SwitchObjectController>();
	static List<SwitchObjectController> blue_switches = new List<SwitchObjectController>();
	static List<SwitchObjectController> green_switches = new List<SwitchObjectController>();
	static List<SwitchEndPoint> end_points = new List<SwitchEndPoint>();

	public static void AddToSwitchList(SwitchObjectController n_switch, Enumerators.SwitchColorType n_color)
	{
		switch (n_color)
		{
			case Enumerators.SwitchColorType.Purple:
				purple_switches.Add(n_switch);
				break;
			case Enumerators.SwitchColorType.Red:
				red_switches.Add(n_switch);
				break;
			case Enumerators.SwitchColorType.Blue:
				blue_switches.Add(n_switch);
				break;
			case Enumerators.SwitchColorType.Green:
				green_switches.Add(n_switch);
				break;
		}
	}

	public static void AddToEndpointList(SwitchEndPoint n_endpoint)
	{
		end_points.Add(n_endpoint);
	}

	public static void UpdateEndpoints()
	{
		foreach (SwitchEndPoint endpoint in end_points)
		{
			endpoint.UpdateSwitchState();
		}
	}

	// Reset all the lists before any switches are added to the list
	public static void ResetSwitchNetworks()
	{
		purple_switches.Clear();
		red_switches.Clear();
		blue_switches.Clear();
		green_switches.Clear();
	}

	// Get if all switches of a particular color are active
	public static bool GetNetworkState_And(Enumerators.SwitchColorType n_color)
	{
		bool network_state = true;

		switch (n_color)
		{
			case Enumerators.SwitchColorType.Purple:
				foreach ( SwitchObjectController switch_obj in purple_switches)
				{
					network_state = (network_state) && (switch_obj.SwitchOn);
				}
				return network_state;
			case Enumerators.SwitchColorType.Red:
				foreach (SwitchObjectController switch_obj in red_switches)
				{
					network_state = (network_state) && (switch_obj.SwitchOn);
				}
				return network_state;
			case Enumerators.SwitchColorType.Blue:
				foreach (SwitchObjectController switch_obj in blue_switches)
				{
					network_state = (network_state) && (switch_obj.SwitchOn);
				}
				return network_state;
			case Enumerators.SwitchColorType.Green:
				foreach (SwitchObjectController switch_obj in green_switches)
				{
					network_state = (network_state) && (switch_obj.SwitchOn);
				}
				return network_state;
		}

		return false;
	}

	// Get if any switches of a particular color are active
	public static bool GetNetworkState_Or(Enumerators.SwitchColorType n_color)
	{
		bool network_state = false;

		switch (n_color)
		{
			case Enumerators.SwitchColorType.Purple:
				foreach (SwitchObjectController switch_obj in purple_switches)
				{
					network_state = (network_state) || (switch_obj.SwitchOn);
				}
				return network_state;
			case Enumerators.SwitchColorType.Red:
				foreach (SwitchObjectController switch_obj in red_switches)
				{
					network_state = (network_state) || (switch_obj.SwitchOn);
				}
				return network_state;
			case Enumerators.SwitchColorType.Blue:
				foreach (SwitchObjectController switch_obj in blue_switches)
				{
					network_state = (network_state) || (switch_obj.SwitchOn);
				}
				return network_state;
			case Enumerators.SwitchColorType.Green:
				foreach (SwitchObjectController switch_obj in green_switches)
				{
					network_state = (network_state) || (switch_obj.SwitchOn);
				}
				return network_state;
		}

		return false;
	}

	// Update all switches to conform to light changes at end of player and ally turn phases
	public static void UpdateAllSwitches()
	{
		foreach (SwitchObjectController switch_obj in purple_switches)
		{
			switch_obj.UpdateSwitchState();
		}
		foreach (SwitchObjectController switch_obj in red_switches)
		{
			switch_obj.UpdateSwitchState();
		}
		foreach (SwitchObjectController switch_obj in blue_switches)
		{
			switch_obj.UpdateSwitchState();
		}
		foreach (SwitchObjectController switch_obj in green_switches)
		{
			switch_obj.UpdateSwitchState();
		}

		UpdateEndpoints();
	}

}

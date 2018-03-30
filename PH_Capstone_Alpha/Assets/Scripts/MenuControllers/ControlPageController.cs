using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPageController : MonoBehaviour {

	[SerializeField]
	Text description_text, title_text;
	[SerializeField]
	GameObject single_key, up_key, down_key, right_key, left_key, video_display;

	Dictionary<string, ControlPageDisplay> controlDisp_dictionary;

	// Use this for initialization
	void Start () {
		controlDisp_dictionary = new Dictionary<string, ControlPageDisplay>();

		controlDisp_dictionary.Add("player_movement", new ControlPageDisplay( "Player Movement", "Description of player movement", "", "W", "S", "A", "D", Color.white, ""));
		controlDisp_dictionary.Add("camera_movement", new ControlPageDisplay("Camera Movement", "Description of camera movement", "", "UP", "DOWN", "LEFT", "RIGHT", Color.white, ""));
	}

	public void LoadControlPageDisplay(string index)
	{
		if (controlDisp_dictionary.ContainsKey(index))
		{
			ControlPageDisplay active_ControlPage = controlDisp_dictionary[index];

			// Set title and description
			title_text.text = active_ControlPage.title_text;
			description_text.text = active_ControlPage.description_text;

			// Set single key
			single_key.transform.Find("Text").GetComponent<Text>().text = active_ControlPage.single_text;
			if (active_ControlPage.single_text != "")
			{
				single_key.GetComponent<Image>().color = active_ControlPage.control_color;
			}
			else
			{
				single_key.GetComponent<Image>().color = Color.clear;
			}

			// Set up key
			up_key.transform.Find("Text").GetComponent<Text>().text = active_ControlPage.up_text;
			if (active_ControlPage.up_text != "")
			{
				up_key.GetComponent<Image>().color = active_ControlPage.control_color;
			}
			else
			{
				up_key.GetComponent<Image>().color = Color.clear;
			}

			// Set down key
			down_key.transform.Find("Text").GetComponent<Text>().text = active_ControlPage.down_text;
			if (active_ControlPage.down_text != "")
			{
				down_key.GetComponent<Image>().color = active_ControlPage.control_color;
			}
			else
			{
				down_key.GetComponent<Image>().color = Color.clear;
			}

			// Set left key
			left_key.transform.Find("Text").GetComponent<Text>().text = active_ControlPage.left_text;
			if (active_ControlPage.left_text != "")
			{
				left_key.GetComponent<Image>().color = active_ControlPage.control_color;
			}
			else
			{
				left_key.GetComponent<Image>().color = Color.clear;
			}

			// Set right key
			right_key.transform.Find("Text").GetComponent<Text>().text = active_ControlPage.right_text;
			if (active_ControlPage.right_text != "")
			{
				right_key.GetComponent<Image>().color = active_ControlPage.control_color;
			}
			else
			{
				right_key.GetComponent<Image>().color = Color.clear;
			}
		}
	}
}

public class ControlPageDisplay
{
	public string title_text;
	public string description_text;
	public string single_text;
	public string up_text;
	public string down_text;
	public string left_text;
	public string right_text;
	public Color control_color;
	public string video_address;

	public ControlPageDisplay(string n_title_text, string n_description_text, string n_single_text, string n_up_text, string n_down_text, string n_left_text, string n_right_text, Color n_control_color, string n_video_address)
	{
		title_text = n_title_text;
		description_text = n_description_text;
		single_text = n_single_text;
		up_text = n_up_text;
		down_text = n_down_text;
		left_text = n_left_text;
		right_text = n_right_text;
		control_color = n_control_color;
		video_address = n_video_address;
	}
}

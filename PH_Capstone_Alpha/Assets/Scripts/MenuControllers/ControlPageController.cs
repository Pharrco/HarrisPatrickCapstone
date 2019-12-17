using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPageController : MonoBehaviour {

	[SerializeField]
	Text description_text, title_text;
	[SerializeField]
	GameObject single_key, up_key, down_key, right_key, left_key;
	[SerializeField]
	Image display_image;
	[SerializeField]
	Sprite display_playerMove, display_cameraMove, display_cameraZoom, display_takingTurns, display_lightResource, display_whiteLight, display_infrdLight, display_ulvltLight, display_dispelMagic, display_chaoticMagic, display_switches, display_timer, display_miniMap, display_gorgonLock, display_treasure, display_levelComplete, display_allies;

	Dictionary<string, ControlPageDisplay> controlDisp_dictionary;

	// Use this for initialization
	void Start () {

		if (display_image != null)
		{
			display_image.color = Color.clear;
		}

		controlDisp_dictionary = new Dictionary<string, ControlPageDisplay>();

		controlDisp_dictionary.Add("player_movement", new ControlPageDisplay("Player Movement", "During your turn, when the turn indicator at the bottom of the screen is blue, you may select to move one space in a direction by pressing WASD relative to the direction your character is facing. Once you select a move, you will not be able to move again until any enemies have taken their turns and it is your turn again.", "", "W", "S", "A", "D", Color.white, display_playerMove));
		controlDisp_dictionary.Add("camera_movement", new ControlPageDisplay("Camera Movement", "To get a different view of the situation, change the position of the camera using the arrow keys. Changing positions this way unlocks the camera's position, so it will stay at the same position. Pressing tab locks the position of the camera so it will rotate as the player does. Pressing tab again will lock the camera behind the player.", "Tab", "UP", "DOWN", "LEFT", "RIGHT", Color.white, display_cameraMove));
		controlDisp_dictionary.Add("camera_zoom", new ControlPageDisplay("Camera Zoom", "Zoom in on your character using +/= key.\n\nZoom out from your character using _/- key.", "", "", "-", "", "=", Color.white, display_cameraZoom));
		controlDisp_dictionary.Add("take_turns", new ControlPageDisplay("Taking Turns", "Each enemy on the field gets a chance to move between your turns. The round indicator on the bottom center of your screen indicates whose turn it is.\n\nBLUE: PLAYER\nRED: ENEMIES", "", "", "", "", "", Color.white, display_takingTurns));
		controlDisp_dictionary.Add("light_resource", new ControlPageDisplay("Light Resource", "Your light is your greatest weapon, but it is a limited one and there are many dangers in the dark. Conserve power by switching to less costly colors of light. Tools may exist to help players running low on power.", "", "", "", "", "", Color.white, display_lightResource));
		controlDisp_dictionary.Add("light_white", new ControlPageDisplay("White Light", "White light is your flashlight's default setting. Above all, the white light is best for illuminating the environment around you and alerting every enemy to your location.", "7", "", "", "", "", Color.white, display_whiteLight));
		controlDisp_dictionary.Add("light_infrd", new ControlPageDisplay("Infrared Light", "The flashlight's low power mode allows you to move among enemies unnoticed. Be cautious, though, since infrared light is not strong enough to trigger switches and you may find that some opponents don't like being snuck up on.", "8", "", "", "", "", Color.white, display_infrdLight));
		controlDisp_dictionary.Add("light_ulvlt", new ControlPageDisplay("Ultraviolet Light", "The flashlight's most powerful setting may blind enemies or drive them into a rage, and consumes power faster than any other setting. It also illuminates twice the distance of white or infrared and can trigger switches from afar.", "9", "", "", "", "", Color.white, display_ulvltLight));
		controlDisp_dictionary.Add("magic_dispel", new ControlPageDisplay("Dispelling Magic", "The witch's magic has penetrated every molecule of the mansion and these concentrated deposits of magic prevent your flashlight's battery from recharging. Clear the level of thes magic deposits to lower the room's ambient magic until your flashlight can charge.", "", "", "", "", "", Color.white, display_dispelMagic));
		controlDisp_dictionary.Add("magic_chaos", new ControlPageDisplay("Chaotic Magic", "Some accumululations of magic are more chaotic and powerful than others. The only way to contain such potent magic is with a special binding that keeps the chaotic magic in check. This binding is extremely dangerous once set and any attempts to reenter an area wwith one in place will cause extreme injury.", "", "", "", "", "", Color.white, display_chaoticMagic));
		controlDisp_dictionary.Add("switch_function", new ControlPageDisplay("Switches", "These switches channel energy between multiple locations in the area, allowing the witch's traps to be activated or deactivated from a distance when switches are triggered by white or ultraviolet light. Different colors have different functionality, experiment to master them all.", "", "", "", "", "", Color.white, display_switches));
		controlDisp_dictionary.Add("timer_function", new ControlPageDisplay("Timers", "Some of the witch's traps are set to trigger on a set timer. Keep track of these timers on the right hand side of the screen where each timer has it's own indicator which will keep you updated on the status of a particular timer. The small circle in the middle of the indicator lets you know that the timer is linked to a switch and will only count down when the appropriate switch is activated, turning the circle green.", "", "", "", "", "", Color.white, display_timer));
		controlDisp_dictionary.Add("mini_map", new ControlPageDisplay("Mini-Map", "Open the menu, using the button in the bottom left corner of your screen to view the mini-map.", "", "", "", "", "", Color.white, display_miniMap));
		controlDisp_dictionary.Add("gorgon_lock", new ControlPageDisplay("Gorgon Lock", "The gorgon sisters are not as strong as their sister, Medusa, but they'll stop you in your tracks. If your character has turned to stone or you see the gorgon lock icon just above the turn indicator, you can still move backwards, left, or right. Check for other ways that will allow you to move without facing the gorgon's direction.", "", "", "", "", "", Color.white, display_gorgonLock));
		controlDisp_dictionary.Add("treasure_items", new ControlPageDisplay("Treasure", "Capture treasure chests to earn money in-level. The number at the top of your screen shows the total amount of cash you have earned in the level so far. Cash is only added to your total cash when you win or lose a level, though the total is reduced if you lose.", "", "", "", "", "", Color.white, display_treasure));
		controlDisp_dictionary.Add("level_complete", new ControlPageDisplay("Level Completion", "To complete a level, you'll have to neutralize all the concentrated magic in the area, including chaotic energy. However, at times, certain creatures may increase the total amount of magic in the room. In these cases, it is not always necessary to neutralize all the magic in the room. In these cases, an indicator will show on the indicator in the top right corner showing the amount necessary to complete the level", "", "", "", "", "", Color.white, display_levelComplete));
		controlDisp_dictionary.Add("allies_items", new ControlPageDisplay("Allies", "Coming Soon: You're not alone anymore. Decoys and recharging allies planned for beta!", "", "", "", "", "", Color.white, display_allies));
	}

	public void LoadControlPageDisplay(string index)
	{
		if (controlDisp_dictionary.ContainsKey(index))
		{
			ControlPageDisplay active_ControlPage = controlDisp_dictionary[index];

			// Set title and description
			title_text.text = active_ControlPage.title_text;
			description_text.text = active_ControlPage.description_text;

			if (active_ControlPage.display_image != null)
			{
				display_image.color = Color.white;
				display_image.sprite = active_ControlPage.display_image;
			}
			else
			{
				display_image.color = Color.clear;
			}

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
	public Sprite display_image;

	public ControlPageDisplay(string n_title_text, string n_description_text, string n_single_text, string n_up_text, string n_down_text, string n_left_text, string n_right_text, Color n_control_color, Sprite n_display_image)
	{
		title_text = n_title_text;
		description_text = n_description_text;
		single_text = n_single_text;
		up_text = n_up_text;
		down_text = n_down_text;
		left_text = n_left_text;
		right_text = n_right_text;
		control_color = n_control_color;
		display_image = n_display_image;
	}
}

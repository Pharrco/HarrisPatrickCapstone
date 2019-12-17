using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicPageController : MonoBehaviour {

	[SerializeField]
	Text description_text, title_text;
	[SerializeField]
	GameObject play_button;
	string level_id = "";
	Color playSymb_initial;

	Dictionary<string, string> cinematicTitleDict = new Dictionary<string, string>
	{
		{ "MenuCine_1_1", "Late Delivery" }
	};

	Dictionary<string, string> cinematicDescDict = new Dictionary<string, string>
	{
		{ "MenuCine_1_1", "In which a late night delivery, an annoying roommate, and a crazy old lady complicate the life of an innocent slacker." }
	};

	Dictionary<string, int> cinematicLevelDict = new Dictionary<string, int>
	{
		{ "MenuCine_1_1", 0 }
	};

	// Use this for initialization
	void Start () {
		playSymb_initial = play_button.transform.Find("Image").GetComponent<Image>().color;

		play_button.GetComponent<Image>().color = Color.clear;
		play_button.GetComponent<Button>().interactable = false;
		play_button.transform.Find("Image").GetComponent<Image>().color = Color.clear;

	}

	public void SetCinematicDisplay(string n_cinematic)
	{
		if (cinematicLevelDict.ContainsKey(n_cinematic))
		{
			if (cinematicLevelDict[n_cinematic] <= GameSave.loaded_save.GetLevelProgress())
			{
				level_id = n_cinematic;
				description_text.text = cinematicDescDict[n_cinematic];
				title_text.text = cinematicTitleDict[n_cinematic];

				play_button.GetComponent<Image>().color = Color.white;
				play_button.GetComponent<Button>().interactable = true;
				play_button.transform.Find("Image").GetComponent<Image>().color = playSymb_initial;
			}
			else
			{
				level_id = "";
				description_text.text = "Cinematic locked";
				title_text.text = cinematicTitleDict[n_cinematic];

				play_button.GetComponent<Image>().color = Color.clear;
				play_button.GetComponent<Button>().interactable = false;
				play_button.transform.Find("Image").GetComponent<Image>().color = Color.clear;
			}
		}
	}

	public void PlayCinematic()
	{
		if (level_id != "")
		{
			SceneManager.LoadScene(level_id);
		}
	}
}

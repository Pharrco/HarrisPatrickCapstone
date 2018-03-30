using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGuideSelectControl : MonoBehaviour {

	[SerializeField]
	GameObject display_base;
	[SerializeField]
	Vector3 display_offset;
	[SerializeField]
	Text description_text, title_text;

	[SerializeField]
	GameObject enemyDisp_TrueChest, enemyDisp_FalseChest, enemyDisp_Cauldron, enemyDisp_SlimeSimple, enemyDisp_Gorgon;

	GameObject display_object;

	Dictionary<string, string> title_dictionary;
	Dictionary<string, string> description_dictionary;
	Dictionary<string, GameObject> displayObj_dictionary;

	// Use this for initialization
	void Start () {
		title_dictionary = new Dictionary<string, string>();
		description_dictionary = new Dictionary<string, string>();
		displayObj_dictionary = new Dictionary<string, GameObject>();

		title_dictionary.Add("true_chest", "True Chest");
		title_dictionary.Add("false_chest", "Trap Chest");
		title_dictionary.Add("slime_cauldron", "Slime Cauldron");
		title_dictionary.Add("slime_simple", "Simple Slime");
		title_dictionary.Add("gorgon_direct", "Gorgon");

		description_dictionary.Add("true_chest", "True Chest description");
		description_dictionary.Add("false_chest", "Trap Chest description");
		description_dictionary.Add("slime_cauldron", "Slime Cauldron description");
		description_dictionary.Add("slime_simple", "Simple Slime description");
		description_dictionary.Add("gorgon_direct", "Gorgon description");

		displayObj_dictionary.Add("true_chest", enemyDisp_TrueChest);
		displayObj_dictionary.Add("false_chest", enemyDisp_FalseChest);
		displayObj_dictionary.Add("slime_cauldron", enemyDisp_Cauldron);
		displayObj_dictionary.Add("slime_simple", enemyDisp_SlimeSimple);
		displayObj_dictionary.Add("gorgon_direct", enemyDisp_Gorgon);
	}

	public void SelectGuideItem(string index)
	{
		if (displayObj_dictionary.ContainsKey(index))
		{
			Destroy(display_object);

			description_text.text = description_dictionary[index];
			title_text.text = title_dictionary[index];

			display_object = GameObject.Instantiate(displayObj_dictionary[index], display_base.transform);
			display_object.transform.localPosition = display_offset;
		}
	}
}

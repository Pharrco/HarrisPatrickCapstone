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
	GameObject enemyDisp_TrueChest, enemyDisp_FalseChest, enemyDisp_Cauldron, enemyDisp_SlimeSimple, enemyDisp_Gorgon, enemyDisp_Reaper;

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
		title_dictionary.Add("reaper", "Reaper");

		description_dictionary.Add("true_chest", "The only thing better than a big cash reward is an even bigger cash reward. Pick up these treasure chests to supplement your income and your chance of survival. Be careful, not all is as it seems, and you never know when your prize might get up and walk away.");
		description_dictionary.Add("false_chest", "All that glitters is not gold, and sometimes it is gold that is being spat at you at violent speeds by an angry chest on legs that's determined to deprive you of its sweet golden guts. Use ultraviolet to blind them with their own sparkle, but be careful trying to sneak up on them with Infrared, they tend to be hostile when startled.");
		description_dictionary.Add("slime_cauldron", "Overflowing with the witch's hatred for all things good and sanitary, these pots of bad are just looking for an excuse to boil over into a whole pile of bad.\n\nTry to avoid stirring it up or block off its drip area. Don't forget to keep an eye on the kitchen timer to avoid burns.");
		description_dictionary.Add("slime_simple", "A useless blob in both body and spirit, the slime's existence is dedicated to undoing the hard work of others. Take advantage of its obsessive dedication by depriving it of work and the moron will pop out of sheer stubbornness.\nInfrared seems to leave it slightly stupider than normal. Ultraviolet makes them spit like an angry llama, only with acid.");
		description_dictionary.Add("gorgon_direct", "Medusa's little sisters aren't much to look at, but they manage to be stunners in their own way. Glance in their direction and you'll end up a lawn decoration, at least until you can tear your eyes away.");
		description_dictionary.Add("reaper", "Reaper description");

		displayObj_dictionary.Add("true_chest", enemyDisp_TrueChest);
		displayObj_dictionary.Add("false_chest", enemyDisp_FalseChest);
		displayObj_dictionary.Add("slime_cauldron", enemyDisp_Cauldron);
		displayObj_dictionary.Add("slime_simple", enemyDisp_SlimeSimple);
		displayObj_dictionary.Add("gorgon_direct", enemyDisp_Gorgon);
		displayObj_dictionary.Add("reaper", enemyDisp_Reaper);
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

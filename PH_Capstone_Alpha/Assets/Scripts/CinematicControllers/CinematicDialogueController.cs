using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicDialogueController : MonoBehaviour {

    static GameObject dialogue_ui;
    static Text name_text_ui, message_text_ui;
    static Image speaker_image_ui;

    // Use this for initialization
    void Start () {
        // Get the dialogue UI
        dialogue_ui = GameObject.Find("DialoguePanel");
        name_text_ui = dialogue_ui.transform.Find("Name_text").GetComponent<Text>();
        message_text_ui = dialogue_ui.transform.Find("Message_text").GetComponent<Text>();
        speaker_image_ui = dialogue_ui.transform.Find("Speaker_image").GetComponent<Image>();

        // Inactivate dialogue UI
        dialogue_ui.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Receive dialogue data from cinematic controller
    public static void SetDialogue(Sprite n_speaker_sprite, string n_speaker_name, string n_dialogue_message)
    {
        // Activate dialogue UI
        dialogue_ui.SetActive(true);

        // Set the data
        name_text_ui.text = n_speaker_name;
        speaker_image_ui.sprite = n_speaker_sprite;
        message_text_ui.text = n_dialogue_message;
    }

    public static void ClearDialogue()
    {
        // Deactivate dialogue UI
        dialogue_ui.SetActive(false);
    }
}

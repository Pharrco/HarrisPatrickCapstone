﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineDat_1_1 : CinematicBase
{
	[SerializeField]
	CinematicCharAnimator hero_char, witch_char, roommate_char, richlady_char;
    [SerializeField]
    Sprite hero_profile, witch_profile, roommate_profile, richlady_profile;

    public CineDat_1_1()
    {
        Level_array = new int[,] {
        { 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1 },
        { 1, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 1},
		{ 1, 1, 1, 1, 1, 1}
		};

		Style_array = new int[,] {
		{ 0, 0, 0, 0, 1, 2 },
		{ 1, 1, 1, 0, 1, 2 },
		{ 1, 1, 1, 0, 1, 2 },
		{ 1, 1, 1, 1, 1, 2},
		{ 1, 1, 1, 0, 1, 2},
		{ 0, 0, 0, 0, 1, 2}
		};

	}

    private void Awake()
    {
        Cinematic_event_queue = new Queue<CinematicEvent>();
		// TODO: Add box spawn with effect

		Cinematic_event_queue.Enqueue(new CharSpawnEvent(roommate_char, new Vector2(2, 0)));
        Cinematic_event_queue.Enqueue(new CameraViewEvent(roommate_char.gameObject, new Vector3(45, 0, 0)));
        Cinematic_event_queue.Enqueue(new CharMoveEvent(roommate_char, new Vector2(2, 1)));
		Cinematic_event_queue.Enqueue(new CharRotateEvent(roommate_char, new Vector2(3, 1)));
		Cinematic_event_queue.Enqueue(new TimePauseEvent(2f));
		Cinematic_event_queue.Enqueue(new CharRotateEvent(roommate_char, new Vector2(2, 0)));
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "ROOMMATE", "Hey, slacker, package!", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new CharSpawnEvent(hero_char, new Vector2(2, 0)));
		Cinematic_event_queue.Enqueue(new CharMoveEvent(roommate_char, new Vector2(1, 1)));
		Cinematic_event_queue.Enqueue(new CharRotateEvent(roommate_char, new Vector2(3, 1)));
		Cinematic_event_queue.Enqueue(new CharMoveEvent(hero_char, new Vector2(2, 1)));
		Cinematic_event_queue.Enqueue(new CameraViewEvent(hero_char.gameObject, new Vector3(45, 0, 0), 1));
		Cinematic_event_queue.Enqueue(new CharRotateEvent(hero_char, new Vector2(3, 1)));
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Huh. I don't remember ordering anything. Maybe it's a gift from a secret admirer.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "ROOMMATE", "More likely it's a gift from your drunk self", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new CharRotateEvent(hero_char, new Vector2(1, 1)));
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "You're just jealous because someone loves me enough to send me a...", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		// TODO: Insert box destroy, reveal effect, spawn flashlight
		Cinematic_event_queue.Enqueue(new CharMoveEvent(hero_char, new Vector2(3, 1)));
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Flashlight.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new CharMoveEvent(roommate_char, new Vector2(2, 1)));
		Cinematic_event_queue.Enqueue(new CharRotateEvent(hero_char, new Vector2(1, 1)));
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "ROOMMATE", "HAHAHAHA. So, is there a love note from whoever wants to be sure you're not afraid of the dark?", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Nope, just a receipt with a less than loving return policy.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "ROOMMATE", "You have got to get that impulse-buying under control.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Nonsense, I always buy the coolest things when I'm too drunk to walk. It's like reverse beer-goggles.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "ROOMMATE", "Right. Because that $400 surfboard was a brilliant purchase considering you've never been to the ocean and can barely swim.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "One day I will prove my carpet surfing training method. But, first, I'll prove that this was the most intelligent well thought out purchase, ever.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "I'll show you, this thing probably has one thousand and one uses!.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "ROOMMATE", "Well, use number one, it keeps annoying roommates occupied.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new CharMoveEvent(roommate_char, new Vector2(2, 0)));
		Cinematic_event_queue.Enqueue(new CharRemoveEvent(roommate_char));
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Jerk.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Let's see about this thing. High power... Multi-frequency... Ooh, rechargable, that's convenient... Yeah, I'm sure I can find a use for this thing.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "YOU", "Worst case, I can shove it down that jerk's throat and see what kind of bright comments he coughs up then.", false));
		Cinematic_event_queue.Enqueue(new PauseEvent());
		Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		Cinematic_event_queue.Enqueue(new DialogueEvent(roommate_profile, "?????????", "WAAAAAAAAAAAAA", false));
		Cinematic_event_queue.Enqueue(new CameraViewEvent(hero_char.gameObject, new Vector3(135, 0, 0), 1));
		Cinematic_event_queue.Enqueue(new CharSpawnEvent(richlady_char, new Vector2(0, 4)));
		Cinematic_event_queue.Enqueue(new CharMoveEvent(richlady_char, new Vector2(1, 4)));
		// Cinematic_event_queue.Enqueue(new CharMoveEvent(roommate_char, new Vector2(0, 2)));
		// Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
		// Cinematic_event_queue.Enqueue(new CharMoveEvent(roommate_char, new Vector2(0, 3)));
		// Cinematic_event_queue.Enqueue(new CameraViewEvent(roommate_char.gameObject, new Vector3(135, 0, 0), 1));
		// Cinematic_event_queue.Enqueue(new CameraViewEvent(roommate_char.gameObject, new Vector3(225, 0, 0), 1));
		// Cinematic_event_queue.Enqueue(new CameraViewEvent(roommate_char.gameObject, new Vector3(315, 0, 0), 1));
		// Cinematic_event_queue.Enqueue(new CameraViewEvent(roommate_char.gameObject, new Vector3(45, 0, 0), 1));
	}

}

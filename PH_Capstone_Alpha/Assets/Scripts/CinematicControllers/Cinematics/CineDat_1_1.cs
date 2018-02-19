using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineDat_1_1 : CinematicBase
{

    [SerializeField]
    CinematicCharAnimator hero_char, witch_char;
    [SerializeField]
    Sprite hero_profile, witch_profile;

    public CineDat_1_1()
    {
        Level_array = new int[,] {
        { 2, 2, 2, 2 },
        { 2, 2, 2, 2 },
        { 2, 2, 2, 2 },
        { 2, 2, 2, 2 },
        };
    }

    private void Awake()
    {
        Cinematic_event_queue = new Queue<CinematicEvent>();
        Cinematic_event_queue.Enqueue(new CameraViewEvent(witch_char.gameObject, new Vector3(225, 0, 0)));
        Cinematic_event_queue.Enqueue(new DialogueEvent(witch_profile, "WITCH", "Cinematic dialogue goes here", false));
        Cinematic_event_queue.Enqueue(new CharAnimationEvent(witch_char, new Vector2(0, 1)));
        Cinematic_event_queue.Enqueue(new CharAnimationEvent(witch_char, new Vector2(0, 2)));
        Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
        Cinematic_event_queue.Enqueue(new CharAnimationEvent(witch_char, new Vector2(0, 3)));
        Cinematic_event_queue.Enqueue(new DialogueEvent(witch_profile, "WITCH", "Will this disappear with ENTER?", false));
        Cinematic_event_queue.Enqueue(new PauseEvent());
        Cinematic_event_queue.Enqueue(new ClearDialogueEvent());
        Cinematic_event_queue.Enqueue(new CameraViewEvent(witch_char.gameObject, new Vector3(135, 0, 0), 1));
        Cinematic_event_queue.Enqueue(new CameraViewEvent(witch_char.gameObject, new Vector3(225, 0, 0), 1));
        Cinematic_event_queue.Enqueue(new CameraViewEvent(witch_char.gameObject, new Vector3(315, 0, 0), 1));
        Cinematic_event_queue.Enqueue(new CameraViewEvent(witch_char.gameObject, new Vector3(45, 0, 0), 1));
    }

}

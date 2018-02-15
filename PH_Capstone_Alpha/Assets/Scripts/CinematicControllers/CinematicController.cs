using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour {

    Queue<CinematicEvent> cinematic_queue;
    CinematicEvent curr_event;
    float timer;
    GameObject continue_button;

	// Use this for initialization
	void Start () {
        // Deactivate the continue button
        continue_button = GameObject.Find("CinematicUI").transform.Find("ContinueButton").gameObject;
        continue_button.GetComponent<Image>().color = Color.clear;
        continue_button.GetComponent<Button>().interactable = false;

        // Initialize the list of events
        cinematic_queue = GetComponent<CinematicBase>().Cinematic_event_queue;

        // Get the first event
        if (cinematic_queue.Count > 0)
        {
            // Get and trigger the next event
            curr_event = cinematic_queue.Dequeue();
            curr_event.Trigger();

            // If the current event is a pause
            if (curr_event.Event_type == "pause")
            {
                // Activate the "Next" button
                continue_button.GetComponent<Image>().color = Color.white;
                continue_button.GetComponent<Button>().interactable = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (curr_event != null)
        {
            // If current step is not a pause event
            if (curr_event.Event_type != "pause")
            {
                // If current event's target script is complete
                if (curr_event.IsComplete())
                {
                    // Reset the target script
                    curr_event.Reset();

                    // Get and trigger the next event
                    if (cinematic_queue.Count > 0)
                    {
                        curr_event = cinematic_queue.Dequeue();
                        curr_event.Trigger();

                        // If the current event is a pause
                        if (curr_event.Event_type == "pause")
                        {
                            // Activate the "Next" button
                            continue_button.GetComponent<Image>().color = Color.white;
                            continue_button.GetComponent<Button>().interactable = true;
                        }
                    }
                    else
                    {
                        curr_event = null;
                    }
                }
            }
            // If the current step is a pause
            else
            {
                // Increment timer
                timer += Time.deltaTime;

                // When the player presses return after 0.5 seconds
                if (Input.GetKeyDown(KeyCode.Return) && (timer > 0.5f))
                {
                    // Get and trigger the next event
                    if (cinematic_queue.Count > 0)
                    {
                        // Get and trigger next event
                        curr_event = cinematic_queue.Dequeue();
                        curr_event.Trigger();
                        
                        // If the current event is a pause
                        if (curr_event.Event_type == "pause")
                        {
                            // Activate the "Next" button
                            continue_button.GetComponent<Image>().color = Color.white;
                            continue_button.GetComponent<Button>().interactable = true;
                        }
                    }
                    else
                    {
                        curr_event = null;
                    }
                }
            }
        }
	}

    public void ClickContinue()
    {
        // Activate the "Next" button
        continue_button.GetComponent<Image>().color = Color.clear;
        continue_button.GetComponent<Button>().interactable = false;

        // Get and trigger the next event
        if (cinematic_queue.Count > 0)
        {
            // Get and trigger next event
            curr_event = cinematic_queue.Dequeue();
            curr_event.Trigger();

            // If the current event is a pause
            if (curr_event.Event_type == "pause")
            {
                // Activate the "Next" button
                continue_button.GetComponent<Image>().color = Color.white;
                continue_button.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            curr_event = null;
        }
    }
}

// Event class
public class CinematicEvent
{
    public string Event_type { get; protected set; }

    public virtual void Trigger() { }

    public virtual bool IsComplete()
    {
        return true;
    }

    public virtual void Reset() { }
}

// Character animation event type
public class CharAnimationEvent : CinematicEvent
{
    bool completion_wait;
    Vector2 destination;
    CinematicCharAnimator cmd_target;

    public CharAnimationEvent(CinematicCharAnimator n_target, Vector2 n_destination, bool wait_for_next = true)
    {
        Event_type = "character";
        cmd_target = n_target;
        destination = n_destination;
        completion_wait = wait_for_next;
    }

    public override void Trigger() {
        // If the target still exists
        if (cmd_target != null)
        {
            // Send move to character
            cmd_target.StartMove((int)destination.x, (int)destination.y);

        }
    }

    public override bool IsComplete() {
        if (!completion_wait)
        {
            return true;
        }
        else
        {
            return cmd_target.Move_complete;
        }
    }

    public override void Reset()
    {
        cmd_target.ForceComplete();
    }
}

// Dialogue box event type
public class DialogueEvent : CinematicEvent
{
    Sprite speaker_sprite;
    string speaker_name;
    string dialogue_message;
    bool wait_for_input;

    public DialogueEvent(Sprite n_speaker_sprite, string n_speaker_name, string n_dialogue_message, bool n_wait_for_input)
    {
        Event_type = "dialogue";
        speaker_sprite = n_speaker_sprite;
        speaker_name = n_speaker_name;
        dialogue_message = n_dialogue_message;
        wait_for_input = n_wait_for_input;
    }

    public override void Trigger() {
        CinematicDialogueController.SetDialogue(speaker_sprite, speaker_name, dialogue_message);
    }

    public override bool IsComplete() {
        return true;
    }

    public override void Reset()
    {

    }
}

// Environment event type
public class EnvironmentEvent : CinematicEvent
{
    public EnvironmentEvent()
    {
        Event_type = "environment";
    }

    public override void Trigger() { }

    //public override bool IsComplete() { }

    public override void Reset()
    {

    }
}

// Pause event type
public class PauseEvent : CinematicEvent
{
    public PauseEvent()
    {
        Event_type = "pause";
    }

    public override void Trigger() { }

    //public override bool IsComplete() { }

    public override void Reset()
    {

    }
}

// Camera event type
public class CameraViewEvent : CinematicEvent
{
    GameObject target;
    Vector3 view;
    float duration;

    public CameraViewEvent(GameObject n_target, Vector3 n_view, float n_duration = 0)
    {
        Event_type = "camera";
        target = n_target;
        view = n_view;
        duration = n_duration;
    }

    public override void Trigger() {
        // Push the data to the camera controller
        CinematicCameraControl.SetCameraView(target, view, duration);
    }

    public override bool IsComplete() {
        return CinematicCameraControl.IsComplete;
    }

    public override void Reset()
    {

    }
}

// Activate Object event type
public class ActivateObjEvent : CinematicEvent
{
    public ActivateObjEvent()
    {
        Event_type = "activate";
    }

    public override void Trigger() { }

    //public override bool IsComplete() { }

    public override void Reset()
    {

    }
}

public class ClearDialogueEvent : CinematicEvent
{
    public ClearDialogueEvent()
    {
        Event_type = "cleardia";
    }

    public override void Trigger()
    {
        CinematicDialogueController.ClearDialogue();
    }

    public override bool IsComplete()
    {
        return true;
    }

    public override void Reset()
    {

    }
}
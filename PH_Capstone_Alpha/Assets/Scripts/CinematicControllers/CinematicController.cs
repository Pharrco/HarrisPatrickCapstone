using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicController : MonoBehaviour {

    Queue<CinematicEvent> cinematic_queue;
    CinematicEvent curr_event;
	float timer;
	public float pause_timer;
    GameObject continue_button;
	public GameObject display_image;

	// Use this for initialization
	void Start () {
        // Deactivate the continue button
        continue_button = GameObject.Find("CinematicUI").transform.Find("ContinueButton").gameObject;
        continue_button.GetComponent<Image>().color = Color.clear;
        continue_button.GetComponent<Button>().interactable = false;

		display_image = GameObject.Find("CinematicUI").transform.Find("DisplayImage").gameObject;
		display_image.GetComponent<Image>().color = Color.clear;

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
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene(GetComponent<CinematicBase>().next_level);
			GameSave.loaded_save.CompleteLevel(GetComponent<CinematicBase>().Level_id);
			MasterGameController.SaveCurrent();
		}

        if (curr_event != null)
        {
			if (pause_timer > 0)
			{
				pause_timer -= Time.deltaTime;
			}
			else
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
							SceneManager.LoadScene(GetComponent<CinematicBase>().next_level);
							GameSave.loaded_save.CompleteLevel(GetComponent<CinematicBase>().Level_id);
							MasterGameController.SaveCurrent();
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
						// Deactivate the "Next" button
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
			}
        }
	}

    public void ClickContinue()
    {
        // Deactivate the "Next" button
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
public class CharMoveEvent : CinematicEvent
{
    bool completion_wait;
    Vector2 destination;
    CinematicCharAnimator cmd_target;

    public CharMoveEvent(CinematicCharAnimator n_target, Vector2 n_destination, bool wait_for_next = true)
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
} // Implemented

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
} // Implemented

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
} // Implemented

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
} // Implemented

// Activate Object event type
public class ActivateObjEvent : CinematicEvent
{
	ParticleSystem target;

    public ActivateObjEvent(ParticleSystem n_target)
    {
        Event_type = "activate";
		target = n_target;
    }

    public override void Trigger() {
		target.Play(true);
	}

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
} // Implemented

public class CharAnimationTriggerEvent : CinematicEvent
{
	bool completion_wait;
	string trigger_name;
	CinematicCharAnimator cmd_target;

	public CharAnimationTriggerEvent(CinematicCharAnimator n_target, string n_trigger, bool wait_for_next = true)
	{
		Event_type = "charanim_trig";
		cmd_target = n_target;
		trigger_name = n_trigger;
		completion_wait = wait_for_next;
	}

	public override void Trigger()
	{
		// If the target still exists
		if (cmd_target != null)
		{
			// Send move to character
			cmd_target.TriggerAnimation(trigger_name);
		}
	}

	public override bool IsComplete()
	{
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
} // Implemented on control

public class CharAnimationToggleEvent : CinematicEvent
{
	bool completion_wait;
	string trigger_name;
	CinematicCharAnimator cmd_target;

	public CharAnimationToggleEvent(CinematicCharAnimator n_target, string n_trigger, bool wait_for_next = true)
	{
		Event_type = "charanim_trig";
		cmd_target = n_target;
		trigger_name = n_trigger;
		completion_wait = wait_for_next;
	}

	public override void Trigger()
	{
		// If the target still exists
		if (cmd_target != null)
		{
			// Send move to character
			cmd_target.ToggleAnimation(trigger_name);
		}
	}

	public override bool IsComplete()
	{
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
} // Implemented on control

public class CharRotateEvent : CinematicEvent
{
	bool completion_wait;
	Vector2 destination;
	CinematicCharAnimator cmd_target;

	public CharRotateEvent(CinematicCharAnimator n_target, Vector2 n_destination, bool wait_for_next = true)
	{
		Event_type = "charrota";
		cmd_target = n_target;
		destination = n_destination;
		completion_wait = wait_for_next;
	}

	public override void Trigger()
	{
		// If the target still exists
		if (cmd_target != null)
		{
			// Send move to character
			cmd_target.StartRotate((int)destination.x, (int)destination.y);

		}
	}

	public override bool IsComplete()
	{
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
} // Implemented

public class CharSpawnEvent : CinematicEvent
{
	bool completion_wait;
	Vector2 destination;
	CinematicCharAnimator cmd_target;

	public CharSpawnEvent(CinematicCharAnimator n_target, Vector2 n_destination)
	{
		Event_type = "charspawn";
		cmd_target = n_target;
		destination = n_destination;
	}

	public override void Trigger() {
		// If the target still exists
		if (cmd_target != null)
		{
			// Send move to character
			cmd_target.MoveTo((int)destination.x, (int)destination.y);
		}
	}

	public override bool IsComplete() {
		return true;
	}

	public override void Reset()
	{

	}
} // Implemented

public class CharRemoveEvent : CinematicEvent
{
	CinematicCharAnimator cmd_target;

	public CharRemoveEvent(CinematicCharAnimator n_target)
	{
		Event_type = "charremove";
		cmd_target = n_target;
	}

	public override void Trigger() {
		// If the target still exists
		if (cmd_target != null)
		{
			// Send move to character
			cmd_target.Despawn();
		}
	}

	public override bool IsComplete()
	{
		return true;
	}


	public override void Reset()
	{

	}
} // Implemented

public class PropSpawnEvent : CinematicEvent
{

	CinematicPropControl target;
	Vector3 destination;

	public PropSpawnEvent(CinematicPropControl n_target, Vector3 n_destination)
	{
		Event_type = "propspawn";
		target = n_target;
		destination = n_destination;
	}

	public override void Trigger() {
		target.MoveTo(destination);
	}

	//public override bool IsComplete() { }

	public override void Reset()
	{

	}
} // Implemented

public class PropRemoveEvent : CinematicEvent
{
	CinematicPropControl target;

	public PropRemoveEvent(CinematicPropControl n_target)
	{
		Event_type = "propremove";
		target = n_target;
	}

	public override void Trigger() {
		target.Despawn();
	}

	//public override bool IsComplete() { }

	public override void Reset()
	{

	}
} // Implemented

public class TimePauseEvent : CinematicEvent
{
	float pause_time;

	public TimePauseEvent(float n_time)
	{
		Event_type = "timepause";
		pause_time = n_time;
	}

	public override void Trigger() {
		GameObject.Find("CinematicController").GetComponent<CinematicController>().pause_timer = pause_time;
	}

	//public override bool IsComplete() { }

	public override void Reset()
	{

	}
} // Implemented

public class ShowImageEvent : CinematicEvent
{
	Sprite event_image;

	public ShowImageEvent(Sprite n_image)
	{
		Event_type = "showimage";
		event_image = n_image;
	}

	public override void Trigger() {
		GameObject.Find("CinematicController").GetComponent<CinematicController>().display_image.GetComponent<Image>().color = Color.white;
		GameObject.Find("CinematicController").GetComponent<CinematicController>().display_image.GetComponent<Image>().sprite = event_image;
	}

	//public override bool IsComplete() { }

	public override void Reset()
	{

	}
} // Implemented

public class HideImageEvent : CinematicEvent
{
	public HideImageEvent()
	{
		Event_type = "hideimage";
	}

	public override void Trigger() {
		GameObject.Find("CinematicController").GetComponent<CinematicController>().display_image.GetComponent<Image>().color = Color.clear;
	}

	//public override bool IsComplete() { }

	public override void Reset()
	{

	}
} // Implemented
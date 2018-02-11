using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour {

    Queue<CinematicEvent> cinematic_queue;
    CinematicEvent curr_event;
    float timer;

	// Use this for initialization
	void Start () {
        // Initialize the list of events
        cinematic_queue = new Queue<CinematicEvent>();

        // Get the first event
        if (cinematic_queue.Count > 0)
        {
            curr_event = cinematic_queue.Dequeue();
            curr_event.Trigger();
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
                        curr_event = cinematic_queue.Dequeue();
                        curr_event.Trigger();
                    }
                }
            }
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
        Debug.Log("Called from event parent");
        return true;
    }

    public virtual void Reset() { }
}

// Character animation event type
public class CharAnimationEvent : CinematicEvent
{
    public CharAnimationEvent()
    {
        Event_type = "character";
    }

    public override void Trigger() { }

    //public override bool IsComplete() { }

    public override void Reset()
    {
        
    }
}

// Dialogue box event type
public class DialogueEvent : CinematicEvent
{
    public DialogueEvent()
    {
        Event_type = "dialogue";
    }

    public override void Trigger() { }

    //public override bool IsComplete() { }

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
    public CameraViewEvent()
    {
        Event_type = "camera";
    }

    public override void Trigger() { }

    //public override bool IsComplete() { }

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
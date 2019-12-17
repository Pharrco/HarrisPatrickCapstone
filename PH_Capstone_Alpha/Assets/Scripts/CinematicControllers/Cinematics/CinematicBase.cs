using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBase : MonoBehaviour {

    [SerializeField]
    public string next_level;

    public int[,] Level_array { get; protected set; }

	public int[,] Style_array { get; protected set; }

	public Queue<CinematicEvent> Cinematic_event_queue { get; protected set; }

	public int Level_id { get; protected set; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{

    public int[,] Level_array { get; protected set; }
    public List<Vector2> Marker_list { get; protected set; }
    public Vector2 Player_start { get; protected set; }
    public int Facing_start { get; protected set; }

}
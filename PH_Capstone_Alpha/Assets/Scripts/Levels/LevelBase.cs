using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBase : MonoBehaviour
{

    [SerializeField]
    public string next_level;

    public int[,] Level_array { get; protected set; }
    public List<Vector2> Marker_list { get; protected set; }
    public Vector2 Player_start { get; protected set; }
    public int Facing_start { get; protected set; }
    public int Level_id { get; protected set; }
    public List<InanimateSpawn> Prop_list { get; protected set; }

}

public class InanimateSpawn
{
    public GameObject Spawn_prefab { get; private set; }
    public int X_coord { get; private set; }
    public int Y_coord { get; private set; }
    public float H_offset { get; private set; }

    public InanimateSpawn(int n_x_coord, int n_y_coord, GameObject n_prefab, float n_offset = 0)
    {
        X_coord = n_x_coord;
        Y_coord = n_y_coord;
        Spawn_prefab = n_prefab;
        H_offset = n_offset;
    }
}
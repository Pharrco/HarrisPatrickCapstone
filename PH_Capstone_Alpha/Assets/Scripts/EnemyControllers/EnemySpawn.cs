using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn {

    int spawn_coord_x, spawn_coord_y;
    GameObject spawn_prefab;
    Vector3 spawn_offset;
    int rotation;

    public EnemySpawn(int n_spawn_coord_x, int n_spawn_coord_y, GameObject n_spawn_prefab, Vector3 n_spawn_offset, int n_rotation)
    {
        spawn_coord_x = n_spawn_coord_x;
        spawn_coord_y = n_spawn_coord_y;
        spawn_prefab = n_spawn_prefab;
        spawn_offset = n_spawn_offset;
        rotation = n_rotation;
    }

    public GameObject TriggerSpawn()
    {
        GameObject n_enemy = GameObject.Instantiate(spawn_prefab, new Vector3( spawn_offset.x + (spawn_coord_x - (BuildBoard.GetArrayHeight() / 2)) * 5, spawn_offset.y + BuildBoard.GetArrayValue(spawn_coord_x, spawn_coord_y) - 1f, spawn_offset.z + (spawn_coord_y - (BuildBoard.GetArrayWidth() / 2)) * 5), Quaternion.Euler(0, rotation, 0));
        n_enemy.GetComponent<BaseEnemyController>().SetEnemyPosition(spawn_coord_x, spawn_coord_y);
        return n_enemy;
    }

    public int GetX()
    {
        return spawn_coord_x;
    }

    public int GetY()
    {
        return spawn_coord_y;
    }
}

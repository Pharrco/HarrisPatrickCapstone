using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyController : MonoBehaviour {

    public int Enemy_Pos_X { get; protected set; }
    public int Enemy_Pos_Y { get; protected set; }
    public bool MoveComplete { get; protected set; }

    public virtual void PlayerOn() { }

    public virtual void LightEffect(LightStatus n_lightColor) { }

    public virtual void GetMove() { }

    public void SetEnemyPosition(int n_coord_x, int n_coord_y)
    {
        Enemy_Pos_X = n_coord_x;
        Enemy_Pos_Y = n_coord_y;
    }
}

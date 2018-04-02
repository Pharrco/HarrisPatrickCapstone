using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAllyController : MonoBehaviour {

	public int Ally_Pos_X { get; protected set; }
	public int Ally_Pos_Y { get; protected set; }
	public bool MoveComplete { get; protected set; }
	public virtual void PlayerOn() { }

	public virtual void LightEffect(LightStatus n_lightColor) { }

	public virtual void GetMove() { }

	public void SetAllyPosition(int n_coord_x, int n_coord_y)
	{
		Ally_Pos_X = n_coord_x;
		Ally_Pos_Y = n_coord_y;
	}
}

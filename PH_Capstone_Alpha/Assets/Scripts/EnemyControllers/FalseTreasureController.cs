using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseTreasureController : BaseEnemyController
{
    private enum BehaviorState { Idle, Flee, Attack, Stunned, Pass, Die }

    BehaviorState curr_state = BehaviorState.Idle;

    public override void PlayerOn()
    {
        EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);
    }

    public override void GetMove()
    {

    }

    public override void LightEffect(LightStatus n_lightColor)
    {
        switch (n_lightColor)
        {
            case LightStatus.White:
                curr_state = BehaviorState.Flee;
                GetComponent<Animator>().SetBool("Rest", false);
                break;
            case LightStatus.Infrd:
                curr_state = BehaviorState.Attack;
                break;
            case LightStatus.Ulvlt:
                curr_state = BehaviorState.Stunned;
                break;
            case LightStatus.Nopwr:
                curr_state = BehaviorState.Idle;
                GetComponent<Animator>().SetBool("Rest", true);
                break;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueTreasureController : BaseEnemyController {

    public override void PlayerOn()
    {
        EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);
    }

    public override void LightEffect(LightStatus n_lightColor) {
        switch (n_lightColor)
        {
            case LightStatus.White:
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case LightStatus.Infrd:

                break;
            case LightStatus.Ulvlt:

                break;
            case LightStatus.Nopwr:
                GetComponent<Animator>().SetBool("Open", false);
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueTreasureController : BaseEnemyController {

    public override void PlayerOn()
    {
        EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);
        PhaseController.SetPlayerPause();
        CashControl.GenerateRandomCash(50, 100);
        GameObject.Find("MessagePanel").GetComponent<Transform>().Find("Panel/NewCashText").GetComponent<Text>().text = "$" + CashControl.GetNewCash().ToString();
    }

    public override void GetMove() { }

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

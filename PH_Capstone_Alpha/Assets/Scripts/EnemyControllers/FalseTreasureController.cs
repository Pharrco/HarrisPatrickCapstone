using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FalseTreasureController : BaseEnemyController
{
    private enum BehaviorState { Idle, Flee, Attack, Stunned, Pass, Die }
    private List<Vector2> avail_moves;
    Vector2 move_destination;

    BehaviorState curr_state = BehaviorState.Idle;

    private void Start()
    {
        MoveComplete = true;

        avail_moves = new List<Vector2>() { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };

        move_destination = new Vector2(-1, -1);
    }

    public override void PlayerOn()
    {
        EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);
        PhaseController.SetPlayerPause();
        CashControl.GenerateRandomCash(100, 150);
        GameObject.Find("MessagePanel").GetComponent<Transform>().Find("Panel/NewCashText").GetComponent<Text>().text = "$" + CashControl.GetNewCash().ToString();
    }

    public override void GetMove()
    {
        if (curr_state == BehaviorState.Flee)
        {
            Vector2 best_move = new Vector2(-1, -1);

            foreach (Vector2 move_coord in avail_moves)
            {
                int try_x = Enemy_Pos_X + (int)move_coord.x;
                int try_y = Enemy_Pos_Y + (int)move_coord.y;

                // If targeted space is within the limits of the board
                if ((try_x >= 0) && (try_x < BuildBoard.GetArrayHeight()) && (try_y >= 0) && (try_y < BuildBoard.GetArrayWidth()))
                {
                    // If the difference between the current space and target space is one or zero
                    if ((Mathf.Abs(BuildBoard.GetArrayValue(Enemy_Pos_X, Enemy_Pos_Y) - BuildBoard.GetArrayValue(try_x, try_y)) < 1.5) && (BuildBoard.GetArrayValue(try_x, try_y) != 0))
                    {
                        // If the light status in the target space is not white or ultraviolet
                        if ((LightEffectControl.GetLightPoint(try_x, try_y) != LightStatus.White) && (LightEffectControl.GetLightPoint(try_x, try_y) != LightStatus.Ulvlt))
                        {
                            // If the space is not occupied by another enemy
                            if (!EnemyGridControl.IsEnemyOccupied(try_x, try_y))
                            {
                                // If the space is not occupied by the player
                                if ((PlayerLocator.Player_Pos_X != try_x) || (PlayerLocator.Player_Pos_Y != try_y))
                                {
                                    // If the current best move has not been set
                                    if (best_move == new Vector2(-1, -1))
                                    {
                                        best_move = new Vector2(try_x, try_y);

                                        Debug.Log("(" + try_x + ", " + try_y + ") is an acceptable move");
                                    }
                                    // If the current best move has been set
                                    else
                                    {
                                        // Randomly
                                        if (Random.Range((int)0, (int)99) % 2 == 0)
                                        {
                                            best_move = new Vector2(try_x, try_y);
                                            Debug.Log("(" + try_x + ", " + try_y + ") is an acceptable move");
                                        }
                                        else
                                        {
                                            Debug.Log("(" + try_x + ", " + try_y + ") is an acceptable move, but is being randomly ignored");
                                        }
                                    }
                                }
                                else
                                {
                                    Debug.Log("(" + try_x + ", " + try_y + ") is occupied by player");
                                }
                            }
                            else
                            {
                                Debug.Log("(" + try_x + ", " + try_y + ") is occupied by another enemy");
                            }
                        }
                        else
                        {
                            Debug.Log("(" + try_x + ", " + try_y + ") is lit");
                        }
                    }
                    else
                    {
                        Debug.Log("(" + try_x + ", " + try_y + ") is too great a height difference");
                    }
                }
                else
                {
                    Debug.Log("(" + try_x + ", " + try_y + ") is out of range");
                }
            }

            MoveComplete = false;

            if (best_move == new Vector2(-1, -1))
            {
                curr_state = BehaviorState.Pass;
                MoveComplete = true;
                Debug.Log(name + " Passes: No move available");
            }
            else
            {
                move_destination = best_move;
                float n_rotation = Mathf.Atan2(move_destination.x - Enemy_Pos_X, move_destination.y - Enemy_Pos_Y) * Mathf.Rad2Deg;
                EnemyGridControl.SwapEnemyPoints(Enemy_Pos_X, Enemy_Pos_Y, (int)move_destination.x, (int)move_destination.y);
                Debug.Log(name + " moves to (" + (int)move_destination.x + ", " + (int)move_destination.y + ")");
                transform.rotation = Quaternion.Euler(0, n_rotation, 0);
            }
        }
    }

    public void Update()
    {
        if (PhaseController.GetCurrPhase() == GamePhase.EnemyAnimation)
        {
            if ((curr_state == BehaviorState.Flee) && (!MoveComplete))
            {
                transform.position = new Vector3(((int)move_destination.x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue((int)move_destination.x, (int)move_destination.y) - 1f, ((int)move_destination.y - (BuildBoard.GetArrayWidth() / 2)) * 5);
                MoveComplete = true;
            }
        }
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


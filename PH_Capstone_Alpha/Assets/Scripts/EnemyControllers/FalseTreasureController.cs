using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FalseTreasureController : BaseEnemyController
{
    private enum BehaviorState { Idle, Flee, Attack, Stunned, Pass, Die }
    private List<Vector2> avail_moves;
    Vector2 move_destination;
    Vector3 movePoint_origin, movePoint_target, movePoint_temp;
    float progress = 0;
    [SerializeField]
    float move_speed, error_dist;
    Quaternion curr_rotation;
    Queue<Vector3> move_queue;

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

                movePoint_target = new Vector3((move_destination.x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue((int)move_destination.x, (int)move_destination.y) - 1f, (move_destination.y - (BuildBoard.GetArrayWidth() / 2)) * 5);

                move_queue = GenerateMoveQueue(transform.position, movePoint_target);

                movePoint_origin = move_queue.Dequeue();
                movePoint_temp = move_queue.Dequeue();

                // Start run animation
                GetComponent<Animator>().SetBool("Run", true);
            }
        }
    }

    public void Update()
    {
        if (PhaseController.GetCurrPhase() == GamePhase.EnemyAnimation)
        {
            if ((curr_state == BehaviorState.Flee) && (!MoveComplete))
            {
                // Update progress
                progress += move_speed * Time.deltaTime;

                // Lerp towards temp target
                transform.position = Vector3.Lerp(movePoint_origin, movePoint_temp, progress);

                // If within error distance of temp target
                if (Vector3.Distance(transform.position, movePoint_temp) < error_dist)
                {
                    // Move to temp target
                    transform.position = movePoint_temp;

                    // If an item remains in the move queue
                    if (move_queue.Count > 0)
                    {
                        // The old destination is the new origin
                        movePoint_origin = movePoint_temp;

                        // The new destination is the next point in the queue
                        movePoint_temp = move_queue.Dequeue();

                        // Reset progress
                        progress = 0;
                    }
                    else
                    {
                        // Move to the final point
                        transform.position = movePoint_target;

                        // Movement is complete
                        MoveComplete = true;

                        // End run animation
                        GetComponent<Animator>().SetBool("Run", false);
                    }
                }
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

    private Queue<Vector3> GenerateMoveQueue(Vector3 move_origin, Vector3 move_target)
    {
        Queue<Vector3> n_move_queue = new Queue<Vector3>();

        float x_diff = move_target.x - move_origin.x;
        float z_diff = move_target.z - move_origin.z;

        n_move_queue.Enqueue(move_origin);
        n_move_queue.Enqueue(new Vector3(move_origin.x + (x_diff / 3.0f), move_origin.y, move_origin.z + (z_diff / 3.0f)));
        n_move_queue.Enqueue(new Vector3(move_origin.x + (2.0f * x_diff / 3.0f), move_target.y, move_origin.z + (2.0f * z_diff / 3.0f)));
        n_move_queue.Enqueue(move_target);

        return n_move_queue;
    }
}


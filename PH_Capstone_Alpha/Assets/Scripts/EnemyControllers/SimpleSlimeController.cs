using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSlimeController : BaseEnemyController
{
	private enum BehaviorState { Idle, SeedBlind, SeedVisible, Aggro, Stunned, Die }
	private List<Vector2> avail_moves;
	Vector2 move_destination;
	Vector3 movePoint_origin, movePoint_target, movePoint_temp;
	float progress = 0;
	[SerializeField]
	float move_speed, error_dist;
	Queue<Vector3> move_queue;
	float die_timer = 100;
	public SlimeSpawner spawn_source;

	List<GameObject> target_list;
	List<GameObject> projectile_list;
	List<Vector2> avail_targets;

	[SerializeField]
	GameObject projectile_prefab;

	BehaviorState curr_state = BehaviorState.SeedVisible;

	private void Start()
	{
		MoveComplete = true;

		avail_moves = new List<Vector2>() { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) };

		move_destination = new Vector2(-1, -1);

		target_list = new List<GameObject>();
		projectile_list = new List<GameObject>();

		avail_targets = new List<Vector2>()
		{
			new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0),
			new Vector2(1, 1), new Vector2(-1, -1), new Vector2(1, -1), new Vector2(-1, 1),
			new Vector2(0, 2), new Vector2(0, -2), new Vector2(2, 0), new Vector2(-2, 0)
		};
	}

	public override void PlayerOn()
	{
		EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);

		if (spawn_source != null)
		{
			spawn_source.UpdateTimer(3);
		}
	}

	public override void GetMove()
	{
		// If currently fleeing the player
		if (curr_state == BehaviorState.SeedVisible)
		{
			// Set invalid move as default
			Vector2 best_move = new Vector2(-1, -1);

			// For each available move
			foreach (Vector2 move_coord in avail_moves)
			{
				// Get the targeted board coordinate that the move would end at
				int try_x = Enemy_Pos_X + (int)move_coord.x;
				int try_y = Enemy_Pos_Y + (int)move_coord.y;

				// If targeted space is within the limits of the board
				if ((try_x >= 0) && (try_x < BuildBoard.GetArrayHeight()) && (try_y >= 0) && (try_y < BuildBoard.GetArrayWidth()))
				{
					// If the difference between the current space and target space is one or zero
					if ((Mathf.Abs(BuildBoard.GetArrayValue(Enemy_Pos_X, Enemy_Pos_Y) - BuildBoard.GetArrayValue(try_x, try_y)) < 1.5) && (BuildBoard.GetArrayValue(try_x, try_y) != 0))
					{
						// If the space is not occupied by another enemy
						if (!EnemyGridControl.IsEnemyOccupied(try_x, try_y))
						{
							// If the space is not occupied by an environmental obstacle
							if (!EnvironmentController.IsOccupied(try_x, try_y))
							{
								// If the space is not occupied by the player
								if ((PlayerLocator.Player_Pos_X != try_x) || (PlayerLocator.Player_Pos_Y != try_y))
								{
									// If the space is not occupied by a marker
									if (!MarkerControl.IsMarkerOccupied(try_x, try_y))
									{
										// If the current best move has not been set
										if (best_move == new Vector2(-1, -1))
										{
											// This is the new best move
											best_move = new Vector2(try_x, try_y);

											Debug.Log("(" + try_x + ", " + try_y + ") is an acceptable move");
										}
										// If the current best move has been set
										else
										{
											// Randomly...
											if (Random.Range((int)0, (int)99) % 2 == 0)
											{
												// Accept the new move
												best_move = new Vector2(try_x, try_y);
												Debug.Log("(" + try_x + ", " + try_y + ") is an acceptable move");
											}
											else
											{
												// Reject the new move
												Debug.Log("(" + try_x + ", " + try_y + ") is an acceptable move, but is being randomly ignored");
											}
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
								Debug.Log("(" + try_x + ", " + try_y + ") is occupied by an environment obstacle");
							}
						}
						else
						{
							Debug.Log("(" + try_x + ", " + try_y + ") is occupied by another enemy");
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

			// Set moving for animation phase
			MoveComplete = false;

			// If the move was never changed from the default
			if (best_move == new Vector2(-1, -1))
			{
				// Pass, no moves are available
				curr_state = BehaviorState.Die;

				// No animation, movement complete
				MoveComplete = true;

				Debug.Log(name + " Dies: No move available");

				// Start the dying animation
				GetComponent<Animator>().SetTrigger("Die");

				// Set the timer
				die_timer = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

				if (spawn_source != null)
				{
					spawn_source.UpdateTimer(3);
				}
			}
			else
			{
				// Set final destination
				move_destination = best_move;

				// Get the rotation to next move
				float n_rotation = Mathf.Atan2(move_destination.x - Enemy_Pos_X, move_destination.y - Enemy_Pos_Y) * Mathf.Rad2Deg;

				// Move the enemy object in the enemy grid
				EnemyGridControl.SwapEnemyPoints(Enemy_Pos_X, Enemy_Pos_Y, (int)move_destination.x, (int)move_destination.y);

				Debug.Log(name + " moves to (" + (int)move_destination.x + ", " + (int)move_destination.y + ")");

				// Rotate the enemy object before moving
				transform.rotation = Quaternion.Euler(0, n_rotation, 0);

				// Store destination vector
				movePoint_target = new Vector3((move_destination.x - (BuildBoard.GetArrayHeight() / 2)) * 5, BuildBoard.GetArrayValue((int)move_destination.x, (int)move_destination.y) - 1f, (move_destination.y - (BuildBoard.GetArrayWidth() / 2)) * 5);

				// Generate the queue of animation targets
				move_queue = GenerateMoveQueue(transform.position, movePoint_target);

				// Get the first origin and destination
				movePoint_origin = move_queue.Dequeue();
				movePoint_temp = move_queue.Dequeue();

				// Start run animation
				GetComponent<Animator>().SetBool("Move Forward", true);
			}
		}

		// If currently attacking after being surprised
		if (curr_state == BehaviorState.Aggro)
		{
			// Clear the target list
			target_list.Clear();

			// For each available attack
			foreach (Vector2 attack_coord in avail_targets)
			{
				// Get the targeted board coordinate that the attack would end at
				int try_x = Enemy_Pos_X + (int)attack_coord.x;
				int try_y = Enemy_Pos_Y + (int)attack_coord.y;

				// If targeted space is within the limits of the board
				if ((try_x >= 0) && (try_x < BuildBoard.GetArrayHeight()) && (try_y >= 0) && (try_y < BuildBoard.GetArrayWidth()))
				{
					// If the player exists in the space
					if ((PlayerLocator.Player_Pos_X == try_x) && (PlayerLocator.Player_Pos_Y == try_y))
					{
						// Add the player to the target list
						target_list.Add(GameObject.FindGameObjectWithTag("Player"));
					}

					// If an ally object is present in the space
					if (AllyGridControl.IsAllyOccupied(try_x, try_y))
					{
						// Add the ally to the target list
						target_list.Add(AllyGridControl.GetAllyFromGrid(try_x, try_y));
					}
				}
			}

			// Set moving for animation phase
			MoveComplete = false;

			// If the move was never changed from the default
			if (target_list.Count <= 0)
			{
				// Pass, no moves are available
				curr_state = BehaviorState.Idle;

				// No animation, movement complete
				MoveComplete = true;

				Debug.Log(name + " Passes on attack: No attacks available");
			}
			else
			{
				// Clear the projectile list
				projectile_list.Clear();

				// For each target in the target list
				foreach (GameObject target in target_list)
				{
					// Create a projectile
					GameObject projectile = GameObject.Instantiate(projectile_prefab);
					projectile.GetComponent<ChestProjectile>().Initialize(transform.position + Vector3.up, target.transform.position + Vector3.up);

					// Get the rotation to next move
					float n_rotation = Mathf.Atan2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler(0, n_rotation, 0);

					// Start shoot animation
					GetComponent<Animator>().SetTrigger("Cast Spell");

					// Add the projectile to the projectile list
					projectile_list.Add(projectile);
				}
			}
		}
	}

	public void Update()
	{
		if (die_timer < 0) // Timer run out
		{
			EnemyGridControl.DestroyEnemyAt(Enemy_Pos_X, Enemy_Pos_Y);
		}
		else if (die_timer != 100) // Timer running
		{
			die_timer -= Time.deltaTime;
		}

		// If the current phase is the enemy animation phase
		if (PhaseController.GetCurrPhase() == GamePhase.EnemyAnimation)
		{
			// If the enemy is fleeing and movement is not complete
			if ((curr_state == BehaviorState.SeedVisible) && (!MoveComplete))
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
						GetComponent<Animator>().SetBool("Move Forward", false);

						MarkerControl.PlaceSimpleMarker(Enemy_Pos_X, Enemy_Pos_Y);
					}
				}
			}

			// If the enemy is attacking and movement is not complete
			if ((curr_state == BehaviorState.Aggro) && (!MoveComplete))
			{
				// If the only remaining objects in the list are null
				if (projectile_list.Count == projectile_list.FindAll(el => el == null).Count)
				{
					// Move is complete
					MoveComplete = true;

					// For each target in the list
					foreach (GameObject target in target_list)
					{
						// If the target is the player
						if (target.tag == "Player")
						{
							target.GetComponent<Animator>().SetTrigger("Take Damage");
							PlayerHealth.PlayerTakeDamage();
						}
						// If the target is an ally
						else
						{

						}
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
				GetComponent<Animator>().SetBool("Defend", false);
				curr_state = BehaviorState.SeedVisible;
				break;
			case LightStatus.Infrd:
				curr_state = BehaviorState.Stunned;
				GetComponent<Animator>().SetTrigger("Take Damage");
				GetComponent<Animator>().SetBool("Defend", true);
				break;
			case LightStatus.Ulvlt:
				GetComponent<Animator>().SetBool("Defend", false);
				curr_state = BehaviorState.Aggro;
				break;
			case LightStatus.Nopwr:
				GetComponent<Animator>().SetBool("Defend", false);
				curr_state = BehaviorState.SeedVisible;
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

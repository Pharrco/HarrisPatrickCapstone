using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicCameraControl : MonoBehaviour {

    static GameObject camera_main;
    static GameObject focus_target;
    static Vector3 current_focus, target_focus, current_view, target_view;
    static float rotation_status = 0;
    static float rotation_duration;
    static bool on_target;
    static bool focus_changing, view_changing;
    static float rotation;
    [SerializeField]
    GameObject WallN, WallE, WallS, WallW;

    public static bool IsComplete { get; private set; }

    // Use this for initialization
    void Awake () {
        // Get the main camera
        camera_main = Camera.main.gameObject;

        IsComplete = true;
    }
	
	// Update is called once per frame
	void Update () {
        // If the movement is not complete
		if (!IsComplete)
        {
            // Update rotation status
            rotation_status = Mathf.Min(1f, rotation_status + (Time.deltaTime / rotation_duration));

            if (rotation_status == 1)
            {
                // Set view to final
                current_view = target_view;

                // Position the camera
                camera_main.transform.position = new Vector3(focus_target.transform.position.x + (10 - current_view.y - current_view.z) * Mathf.Sin(current_view.x * Mathf.Deg2Rad), focus_target.transform.position.y + 6 + current_view.y - current_view.z, focus_target.transform.position.z + (10 - current_view.y - current_view.z) * Mathf.Cos(current_view.x * Mathf.Deg2Rad));

                // Look at the focus target
                camera_main.transform.LookAt(focus_target.transform.position);

                // The movement is complete
                IsComplete = true;
            }
            else
            {
                // If the focus is changing
                if (focus_changing)
                {
                    // Update the temporary focus by lerping between current and target
                    Vector3 temp_focus = Vector3.Lerp(current_focus, target_focus, rotation_status);

                    // If the view is changing
                    if (view_changing)
                    {
                        // Update the temporary view by lerping between current and target
                        Vector3 temp_view = new Vector3( Mathf.LerpAngle(current_view.x, target_view.x, rotation_status), Mathf.Lerp(current_view.y, target_view.y, rotation_status), Mathf.Lerp(current_view.z, target_view.z, rotation_status));

                        // Set the camera to the temp view
                        camera_main.transform.position = new Vector3(temp_focus.x + (10 - temp_view.y - temp_view.z) * Mathf.Sin(temp_view.x * Mathf.Deg2Rad), temp_focus.y + 6 + temp_view.y - temp_view.z, temp_focus.z + (10 - temp_view.y - temp_view.z) * Mathf.Cos(temp_view.x * Mathf.Deg2Rad));

                        rotation = temp_view.x;
                    }
                    else // If view is not changing
                    {
                        // Position the camera
                        camera_main.transform.position = new Vector3(temp_focus.x + (10 - current_view.y - current_view.z) * Mathf.Sin(current_view.x * Mathf.Deg2Rad), temp_focus.y + 6 + current_view.y - current_view.z, temp_focus.z + (10 - current_view.y - current_view.z) * Mathf.Cos(current_view.x * Mathf.Deg2Rad));

                        rotation = current_view.x;
                    }

                    // Look at the focus
                    camera_main.transform.LookAt(temp_focus);
                }
                else // If focus is not changing
                {
                    // If the view is changing
                    if (view_changing)
                    {
                        // Update the temporary view by lerping between current and target
                        Vector3 temp_view = new Vector3(Mathf.LerpAngle(current_view.x, target_view.x, rotation_status), Mathf.Lerp(current_view.y, target_view.y, rotation_status), Mathf.Lerp(current_view.z, target_view.z, rotation_status));

                        // Set the camera to the temp view
                        camera_main.transform.position = new Vector3(focus_target.transform.position.x + (10 - temp_view.y - temp_view.z) * Mathf.Sin(temp_view.x * Mathf.Deg2Rad), focus_target.transform.position.y + 6 + temp_view.y - temp_view.z, focus_target.transform.position.z + (10 - temp_view.y - temp_view.z) * Mathf.Cos(temp_view.x * Mathf.Deg2Rad));

                        rotation = temp_view.x;
                    }
                    else // If view is not changing
                    {
                        // Position the camera
                        camera_main.transform.position = new Vector3(focus_target.transform.position.x + (10 - current_view.y - current_view.z) * Mathf.Sin(current_view.x * Mathf.Deg2Rad), focus_target.transform.position.y + 6 + current_view.y - current_view.z, focus_target.transform.position.z + (10 - current_view.y - current_view.z) * Mathf.Cos(current_view.x * Mathf.Deg2Rad));

                        rotation = current_view.x;
                    }

                    // Look at the target
                    camera_main.transform.LookAt(focus_target.transform.position);
                }
            }
        }
        else
        {
            if (focus_target != null)
            {
                // Position the camera
                camera_main.transform.position = new Vector3(focus_target.transform.position.x + (10 - current_view.y - current_view.z) * Mathf.Sin(current_view.x * Mathf.Deg2Rad), focus_target.transform.position.y + 6 + current_view.y - current_view.z, focus_target.transform.position.z + (10 - current_view.y - current_view.z) * Mathf.Cos(current_view.x * Mathf.Deg2Rad));

                rotation = current_view.x;

                // Look at the focus target
                camera_main.transform.LookAt(focus_target.transform.position);
            }
        }

        // Set South wall based on visibility
        if ((rotation < 320) && (rotation > 210))
        {
            WallS.SetActive(false);
        }
        else
        {
            WallS.SetActive(true);
        }

        // Set North wall based on visibility
        if ((rotation < 140) && (rotation > 30))
        {
            WallN.SetActive(false);
        }
        else
        {
            WallN.SetActive(true);
        }

        // Set East wall based on visibility
        if ((rotation < 230) && (rotation > 120))
        {
            WallE.SetActive(false);
        }
        else
        {
            WallE.SetActive(true);
        }

        // Set East wall based on visibility
        if ((rotation < 50) || (rotation > 300))
        {
            WallW.SetActive(false);
        }
        else
        {
            WallW.SetActive(true);
        }
    }

    public static void SetCameraView(GameObject n_target, Vector3 n_view, float duration = 0)
    {
        // The movement is not complete
        IsComplete = false;

        // If this is to be an instantaneous view change
        if (duration == 0)
        {
            // Set the new focus and view
            focus_target = n_target;
            current_view = new Vector3(Mathf.Clamp(n_view.x, 0, 360), Mathf.Clamp(n_view.y, 0, 6), Mathf.Clamp(n_view.z, 0, 3));

            // If the current focus target is set
            if ((focus_target != null) && (focus_target.transform != null))
            {
                // Position the camera
                camera_main.transform.position = new Vector3(focus_target.transform.position.x + (10 - current_view.y - current_view.z) * Mathf.Sin(current_view.x * Mathf.Deg2Rad), focus_target.transform.position.y + 6 + current_view.y - current_view.z, focus_target.transform.position.z + (10 - current_view.y - current_view.z) * Mathf.Cos(current_view.x * Mathf.Deg2Rad));

                // Look at the focus target
                camera_main.transform.LookAt(focus_target.transform.position);

                rotation = current_view.x;

                // The movement is complete
                IsComplete = true;
            }
        }
        else // If this change is going to be interpolated over time
        {
            // Reset rotation status and set the rotation duration
            rotation_status = 0;
            rotation_duration = duration;

            // If the target has changed
            if (n_target != focus_target)
            {
                // The focus is changing
                focus_changing = true;

                // The current focus is the position of the current target
                current_focus = focus_target.transform.position;

                // The target focus is the position of the new target
                target_focus = n_target.transform.position;

                // Set the new focus target
                focus_target = n_target;
            }

            // If the view has changed
            if (current_view != new Vector3(Mathf.Clamp(n_view.x, 0, 360), Mathf.Clamp(n_view.y, 0, 6), Mathf.Clamp(n_view.z, 0, 3)))
            {
                // The view is changing
                view_changing = true;

                // Set target view
                target_view = new Vector3(Mathf.Clamp(n_view.x, 0, 360), Mathf.Clamp(n_view.y, 0, 6), Mathf.Clamp(n_view.z, 0, 3));
            }
        }
    }
}

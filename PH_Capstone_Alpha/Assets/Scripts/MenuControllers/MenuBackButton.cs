using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBackButton : MonoBehaviour {

	public void ClickBackToMain()
    {
        SceneManager.LoadScene("Menu_Main");
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.Backspace)))
        {
            SceneManager.LoadScene("Menu_Main");
        }
    }
}

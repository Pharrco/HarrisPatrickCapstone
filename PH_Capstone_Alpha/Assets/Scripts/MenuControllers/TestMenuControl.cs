using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestMenuControl : MonoBehaviour {

	private void Start()
	{
		GameObject.Find("HUD").transform.Find("PlayerCashText").GetComponent<Text>().text = "$" + GameSave.loaded_save.GetPlayerCash().ToString();
	}

	public void StartTestLevel()
    {
        SceneManager.LoadScene("SceneSelect_Area1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeLevel(string n_level)
    {
        SceneManager.LoadScene(n_level);
    }
}

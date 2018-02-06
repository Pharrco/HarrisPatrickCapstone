using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenuControl : MonoBehaviour {

    public void StartTestLevel()
    {
        SceneManager.LoadScene("SceneSelect_Area1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitBtnController : MonoBehaviour {

	// Quit Game
    public void OnClick()
    {
        Application.Quit();
    }
}

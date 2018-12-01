using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting gamee");
    }



	public void LoadLevel(int levelNumber)
    {
        if (SceneManager.GetSceneByBuildIndex(levelNumber) != null)
            SceneManager.LoadScene(levelNumber);
        else
            Debug.Log("no such index in build");
    }
}

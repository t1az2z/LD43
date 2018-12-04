using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;


public class DeathScreen : MonoBehaviour
{

    public void LoadLevel(int levelNumber)
    {
        if (SceneManager.GetSceneByBuildIndex(levelNumber) != null)
            SceneManager.LoadScene(levelNumber);
        else
            Debug.Log("no such index in build");
    }
}

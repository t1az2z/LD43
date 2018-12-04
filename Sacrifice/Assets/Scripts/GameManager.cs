using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public Player player;
    public UIController ui;

    private void Awake()
    {
        SingletonImplementation();
        player = FindObjectOfType<Player>();
        ui = FindObjectOfType<UIController>();
    }

    void Start () {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            AudioManager.instance.Stop("MenuTheme");

	}

    private void SingletonImplementation()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update () {
		if (player == null)
            player = FindObjectOfType<Player>();

        if (player.isDead)
        {
            AudioManager.instance.Stop("ZombieSteps");
            AudioManager.instance.Stop("ZombieHit");
            
        }
        if (ui == null)
            ui = FindObjectOfType<UIController>();
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public IEnumerator FreezeTime(float stopTime)
    {
        Time.timeScale = 0f;
        float stopEndTime = Time.realtimeSinceStartup + stopTime;
        while (Time.realtimeSinceStartup < stopEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public Player player;

    private void Awake()
    {
        SingletonImplementation();
        player = FindObjectOfType<Player>();
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

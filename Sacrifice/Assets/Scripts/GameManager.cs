using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public Player player;

    private void Awake()
    {
        SingletonImplementation();
        player = FindObjectOfType<Player>();
    }

    void Start () {
		
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

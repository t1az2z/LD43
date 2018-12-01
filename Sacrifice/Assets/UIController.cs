using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    GameManager gameManager;
    public Image healthBar;
    public TextMeshProUGUI healthText;
    float maxHealth;
    float health;

	// Use this for initialization
	void Start () {
        maxHealth = GameManager.Instance.player.maxHP;
        health = GameManager.Instance.player.hp;

    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatehealthBar();
        healthText.text = health + "/" + maxHealth;
    }

    private void UpdatehealthBar()
    {
        health = GameManager.Instance.player.hp;
        healthBar.fillAmount = health / maxHealth;
    }
}

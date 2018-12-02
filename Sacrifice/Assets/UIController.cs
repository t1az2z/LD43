using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour {

    public Image healthBar;
    public Image ammoBar;
    public TextMeshProUGUI healthText;
    float maxHealth;
    float health;

    public TextMeshProUGUI ammoText;
    float ammo;
    float maxAmmo;

    public Image weapon;



    // Use this for initialization
    void Start () {
        maxHealth = GameManager.Instance.player.maxHP;
        health = GameManager.Instance.player.hp;

        maxAmmo = GameManager.Instance.player.maxAmmo;
        ammo = GameManager.Instance.player.ammo;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdatehealthBar();
        UpdateAmmo();

        weapon.sprite = GameManager.Instance.player.weapon.GetComponent<SpriteRenderer>().sprite;
    }

    private void UpdateAmmo()
    {
        maxAmmo = GameManager.Instance.player.maxAmmo;
        ammo = GameManager.Instance.player.ammo;
        ammoBar.fillAmount = ammo / maxAmmo;

        ammoText.text = ammo + "/" + maxAmmo;


    }
    private void UpdatehealthBar()
    {
        maxHealth = GameManager.Instance.player.maxHP;
        health = GameManager.Instance.player.hp;
        healthBar.fillAmount = health / maxHealth;

        healthText.text = health + "/" + maxHealth;
    }
}

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

    public CanvasGroup deathScreen;
    public CanvasGroup button;


    public TextMeshProUGUI ammoText;
    float ammo;
    float maxAmmo;

    public Image weapon;
    public Animator textAnim;

    public Animator damage;
    public Animator heal;


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
        if (GameManager.Instance.player.isDead) textAnim.SetBool("ShowTooltip", false);
        weapon.sprite = GameManager.Instance.player.weapon.GetComponent<SpriteRenderer>().sprite;

        if (GameManager.Instance.player.isDead)
        {
            deathScreen.alpha = 1;
            deathScreen.blocksRaycasts = true;
            button.alpha = 1;
            button.blocksRaycasts = true;
        }
        else
        {
            deathScreen.alpha = 0f;
            deathScreen.blocksRaycasts = false;
            button.alpha = 0f;
            button.blocksRaycasts = false;
        }
    }

    public void Restart()
    {
        GameManager.Instance.LoadScene(2);
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

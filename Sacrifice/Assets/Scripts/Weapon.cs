using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public GameObject bulletSpawner;
    public WeaponType weaponType;
    Sprite sprite;
    WeaponType currentWeapon;
    float knockback;
    Bullet bullet;
    float rateOfFire;
	// Use this for initialization

	void Start ()
    {
        InitializeWeapon();
    }


    public void Shoot(Vector2 direction)
    {
        var currentBullet = Instantiate(bullet, bulletSpawner.transform.position, transform.rotation);
        var bSr = currentBullet.gameObject.GetComponent<SpriteRenderer>();
        bSr.sprite = weaponType.bulletSprite;
        var col = currentBullet.gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        var rb = currentBullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * currentWeapon.bulletSpeed;
        currentBullet.damage = weaponType.damage;



        
    }

    private void InitializeWeapon()
    {
        currentWeapon = weaponType;
        sprite = weaponType.sprite;
        knockback = weaponType.knockback;
        bullet = weaponType.bulletType.GetComponent<Bullet>();
    }
}

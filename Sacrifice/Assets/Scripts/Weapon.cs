using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Weapon : MonoBehaviour {

    public GameObject bulletSpawner;
    public WeaponType weaponType;
    SpriteRenderer sr;
    WeaponType currentWeapon;
    public float knockback;
    Bullet bullet;
    float rateOfFire;
    public float dispersion;
    [HideInInspector]public int shootCost;

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
        if (weaponType.destroyProjectileOnCollision)
            sr.sortingOrder += 1;
        var col = currentBullet.gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        var rb = currentBullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * currentWeapon.bulletSpeed;
        currentBullet.damage = weaponType.damage;
        currentBullet.destroyonCollision = weaponType.destroyProjectileOnCollision;

    }

    private void InitializeWeapon()
    {
        sr = GetComponent<SpriteRenderer>();
        currentWeapon = weaponType;
        shootCost = weaponType.shootCost;
        sr.sprite = weaponType.sprite;
        knockback = weaponType.knockback;
        bullet = weaponType.bulletType.GetComponent<Bullet>();
    }
}

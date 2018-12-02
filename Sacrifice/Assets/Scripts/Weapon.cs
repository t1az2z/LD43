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
    string shootSound;
    

	// Use this for initialization

	void Start ()
    {
        InitializeWeapon(weaponType);
    }


    public void Shoot(Vector2 direction)
    {
        var currentBullet = Instantiate(bullet, bulletSpawner.transform.position, transform.rotation);
        var bSr = currentBullet.gameObject.GetComponent<SpriteRenderer>();
        bSr.sprite = currentWeapon.bulletSprite;
        if (currentWeapon.destroyProjectileOnCollision)
            sr.sortingOrder += 1;
        var col = currentBullet.gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        var rb = currentBullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * currentWeapon.bulletSpeed;
        currentBullet.damage = currentWeapon.damage;
        currentBullet.destroyonCollision = currentWeapon.destroyProjectileOnCollision;
        var shotSound = AudioManager.instance.FindClipByName(shootSound);
        shotSound.volume = Random.Range(.8f, 1f);
        shotSound.pitch = Random.Range(.95f, 1f);
        shotSound.Play();
    }

    public void InitializeWeapon(WeaponType wpn)
    {
        sr = GetComponent<SpriteRenderer>();
        currentWeapon = wpn;
        shootCost = wpn.shootCost;
        sr.sprite = wpn.sprite;
        knockback = wpn.knockback;
        bullet = wpn.bulletType.GetComponent<Bullet>();
        shootSound = wpn.shootSoundName;
    }
}

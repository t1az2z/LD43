using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public WeaponType weaponType;
    Sprite sprite;
    private WeaponType currentWeapon;
    float knockback;
    Bullet bullet;
    float rateOfFire;
	// Use this for initialization

	void Start ()
    {
        InitializeWeapon();
    }


    public void Shoot()
    {
        Debug.Log("Weapon " + weaponType + ". Bullet " + bullet + ". Knockback " + knockback);
    }

    private void InitializeWeapon()
    {
        currentWeapon = weaponType;
        sprite = weaponType.sprite;
        knockback = weaponType.knockback;
        bullet = weaponType.bulletType;
    }
}

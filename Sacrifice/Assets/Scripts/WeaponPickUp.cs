using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

    public WeaponType[] weapons;
    public WeaponType weapon;
    SpriteRenderer sr;

    private void Awake()
    {
        weapon = weapons[Random.Range(0, weapons.Length)];
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = weapon.sprite;
    }

 
}

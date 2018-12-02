using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour {

    WeaponType[] weapons;
    WeaponType weapon;

    private void Awake()
    {
        weapon = weapons[Random.Range(0, weapons.Length)];
    }
}

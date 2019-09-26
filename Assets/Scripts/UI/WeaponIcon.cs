using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    public Weapon weapon;

    public Image selected;
    public Text bulletCount;

    private void Start()
    {
        if (weapon.infinteBullet)
        {
            bulletCount.text = "∞";
            bulletCount.fontSize *= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        selected.gameObject.SetActive(weapon.Using);
        if (!weapon.infinteBullet)
        {
            bulletCount.text = $"{weapon.BulletCount}/{weapon.maxCapacity}";
        }
    }
}
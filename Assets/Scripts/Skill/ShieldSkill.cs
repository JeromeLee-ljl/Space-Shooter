using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : Skill
{
    public Shield shield;

    public float shieldRadius = 0.5f;

    public float shieldShowTime = 5f;

    void Start()
    {
        shield.transform.localScale = new Vector3(shieldRadius * 2, shieldRadius * 2, 1);
        CloseShield();
    }

    protected override void Effect()
    {
        shield.gameObject.SetActive(true);
        Invoke("CloseShield", shieldShowTime);
    }

    public void CloseShield()
    {
        shield.gameObject.SetActive(false);
    }
}
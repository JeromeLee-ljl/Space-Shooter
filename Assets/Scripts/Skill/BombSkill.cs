using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkill : Skill
{
    public Missile missilePrefab;
    public Transform gunPoint;
    public float missileSpeed;
    public float damage;
    public float boomRaidus = 6f;
    public float boomDamage = 10f;
    public AudioClip shootClip;

    protected override void Effect()
    {
        AudioManager.Instance.PlayGameClip(shootClip);
        CreateMissile();
    }

    protected Bullet CreateMissile()
    {
        Missile bullet = PoolManager.Instance.GetInstance(missilePrefab.name).GetComponent<Missile>();
        bullet.transform.position = gunPoint.position;
        bullet.transform.rotation = gunPoint.rotation;
        bullet.gameObject.layer = gameObject.layer;

        bullet.speed = missileSpeed;
        bullet.hitDamage = damage;
        bullet.explosionRadius = boomRaidus;
        bullet.explosionDamage = boomDamage;
        bullet.Reset();
        return bullet;
    }
}
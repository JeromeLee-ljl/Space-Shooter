using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MyMono
{
    public Bullet bulletPrefab;
    public float bulletSpeed = 5f;

    public float trackAbility = 10f;

//    public Bullet bulletPrefab;
    // 枪口位置，bullet从此处发射
    public Transform gunPoint;

    // 伤害
    public float damage = 1f;

    // 两次开火的间隔
    public float fireInterval = 0.3f;

    public bool infinteBullet;

    // 最大容量
    public int maxCapacity = 100;

    // 当前子弹数量
    public int BulletCount { get; private set; }

    public AudioClip shootClip;
    private bool _isUsing;

    public bool Using { get; set; }

    protected override void Awake()
    {
        base.Awake();
        BulletCount = maxCapacity;
    }

    /// <summary>
    /// 朝正前方开火,跟踪目标 
    /// </summary>
    public void Fire(Transform target)
    {
        if (BulletCount <= 0) return;

        if (!infinteBullet)
        {
            BulletCount--;
        }

//        if (gameObject.layer == LayerMask.NameToLayer("Player"))
//        {
//            BulletCount--;
//        }
        AudioManager.Instance.PlayGameClip(shootClip);
        FireToTarget(target);
    }

    /// <summary>
    /// 添加子弹
    /// </summary>
    public void AddBullet(int count)
    {
        if (BulletCount >= maxCapacity) return;
        BulletCount = Mathf.Clamp(BulletCount + count, 0, maxCapacity);
    }

    protected Bullet CreateBullet(Transform target)
    {
        Bullet bullet = PoolManager.Instance.GetInstance(bulletPrefab.name).GetComponent<Bullet>();
        bullet.transform.position = gunPoint.position;
        bullet.transform.rotation = gunPoint.rotation;
        bullet.gameObject.layer = gameObject.layer;
        bullet.trackAbility = trackAbility;
        bullet.onHitPlane = _ => { };
        bullet.speed = bulletSpeed;
        bullet.hitDamage = damage;
        bullet.target = target;
        bullet.Reset();
        return bullet;
    }

    // 子类实现开火功能
    protected abstract void FireToTarget(Transform target);
}
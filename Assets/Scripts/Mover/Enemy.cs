using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

//todo shoot move
public class Enemy : Plane
{
    public int score = 1;

    public void SetVelocity(Vector2 velocity)
    {
        _rigidbody2D.velocity = velocity;
    }

    public void ShootInterval(float interval)
    {
        StartCoroutine(StartShoot(interval));
    }

    private IEnumerator StartShoot(float interval)
    {
        while (gameObject.activeInHierarchy && currentHealth > 0)
        {
            yield return new WaitForSeconds(interval);
            shootSkill.Launch();
        }
    }

    protected override void Update()
    {
        base.Update();
        // 如果到达边界，则反向运动
        if (Mathf.Abs(_rigidbody2D.position.x) > GameManager.Instance.gameRange.x)
        {
            var velocity = _rigidbody2D.velocity;
            velocity = new Vector2(-velocity.x, velocity.y);
            _rigidbody2D.velocity = velocity;
        }

        if (transform.position.y > GameManager.Instance.gameRange.y)
        {
            if (Math.Abs(_rigidbody2D.velocity.y) < 0.01f)
            {
                transform.Translate(Time.deltaTime * speed * Vector3.up);
            }
        }
    }


    public Item dropItemPrefab;

    public float dropPercent;

    // 死亡，回收，爆炸
    protected override void DestroySelf()
    {
        base.DestroySelf();
        if (Random.Range(0, 1f) < dropPercent && dropItemPrefab != null)
        {
            GameObject item = PoolManager.Instance.GetInstance(dropItemPrefab.name);
            item.transform.position = transform.position;
        }

        GameManager.Instance.score += score;
    }
}
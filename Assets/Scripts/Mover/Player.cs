using System;
using System.Collections;
using UnityEngine;


public class Player : Plane
{
    public float deathTime = 1f;

    // <summary>
    /// 移动 限制范围
    /// </summary>
    public void Move(float x, float y)
    {
        Vector2 moveRange = GameManager.Instance.gameRange;
        // 限制移动范围
        if (_rigidbody2D.position.x > moveRange.x && x > 0 || _rigidbody2D.position.x < -moveRange.x && x < 0)
            x = 0;
        if (_rigidbody2D.position.y > moveRange.y && y > 0 || _rigidbody2D.position.y < -moveRange.y && y < 0)
            y = 0;
        _rigidbody2D.velocity = speed * new Vector2(x, y).normalized;
    }

    /// <summary>
    /// 被攻击，损失生命值
    /// </summary>
    public override void ChangeHealth(float delta)
    {
        base.ChangeHealth(delta);
        if (currentHealth > 0 && delta < 0)
        {
            // todo 添加无敌时间,闪烁
        }
    }

    protected override void DestroySelf()
    {
        Explosion();
        // 不再发生碰撞
        GetComponent<Collider2D>().enabled = false;
        // 不能操作
        GetComponent<PlayerController>().enabled = false;
        // 隐藏
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        // 当死亡时，护盾也应该消失
        shieldSkill.CloseShield();
        StartCoroutine(DeathDelay(deathTime));
    }

    IEnumerator DeathDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }
}
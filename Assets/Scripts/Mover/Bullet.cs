using System;
using System.Collections;
using UnityEngine;

public class Bullet : Mover
{
    // 击中飞机时调用的委托
    public Action<Collider2D> onHitPlane = plane => { };

    // 子弹拐弯能力 0~1
    [HideInInspector] public float trackAbility;

    public Transform target;

    private void FixedUpdate()
    {
        // move
        _rigidbody2D.MovePosition(_rigidbody2D.position + (Vector2) (speed * Time.fixedDeltaTime * transform.up));

        // track target
        if (target != null && target.gameObject.activeSelf)
        {
            Vector3 dir = target.position - transform.position;
            transform.up = Vector2.Lerp(transform.up, dir.normalized, trackAbility * Time.fixedDeltaTime);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Plane>() != null)
            onHitPlane(other);

        base.OnTriggerEnter2D(other);
    }

    public void DestroyBullet()
    {
        DestroySelf();
    }
}
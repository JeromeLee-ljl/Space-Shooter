using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        // 若撞到Plane则爆炸
        if (other.GetComponent<Plane>() != null)
        {
            DestroySelf();
        }
    }
}
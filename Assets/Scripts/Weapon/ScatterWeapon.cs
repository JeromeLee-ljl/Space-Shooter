using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterWeapon : Weapon
{
    public float scatterRangeAngle = 20f;
    public int scatterCount = 3;

    protected override void FireToTarget(Transform target)
    {
        if (scatterCount == 1)
        {
            CreateBullet(target);
        }
        else
        {
            for (int i = 0; i < scatterCount; i++)
            {
                Bullet bullet = CreateBullet(target);
                float angle = Mathf.Lerp(-scatterRangeAngle, scatterRangeAngle, (float) i / (scatterCount - 1));
                bullet.transform.RotateAround(gunPoint.parent.position, Vector3.forward, angle);
            }
        }
    }
}
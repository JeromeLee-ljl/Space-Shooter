using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo  replaced by scatterweapon
public class ParallelWeapon : Weapon
{
    public int parallelCount = 1;
    public float parallelRange = 2;

    protected override void FireToTarget(Transform target)
    {
        if (parallelCount == 1)
        {
            CreateBullet(target);
        }
        else
        {
            for (int i = 0; i < parallelCount; i++)
            {
                Bullet bullet = CreateBullet(target);
                float x = Mathf.Lerp(-parallelRange, parallelRange, (float) i / (parallelCount - 1));
                bullet.transform.Translate(x, 0, 0);
            }
        }
    }
}
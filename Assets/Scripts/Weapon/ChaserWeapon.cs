using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserWeapon : Weapon
{
    // 用于子弹击中后设置目标
    public ShootSkill shootSkill;

    // 追踪弹击中目标后可以引导其他子弹的时间
    public float signalTime = 5f;

    protected override void FireToTarget(Transform target)
    {
        // 信号弹不能追踪
        Bullet bullet = CreateBullet(null);
        //todo 
        bullet.onHitPlane = SetTarget;
    }


    private void SetTarget(Collider2D other)
    {
        ShootSkill.SaveTarget(_camp, other.transform);
        // 可能调用时敌人被消灭，不能调用协程取消锁定， 或当协程还未执行完时，敌人被消灭，也不能取消子弹锁定, 所有用GameManager的方法开启协程
//        StartCoroutine(UnSetTarget(other));
        GameManager.Instance.StartCoroutine(UnSetTarget(other));
    }

    // 当敌机被歼灭时或超过引导时间，取消引导
    IEnumerator UnSetTarget(Collider2D target)
    {
        float time = 0f;
        while (time < signalTime)
        {
            yield return null;
            time += Time.deltaTime;
            if (!target.gameObject.activeInHierarchy)
                break;
        }

        ShootSkill.SaveTarget(_camp, null);
    }
}
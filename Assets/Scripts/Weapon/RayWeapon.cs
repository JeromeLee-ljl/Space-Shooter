using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayWeapon : Weapon
{
    public GameObject ray;
    public GameObject rayHitExplosionPrefab;

    public float rayWidth = 0.5f;

    public float rayMaxDistance = 100f;

    private Transform _plane;

    // Start is called before the first frame update
    void Start()
    {
        _plane = transform.parent.parent;
        ray.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = _plane.up;
    }

    protected override void FireToTarget(Transform target)
    {
        ray.SetActive(true);
        Invoke("CloseRay", fireInterval - Time.deltaTime);

        // 根据阻挡物判断激光的长度
        var hits = Physics2D.RaycastAll(gunPoint.position, transform.up, rayMaxDistance,
            GetAgainstLayerMask(gameObject.layer));
        bool hasObstacle = false;
        // for循环排除其他物体对激光的抵挡，只有Plane 和Bullet 能挡住激光
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Health") || hits[i].collider.CompareTag("Rock")) continue;
            hasObstacle = true;
            RayCast(hits[i].distance);
            break;
//            Mover mover = hits[i].collider.GetComponent<Mover>();
//            if (mover is Plane || mover is Bullet)
//            {
//                hasObstacle = true;
//                RayCast(hits[i].distance);
//                break;
//            }
        }

        if (!hasObstacle)
            RayCast(rayMaxDistance);
    }

    // 对激光范围内所有敌方施加伤害
    private void RayCast(float length)
    {
        ray.transform.localScale = new Vector3(rayWidth, length, 1);
        var hits = Physics2D.BoxCastAll(gunPoint.position, new Vector2(rayWidth, 0.1f), 0, transform.up, length,
            GetAgainstLayerMask(gameObject.layer));
        for (int i = 0; i < hits.Length; i++)
        {
            Mover mover = hits[i].collider.GetComponent<Mover>();
            if (mover != null)
            {
                if (mover is Plane)
                {
                    // 如果是Plane 则要有爆炸效果
                    GameObject explosion = PoolManager.Instance.GetInstance(rayHitExplosionPrefab.name);
                    explosion.transform.localScale = new Vector3(rayWidth, rayWidth, 1);
                    explosion.transform.position = hits[i].point;
                }

                mover.ChangeHealth(-damage);
            }
        }
    }

    private void CloseRay()
    {
        ray.SetActive(false);
    }
}
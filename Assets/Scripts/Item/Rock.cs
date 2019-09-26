using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : Mover
{
    public GameObject hideObject;

    private void OnEnable()
    {
        _rigidbody2D.angularVelocity = Random.Range(-90, 90);
    }

    public void SetVelocity(Vector2 v)
    {
        _rigidbody2D.velocity = v;
    }

    protected override void DestroySelf()
    {
        base.DestroySelf();
        PoolManager.Instance.GetInstance(hideObject.name).transform.position = transform.position;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MyMono
{
    public float rotateSpeed = 360;

    void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.GetComponent<Bullet>().DestroyBullet();
            // 可以被炸弹摧毁
            if (other.name.StartsWith("Missile"))
            {
                Debug.Log("Shield Destroyed by Missile");
                gameObject.SetActive(false);
            }

        }
    }
}
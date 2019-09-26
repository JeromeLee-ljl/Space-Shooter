using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // 动画结束时调用此事件
    public virtual void DestroySelf()
    {
        PoolManager.Instance.GiveBack(gameObject.transform.parent.gameObject);
    }
}
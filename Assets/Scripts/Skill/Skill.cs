using System;
using System.Collections;
using UnityEngine;

public delegate void RemainCoolingTimeChange(float remainCoolingTime);

public abstract class Skill : MyMono
{
    // 技能冷却时间
    public float coolingTime = 1f;

    private float _remainCoolingTime;

    // 技能发动后剩余时间
    public float RemainCoolingTime
    {
        get => _remainCoolingTime;
        private set
        {
            _remainCoolingTime = value;
            OnCoolingTimeChanged(value);
        }
    }

    // 剩余时间改变时调用， UI中订阅此事件以更新UI
    public event RemainCoolingTimeChange OnCoolingTimeChanged = time => { };

    /// <summary>
    /// 发动技能
    /// </summary>
    public virtual void Launch()
    {
        if (!gameObject.activeInHierarchy) return;
        //冷却中，返回
        if (RemainCoolingTime > 0) return;

        StartCoroutine(CoolingTimer());
        Effect();
    }

    /// <summary>
    /// 技能所造成的效果
    /// </summary>
    protected abstract void Effect();

    /// <summary>
    /// 技能冷却计时协程
    /// </summary>
    /// <returns></returns>
    IEnumerator CoolingTimer()
    {
        RemainCoolingTime = coolingTime;
        // 退出循环时 RemainCoolingTime必须大于0 ！！！ 否则在当RemainCoolingTime小于等于0时可能会被重置，则协程不能正常退出
        while (RemainCoolingTime > Time.deltaTime)
        {
            RemainCoolingTime -= Time.deltaTime;
            yield return null;
        }

        RemainCoolingTime = 0;
        yield return null;
    }

    /// <summary>
    /// 刷新技能剩余冷却时间
    /// </summary>
    public virtual void RefreshCoolingTime()
    {
        RemainCoolingTime = 0;
    }

    // 是否需要随鼠标转动
    public bool isAnimAtMouse;

    // 转动的角度
    public float aimRangeAngle = 30;

    private void FixedUpdate()
    {
        if (isAnimAtMouse)
            AimAtMouse();
    }

    // 使技能武器指向鼠标方向
    void AimAtMouse()
    {
        //获取鼠标位置
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePosition - transform.position;
        if (Vector2.Angle(Vector2.up, dir) < aimRangeAngle)
            transform.up = dir;
    }
}
using UnityEngine;

public class ShootSkill : Skill
{
    // 所有敌方单位的攻击目标
    public static Transform EnemyTarget;

    // 玩家的攻击目标
    public static Transform PlayerTarget;

    public static Transform GetTarget(Camp camp)
    {
        if (camp == Camp.Player)
            return PlayerTarget;
        if (camp == Camp.Enemy)
            return EnemyTarget;
        return null;
    }

    public static void SaveTarget(Camp camp, Transform target)
    {
        Debug.Log($"set {camp} target:{target?.name ?? "null"}");
        if (camp == Camp.Player)
            PlayerTarget = target;
        else if (camp == Camp.Enemy)
            EnemyTarget = target;
    }
    //-------------------------------------------


    public Weapon[] weapons;

    // 当前武器r
    public int currentWeaponIndex;

    public Weapon CurrentWeapon { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        foreach (var weapon in weapons)
        {
            weapon.gameObject.layer = gameObject.layer;
        }

        SetWeapon();
    }

    public override void Launch()
    {
        if (CurrentWeapon.BulletCount <= 0) return;
        base.Launch();
    }

    protected override void Effect()
    {
        CurrentWeapon.Fire(GetTarget(_camp));
    }

    /// <summary>
    /// 切换武器
    /// </summary>
    /// <param name="index">武器所在的下标</param>
    public void SwitchWeapon(int index)
    {
        if (index == currentWeaponIndex) return;
        if (index < 0 || index >= weapons.Length)
        {
            Debug.Log("切换武器失败");
            return;
        }

//        Log("切换武器成功：" + index);

        weapons[currentWeaponIndex].Using = false;
        currentWeaponIndex = index;

        RefreshCoolingTime();
        SetWeapon();
    }

    private void SetWeapon()
    {
        CurrentWeapon = weapons[currentWeaponIndex];
        coolingTime = weapons[currentWeaponIndex].fireInterval;
        weapons[currentWeaponIndex].Using = true;
    }

    public T GetWeapon<T>() where T : Weapon
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] is T)
                return weapons[i] as T;
        }

        return null;
    }
}
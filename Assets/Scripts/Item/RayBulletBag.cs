using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBulletBag : Item
{
    public int count = 30;

    protected override void TriggerPlayer(Player player)
    {
        player.shootSkill.GetWeapon<RayWeapon>().AddBullet(count);
    }
}
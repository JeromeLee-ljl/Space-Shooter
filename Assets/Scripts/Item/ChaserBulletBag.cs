using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserBulletBag : Item
{
    public int count = 10;

    protected override void TriggerPlayer(Player player)
    {
        player.shootSkill.GetWeapon<ChaserWeapon>().AddBullet(count);
    }
}
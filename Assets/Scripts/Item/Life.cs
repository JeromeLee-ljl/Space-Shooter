using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Item
{
    public float health = 10f;

    protected override void TriggerPlayer(Player player)
    {
        player.ChangeHealth(health);
    }
}
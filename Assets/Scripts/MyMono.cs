using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum Camp
{
    Null,
    Player,
    Enemy
}

public class MyMono : MonoBehaviour
{
    protected Camp _camp;

    protected virtual void Awake()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
            _camp = Camp.Player;
        else
            _camp = Camp.Enemy;
//        Debug.Log($"{gameObject.name} layer:{gameObject.layer}  camp:{_camp}");
    }

    protected static int GetAgainstLayerMask(Camp layer)
    {
        if (layer.ToString() == "Player")
            return LayerMask.GetMask("Enemy");
        return LayerMask.GetMask("Player");
    }

    protected static int GetAgainstLayerMask(int layer)
    {
        if (LayerMask.LayerToName(layer) == "Player")
            return LayerMask.GetMask("Enemy");
        return LayerMask.GetMask("Player");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    public Skill skill;
    public Image CDMask;

    // Start is called before the first frame update
    void Start()
    {
        skill.OnCoolingTimeChanged += ChangeMask;
    }

    private void ChangeMask(float remainCoolingTime)
    {
        CDMask.fillAmount = remainCoolingTime / skill.coolingTime;
    }
}
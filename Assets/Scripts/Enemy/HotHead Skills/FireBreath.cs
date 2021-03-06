﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/FireBreath")]
public class FireBreath : Skill
{
    public override void initSkillValue()
    {
        realInitSkillValue();
    }

    public override void TestValue()
    {
        RealTestValue();
    }

    public override void Use()
    {
        currentCountdown = valueCondition;
        Manager.Instance.playerManager.TakeDamages(damages);
        BlockDice();
        Manager.Instance.playerManager.InitFireAlt(1);
        equipementOwner.AnimationUse();
    }
}

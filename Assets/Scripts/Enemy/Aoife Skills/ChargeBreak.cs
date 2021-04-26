using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/ChargeBreak")]
public class ChargeBreak : Skill
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
        Manager.Instance.playerManager.TakeDamages(Manager.Instance.enemyBehaviour.armor);
        Manager.Instance.enemyBehaviour.armor = 0;
        BlockDice();
        equipementOwner.AnimationUse();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/BroadswordBreak")]
public class BroadswordBreak : Skill
{
    public override void initSkillValue()
    {
        if (conditions == conditionType.countdown && Manager.Instance.firstTurn)
        {
            currentCountdown = valueCondition;

        }

        if (isReusable)
        {
            timeUsed = 0;
        }
    }

    public override void TestValue()
    {
        RealTestValue();
    }

    public override void Use()
    {
        Manager.Instance.playerManager.TakeDamages(equipementOwner.diceOwn.valueDice);
        BlockDice();
        equipementOwner.AnimationUse();
    }
}

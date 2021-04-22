using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/CoupDePied")]
public class CoupDePied : Skill
{
    public override void initSkillValue()
    {
        if (conditions == conditionType.countdown)
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
        BlockDice();

        Manager.Instance.enemyBehaviour.TakeDamages(damages);
        Manager.Instance.enemyBehaviour.numberOfShock++;

        if (timeUsed == reusableTime)
        {
            equipementOwner.AnimationUse();
        }
    }

}

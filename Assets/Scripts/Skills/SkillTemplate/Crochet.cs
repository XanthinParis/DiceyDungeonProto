using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/Crochet")]
public class Crochet : Skill
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
        Manager.Instance.enemyBehaviour.InitBreak(1);

    }
    
}

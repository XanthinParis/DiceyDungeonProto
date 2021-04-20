using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/FireBreath")]
public class FireBreath : Skill
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
        equipementOwner.GetComponent<BoxCollider2D>().enabled = false;
        Manager.Instance.playerManager.TakeDamages(damages);
        BlockDice();
        Manager.Instance.playerManager.InitFireAlt(1);
        equipementOwner.AnimationUse();
    }
}

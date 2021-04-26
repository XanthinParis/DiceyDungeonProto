using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/Charge")]
public class Charge : Skill
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
        BlockDice();
        equipementOwner.AnimationUse();
    }
}

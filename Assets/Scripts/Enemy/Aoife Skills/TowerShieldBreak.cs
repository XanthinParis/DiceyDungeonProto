using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/TowerShieldBreak")]
public class TowerShieldBreak : Skill
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
        BlockDice();
        equipementOwner.diceOwn = null;
        Manager.Instance.enemyBehaviour.armor += equipementOwner.diceOwn.valueDice;
        equipementOwner.AnimationUse();

    }
}

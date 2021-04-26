using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/Broadsword")]
public class Broadsword : Skill
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
        Manager.Instance.playerManager.TakeDamages(equipementOwner.diceOwn.valueDice+2);
        BlockDice();
        equipementOwner.AnimationUse();
    }
}

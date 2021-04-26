using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/FireBall")]
public class FireBall : Skill
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
        Manager.Instance.playerManager.TakeDamages(equipementOwner.diceOwn.valueDice);
        BlockDice();
        Manager.Instance.playerManager.InitFireAlt(1);
        equipementOwner.AnimationUse();
    }
}

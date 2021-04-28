using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Equipements/Direct")]
public class Direct : Skill
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

        if (isReusable)
        {
            Manager.Instance.playerManager.directRepetition++;

            Manager.Instance.enemyBehaviour.TakeDamages(damages + Manager.Instance.playerManager.directRepetition);
            equipementOwner.AnimationUse();
        }
    }

   
}

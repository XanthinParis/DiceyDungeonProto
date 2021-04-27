using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/Crochet")]
public class Crochet : Skill
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

        Manager.Instance.enemyBehaviour.TakeDamages(damages);
        Manager.Instance.enemyBehaviour.numberOfBreak++;

    }
    
}

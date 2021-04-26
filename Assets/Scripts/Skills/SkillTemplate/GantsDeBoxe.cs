using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/GantsDeBoxe")]
public class GantsDeBoxe : Skill
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

        equipementOwner.GetComponent<BoxCollider2D>().enabled = false;

        Manager.Instance.enemyBehaviour.TakeDamages(damages);
        equipementOwner.AnimationUse();
        
        Debug.Log("Correct");
    }

}

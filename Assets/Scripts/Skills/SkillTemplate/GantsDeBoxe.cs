using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/GantsDeBoxe")]
public class GantsDeBoxe : Skill
{
    public override void initSkillValue()
    {
        if (Manager.Instance.firstTurn)
        {
            if (conditions == conditionType.countdown)
            {
                currentCountdown = valueCondition;

            }
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

        Manager.Instance.enemyBehaviour.TakeDamages(damages);
        equipementOwner.AnimationUse();
        
        Debug.Log("Correct");
    }

}

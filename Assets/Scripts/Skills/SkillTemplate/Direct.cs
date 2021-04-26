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

            equipementOwner.diceOwn.gameObject.SetActive(false);
            equipementOwner.diceOwn = null;
            equipementOwner.GetComponent<BoxCollider2D>().enabled = false;

            Manager.Instance.enemyBehaviour.TakeDamages(damages + Manager.Instance.playerManager.directRepetition);

            currentCountdown = valueCondition;
        }
    }

   
}

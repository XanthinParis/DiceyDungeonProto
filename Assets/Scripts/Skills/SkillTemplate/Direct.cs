using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Equipements/Direct")]
public class Direct : Skill
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
        //Bloquer les joueurs sur la position;
        equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
        equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
        equipementOwner.diceOwn.canMove = false;

        equipementOwner.diceOwn.gameObject.SetActive(false);
        equipementOwner.diceOwn = null;

        if (isReusable)
        {
            Manager.Instance.playerManager.directRepetition++;

            equipementOwner.diceOwn.gameObject.SetActive(false);
            equipementOwner.diceOwn = null;

            Manager.Instance.enemyBehaviour.TakeDamages(damages + Manager.Instance.playerManager.directRepetition);

            currentCountdown = valueCondition;
        }
    }

}

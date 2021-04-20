using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/Esquive")]
public class Esquive : Skill
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
        BlockDice();

        Manager.Instance.playerManager.isEsquive = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/TowerShield")]
public class TowerShield : Skill
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
        //Bloquer les joueurs sur la position;
        BlockDice();

        if (isReusable)
        {
            timeUsed++;
            if (timeUsed < reusableTime)
            {
                //ajout un dé à la main du joueur.
                Manager.Instance.enemyBehaviour.armor += equipementOwner.diceOwn.valueDice;
                equipementOwner.diceOwn = null;
            }
            else
            {
                equipementOwner.diceOwn = null;
                Manager.Instance.enemyBehaviour.armor += equipementOwner.diceOwn.valueDice;
                equipementOwner.AnimationUse();
            }
        }
    }
}

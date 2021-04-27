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
       
        if (isReusable)
        {
            timeUsed++;
            if (timeUsed < reusableTime)
            {
                //ajout un dé à la main du joueur.
                Manager.Instance.enemyBehaviour.armor += equipementOwner.diceOwn.valueDice;
                Manager.Instance.canvasManager.UpdateHealth();
                
            }
            else
            {
                Manager.Instance.enemyBehaviour.armor += equipementOwner.diceOwn.valueDice;
                Manager.Instance.canvasManager.UpdateHealth();
                
                equipementOwner.AnimationUse();
            }
        }

        BlockDice();
    }
}

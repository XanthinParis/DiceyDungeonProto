﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Equipements/JeuDeJambes")]
public class JeuDeJambes : Skill
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
                //Laisser la compétence
               
                //ajout un dé à la main du joueur.
                Manager.Instance.diceManager.AddDicePlayer(1);
                equipementOwner.diceOwn = null;
            }
            else
            {
              
                Manager.Instance.diceManager.AddDicePlayer(1);
                equipementOwner.AnimationUse();
            }
        }
    }

}

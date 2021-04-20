using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Equipements/JeuDeJambes")]
public class JeuDeJambes : Skill
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
        

        if (isReusable)
        {
            timeUsed++;
            if (timeUsed < reusableTime)
            {
                //Laisser la compétence
                equipementOwner.diceOwn.gameObject.SetActive(false);
                equipementOwner.diceOwn = null;
                //ajout un dé à la main du joueur.
                Manager.Instance.diceManager.addDice(1);
            }
            else
            {
                equipementOwner.diceOwn.gameObject.SetActive(false);
                equipementOwner.diceOwn = null;
                Manager.Instance.diceManager.addDice(1);
                equipementOwner.AnimationUse();
                equipementOwner.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

}

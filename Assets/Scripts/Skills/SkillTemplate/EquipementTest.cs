using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Equipements/Test")]
public class EquipementTest : Skill
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
        equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
        equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
        equipementOwner.diceOwn.canMove = false;

        //Stored Dice Value;
        int currentDiceValue = equipementOwner.diceOwn.valueDice;

        //Effect;

        //ClearEquipement - Animation;

        Debug.Log("Correct");
    }
}

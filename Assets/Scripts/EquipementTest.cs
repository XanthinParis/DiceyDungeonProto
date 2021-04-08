using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementTest : Skill
{
    private void Awake()
    {
        if(conditions == conditionType.countdown)
        {
            currentCountdown = valueCondition;
        }
    }

    public override void TestValue()
    {
        switch (conditions)
        {
            //The Dice Value need to be lower or equal to the value ask;
            case conditionType.minValue:
                if (diceOwn.valueDice <= valueCondition)
                {
                    Use();
                }
                else
                {
                    Debug.Log("Wrong Dice Value");
                    break;
                }
                break;

            //The Dice Value need to be higher or equal to the value ask;
            case conditionType.maxValue:
                if (diceOwn.valueDice >= valueCondition)
                {
                    Use();
                }
                else
                {
                    Debug.Log("Wrong Dice Value");
                    break;
                }
                break;
            //While the countdown is not 0, don't start Use(); Each dice reduce the currentCountdown Value
            case conditionType.countdown:
                currentCountdown -= diceOwn.valueDice;

                if (currentCountdown < 0)
                {
                    currentCountdown = 0;
                    currentCountdown = valueCondition;
                    Use();
                }
                else
                {
                    //Manger le Dé
                    break;
                }
                break;
                
            //The Dice Value need to be pair (2.4.6).
            case conditionType.pair:
                if (diceOwn.valueDice == 2 || diceOwn.valueDice == 4 || diceOwn.valueDice == 6)
                {
                    Use();
                }
                else
                {
                    Debug.Log("Wrong Dice Value");
                }
                break;

            //The Dice Value need to be pair (1.3.5).
            case conditionType.impair:
                if (diceOwn.valueDice == 1 || diceOwn.valueDice == 3 || diceOwn.valueDice == 5)
                {
                    Use();
                }
                else
                {
                    Debug.Log("Wrong Dice Value");
                }
                break;

            case conditionType.value:
                Use();
                break;

            default:
                break;
        }
    }

    public override void Use()
    {
        //Stored Dice Value;
        int currentDiceValue = diceOwn.valueDice;

        //DestroyDice
        DestroyDice();

        //Effect;

        //ClearEquipement - Animation;
    }

    public override void DestroyDice()
    {
        Manager.Instance.playerManager.storedDice.Remove(diceOwn.gameObject);
        Destroy(diceOwn.gameObject);
    }
}

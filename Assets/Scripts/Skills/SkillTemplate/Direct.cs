﻿using System.Collections;
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
        switch (conditions)
        {
            //The Dice Value need to be lower or equal to the value ask;
            case conditionType.maxValue:
                if (equipementOwner.diceOwn.valueDice <= valueCondition)
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
            case conditionType.minValue:
                if (equipementOwner.diceOwn.valueDice >= valueCondition)
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

                equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
                equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
                equipementOwner.diceOwn.canMove = false;

                Manager.Instance.playerManager.StartCoroutine(Manager.Instance.playerManager.DelayCountdown(equipementOwner.diceOwn.valueDice, equipementOwner));

                if (currentCountdown < 0)
                {
                    DestroyDice();
                    currentCountdown = 0;

                    currentCountdown = valueCondition;
                    Use();
                }
                else
                {
                    DestroyDice();
                    break;
                }
                break;

            //The Dice Value need to be pair (2.4.6).
            case conditionType.pair:
                if (equipementOwner.diceOwn.valueDice == 2 || equipementOwner.diceOwn.valueDice == 4 || equipementOwner.diceOwn.valueDice == 6)
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
                if (equipementOwner.diceOwn.valueDice == 1 || equipementOwner.diceOwn.valueDice == 3 || equipementOwner.diceOwn.valueDice == 5)
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

    public override void DestroyDice()
    {
        Manager.Instance.playerManager.storedDice.Remove(equipementOwner.diceOwn.gameObject);
        Destroy(equipementOwner.diceOwn.gameObject);
    }

}

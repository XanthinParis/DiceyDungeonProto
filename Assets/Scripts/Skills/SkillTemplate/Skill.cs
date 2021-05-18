using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    [Header("Usability")]
    public bool isReusable;
    public int reusableTime;
    public int timeUsed = 0;
    public bool isBig;
    public int damages;
    public int valueCondition;
    public int currentCountdown;

    [Header("Alterations")]
    public bool isBreak;
    public bool isShock;
    public bool currentlyOnField = false;

    //Type of Conditions
    public enum conditionType { minValue, maxValue, countdown, pair, impair, value }
    public conditionType conditions;

    public enum team {Player, Enemy}
    public team side;

    [Header("UI things")]
    public string skillName;
    public string skillDescription;
    public string diceDescription;
    public bool countdownUsed = false;
    public Color BGColor;
    public Color topBGColor;
    
    //Variable to Store when a Dice is put in a equipement;
    public EquipementOwner equipementOwner;

    public abstract void initSkillValue();

    public void realInitSkillValue()
    {
        if (conditions == conditionType.countdown && Manager.Instance.firstTurn)
        {
            isBreak = false;
            isShock = false;
            currentCountdown = valueCondition;
            countdownUsed = false;
        }

        if (countdownUsed)
        {
            Debug.Log("la");
            currentCountdown = valueCondition;
        }

        if (isReusable)
        {
            timeUsed = 0;
        }

        
    }

    //Test the value of the Dice according conditionType;
    public abstract void TestValue();

    public void RealTestValue()
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
                if(equipementOwner.equipementOwn.side == team.Player)
                {
                    Manager.Instance.playerManager.StartCoroutine(Manager.Instance.playerManager.DelayCountdown(equipementOwner.diceOwn.valueDice, equipementOwner));
                }
                else
                {
                    Manager.Instance.enemyBehaviour.StartCoroutine(Manager.Instance.enemyBehaviour.DelayCountdownEnemy(equipementOwner.diceOwn.valueDice, equipementOwner));
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

    //Effect of the Skill;
    public abstract void Use();

    public void BlockDice()
    {  
        //Bloquer les joueurs sur la position;
        equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
        equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
        equipementOwner.diceOwn.canMove = false;

        equipementOwner.diceOwn.gameObject.SetActive(false);
    }

}

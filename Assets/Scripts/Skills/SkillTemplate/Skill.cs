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

    public bool isBreak;
    public bool isShock;
    public bool currentlyOnField = false;

    //Type of Conditions
    public enum conditionType { minValue, maxValue, countdown, pair, impair, value }
    public conditionType conditions;

    [Header("UI things")]
    public string skillName;
    public string skillDescription;
    public string diceDescription;
    
    //Variable to Store when a Dice is put in a equipement;
    public EquipementOwner equipementOwner;

    public abstract void initSkillValue();

    //Test the value of the Dice according conditionType;
    public abstract void TestValue();

    //Effect of the Skill;
    public abstract void Use();

    public abstract void DestroyDice();

    
    

}

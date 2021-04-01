using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public bool isReusable;
    public int reusableTime;

    public string skillName;
    public string skillDescription;
    public bool isBig;

    public enum conditionType {minValue,maxValue,countdown,pair,impair,value}

    public abstract void Use();

}

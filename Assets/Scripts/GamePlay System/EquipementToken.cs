using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipements", menuName = "EquipementCard", order = 1000)]
public class EquipementToken : ScriptableObject
{
    public bool isBig;
    public string equipementName;
    public string effect;
    public bool reusable;
    public int reusableTime;
    public enum conditionType {compteur, valeurX, valeurMin, valeurMax}
    public conditionType condition;
    public Sprite assetCondition;
}

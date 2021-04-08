using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipementOwner : MonoBehaviour
{
    public Skill equipementOwn;

    public GameObject diceOwn;

    [Header("Visual Stuff")]
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI diceValue;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI diceDescription;

    private bool diceHere;

    public void UpdateVisuel()
    {
        skillName.text = equipementOwn.skillName;
        description.text = equipementOwn.skillDescription;
        diceDescription.text = equipementOwn.diceDescription;

        if (equipementOwn.conditions == Skill.conditionType.countdown)
        {
            diceValue.text = equipementOwn.currentCountdown.ToString();
        }
        else
        {
            diceValue.text = equipementOwn.valueCondition.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            if(collision.gameObject == Manager.Instance.playerManager.storedDice[i])
            {
                //StartTest;
            }
        }
    }
}

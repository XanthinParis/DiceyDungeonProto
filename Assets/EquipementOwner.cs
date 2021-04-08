using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipementOwner : MonoBehaviour
{
    public Skill equipementOwn;

    public DiceBehaviour diceOwn;

    [Header("Visual Stuff")]
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] public TextMeshProUGUI diceValue;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI diceDescription;

    public Transform dicePosition;

    public bool diceHere;

    private void Start()
    {
        
        InitSkill();
    }
    
    public void InitSkill()
    {
        equipementOwn.equipementOwner = this;
        Debug.Log(equipementOwn.equipementOwner);

        if (equipementOwn.conditions == Skill.conditionType.countdown)
        {
            equipementOwn.currentCountdown = equipementOwn.valueCondition;
        }
    }

    //Se lance quand l'équipement est instantier.
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

    private void Update()
    {
        if (diceHere && Input.GetMouseButtonUp(0))
        {
            equipementOwn.TestValue();
        }
    }

    #region OnTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            if(collision.gameObject == Manager.Instance.playerManager.storedDice[i])
            {
                diceOwn = collision.gameObject.GetComponent<DiceBehaviour>();
                Debug.Log("Collide");
                diceHere = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            if (collision.gameObject == Manager.Instance.playerManager.storedDice[i])
            {
                diceHere = false;
                diceOwn = null;
                Debug.Log("ExitCollide");
            }
        }
    }
    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipementOwner : MonoBehaviour
{
    public int position; //Entre 0 et 4;

    public Skill equipementOwn;

    public DiceBehaviour diceOwn;

    [Header("Visual Stuff")]
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] public TextMeshProUGUI diceValue;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI diceDescription;

    public GameObject chocItem;

    private Tweener t;

    public Transform dicePosition;

    public bool diceHere;

    public ChocBehaviour chocBehaviour;
    public bool isChoc = false;
    public bool isBreak = false;

    private void Start()
    {
        t = GetComponent<Tweener>();
        
        InitSkill();
    }
    
    public void InitSkill()
    {
        equipementOwn.equipementOwner = this;
        
        //Debug.Log(equipementOwn.equipementOwner);
    }

    //Se lance quand l'équipement est instantier.
    public void UpdateVisuel()
    {
        skillName.text = equipementOwn.skillName;
        description.text = equipementOwn.skillDescription;
        diceDescription.text = equipementOwn.diceDescription;

        switch (equipementOwn.conditions)
        {
            case Skill.conditionType.minValue:
                diceValue.text = "Min: " +  equipementOwn.valueCondition.ToString();
                break;
            case Skill.conditionType.maxValue:
                diceValue.text = "Max: " + equipementOwn.valueCondition.ToString();
                break;
            case Skill.conditionType.countdown:
                if (Manager.Instance.firstTurn)
                {
                    diceValue.text = equipementOwn.valueCondition.ToString();
                }
                else
                {

                    diceValue.text = equipementOwn.currentCountdown.ToString();
                }
                break;
            case Skill.conditionType.pair:
                diceValue.text = " ";
                break;
            case Skill.conditionType.impair:
                diceValue.text = " ";
                break;
            case Skill.conditionType.value:
                diceValue.text = " ";
                break;
            default:
                break;
        }
    }

    public void UpdateVisuelAlt()
    {
        diceValue.text = equipementOwn.valueCondition.ToString();
    }

    private void Update()
    {
        if (diceHere && Input.GetMouseButtonUp(0))
        {
            equipementOwn.TestValue();
        }
    }

    public void AnimationUse()
    {
        StartCoroutine(AnimationUseEnum());
    }

    public IEnumerator AnimationUseEnum()
    {
        if(equipementOwn.side == Skill.team.Player)
        {
            Vector3 TweenPosition = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
            t.TweenPositionTo(TweenPosition, 0.2f, Easings.Ease.SmootherStep, true);
            yield return new WaitForSeconds(0.2f);
            t.TweenPositionTo(Manager.Instance.goAwayPositionPlayer[position].transform.position, 0.5f, Easings.Ease.SmootherStep, true);
        }
        else
        {
            Vector3 TweenPosition = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
            t.TweenPositionTo(TweenPosition, 0.2f, Easings.Ease.SmootherStep, true);
            yield return new WaitForSeconds(0.2f);
            t.TweenPositionTo(Manager.Instance.goAwayPositionEnemy[position].transform.position, 0.5f, Easings.Ease.SmootherStep, true);
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
                //Debug.Log("Collide");
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
                //Debug.Log("ExitCollide");
            }
        }
    }
    #endregion

}

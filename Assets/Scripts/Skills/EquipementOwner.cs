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

    private Tweener t;

    public Transform dicePosition;

    public bool diceHere;

    private void Start()
    {
        t = GetComponent<Tweener>();
        
        InitSkill();
    }
    
    public void InitSkill()
    {
        equipementOwn.equipementOwner = this;
        //Debug.Log(equipementOwn.equipementOwner);

        if (equipementOwn.conditions == Skill.conditionType.countdown)
        {
            equipementOwn.currentCountdown = equipementOwn.valueCondition;
            diceValue.text = equipementOwn.currentCountdown.ToString();
        }
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
                diceValue.text = equipementOwn.currentCountdown.ToString();
                break;
            case Skill.conditionType.pair:
                break;
            case Skill.conditionType.impair:
                break;
            case Skill.conditionType.value:
                diceValue.text = " ";
                break;
            default:
                break;
        }
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
            PlayerManager.Instance.playerEquipementOwner.Remove(gameObject.GetComponent<EquipementOwner>());
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
        else
        {
            Vector3 TweenPosition = new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z);
            t.TweenPositionTo(TweenPosition, 0.2f, Easings.Ease.SmootherStep, true);
            yield return new WaitForSeconds(0.2f);
            t.TweenPositionTo(Manager.Instance.goAwayPositionEnemy[position].transform.position, 0.5f, Easings.Ease.SmootherStep, true);
            EnemyBehaviour.Instance.enemyEquipementOwner.Remove(gameObject.GetComponent<EquipementOwner>());
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBehaviour : Singleton<EnemyBehaviour>
{
    public int health;
    public int maxHealth;

    public int initialDiceCount;
    public int numberOfDice;
    public List<DiceBehaviour> storedDice = new List<DiceBehaviour>();

    public List<EquipementOwner> enemyActualEquipement = new List<EquipementOwner>();
    public List<Skill> enemySkillList = new List<Skill>();
    public List<Skill> enemybreakSkillList = new List<Skill>();
    public List<EquipementOwner> enemyEquipementOwner = new List<EquipementOwner>();
    public List<Skill> enemySkillWithCountdown = new List<Skill>();

    public EnemyBehaviourAlt enemyAlt;

    //altération
    public int numberOfShock = 0;
    public int numberOfBreak = 0;

    public int armor;

    public bool enemyShock = false;
    public bool enemyBreak = false;

    private void Awake()
    {
        CreateSingleton(true);
    }

    private void Start()
    {
        enemyAlt = Manager.Instance.currentEnemy.GetComponent<EnemyBehaviourAlt>();
    }

    public void InitEnemy()
    {
        numberOfShock = 0;
        armor = 0;
        health = maxHealth;
    }

    public void EnemyBattle()
    {
        gameObject.GetComponent<EnemyBehaviourAlt>().EnemyBattleAlt();
    }

    #region HotHead
    public void Comp1Shoked()
    {
        bool thereIsA6 = false;

        //Trier les dés selon leur valeurs.
        Manager.Instance.enemyBehaviour.storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        //Check si ya un 6
        for (int i = 0; i < storedDice.Count; i++)
        {
            if (storedDice[i].GetComponent<DiceBehaviour>().valueDice == 6)
            {
                thereIsA6 = true;
            }
        }

        if (thereIsA6)
        {
            StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 0, 1));
            StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 2));
            return;
        }
        else
        {
            StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 1, 2));
        }
    }

    public void Comp0Shoked()
    {
        if(storedDice[1].valueDice == 6)
        {
            //Cpt0 plus worth
            StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 0, 1));
            StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 2));
        }
        else //Cpt 1 plus worth
        {
            StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 2));
        }

    }

    public void NoShock()
    {
        if (storedDice[0].valueDice >= enemySkillList[1].currentCountdown)
        {
            StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 0, 2));
            return;
        }
        else if (storedDice[1].valueDice >= enemySkillList[1].currentCountdown)
        {
            StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 0, 2));
            return;
        }
        else
        {
            if(storedDice[0].valueDice == 6)
            {
                StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 0, 2));
                return;
            }

            if (storedDice[0].valueDice + storedDice[1].valueDice >= enemySkillList[1].currentCountdown)
            {
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 1, 2));
                return;
            }
            else
            {
                StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 0, 1));
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 2));
                return;
            }
        }
    }
    #endregion

    #region Aoife

    public void RemoveShockAlt(int shockComp)
    {
        switch (shockComp)
        {
            case 0:
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 0, 1));
                enemyAlt.ContinueAnalyse(false);
            break;

            case 1:
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 1));
                enemyAlt.ContinueAnalyse(false);
                break;

            case 2:
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 1));
                enemyAlt.ContinueAnalyse(false);
                break;

            default:
                break;
        }
    }

    public void NoShockAlt()
    {

    }

    public void BreakSkill0()
    {

    }

    public void BreakSkill1()
    {

    }

    public void BreakSkill2()
    {

    }

    public void NoBreak()
    {

    }

    #endregion

    //waiting time = 0 si c'est la première action.
    public IEnumerator DiceToEquipement(GameObject selectedDice, int skillIndex, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        selectedDice.GetComponent<Tweener>().TweenPositionTo(enemyActualEquipement[skillIndex].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.75f);
        enemyEquipementOwner[skillIndex].diceOwn = selectedDice.GetComponent<DiceBehaviour>();
        RemoveChocInit(skillIndex);
    }

    public void RemoveChocInit(int index)
    {
        if (enemyEquipementOwner[index].isChoc)
        {
            enemyEquipementOwner[index].chocBehaviour.StartCoroutine(enemyEquipementOwner[index].chocBehaviour.RemoveChoc());
        }
        else
        {
            enemyEquipementOwner[index].equipementOwn.TestValue();
        }
    }

    public IEnumerator DelayCountdownEnemy(int value, EquipementOwner equipementOwner)
    {
        equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
        equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
        equipementOwner.diceOwn.canMove = false;

        equipementOwner.diceOwn.gameObject.SetActive(false);

        for (int i = 0; i < value; i++)
        {
            equipementOwner.equipementOwn.currentCountdown--;
            equipementOwner.diceValue.text = equipementOwner.equipementOwn.currentCountdown.ToString();
            yield return new WaitForSeconds(0.1f);

            if (equipementOwner.equipementOwn.currentCountdown <= 0)
            {
                equipementOwner.equipementOwn.currentCountdown = 0;
                equipementOwner.equipementOwn.Use();
                StopCoroutine(DelayCountdownEnemy(value, equipementOwner));
                break;
            }
        }
    }

    public void TakeDamages(int damages)
    {
        for (int i = 0; i < damages; i++)
        {
            if(armor > 0)
            {
                armor--;
            }
            else
            {
                health --;
            }

            
            if (health <= 0)
            {
                health = 0;
                Debug.Log("dead");
            }
            Manager.Instance.canvasManager.UpdateHealth();
        }

       
    }

    public void InitBreak(int breakTime)
    {
        for (int i = 0; i < breakTime; i++)
        {
            int indexChoose = Random.Range(0, enemySkillList.Count-1);

            enemySkillList[indexChoose].isBreak = true;
        }
    }

    public void InitShock()
    {
        //Debug.Log(numberOfShock);
        for (int i = 0; i < numberOfShock; i++)
        {
            Debug.Log(i + "InitShock");
            int indexChoose = Random.Range(0, enemySkillList.Count);

            enemySkillList[indexChoose].isShock = true;
        }
        numberOfShock = 0; 
    }
}

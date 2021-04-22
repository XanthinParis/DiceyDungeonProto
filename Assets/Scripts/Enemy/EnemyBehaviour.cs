using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Singleton<EnemyBehaviour>
{
    public int health;
    public int maxHealth;

    public int initialDiceCount;
    public int numberOfDice;
    public List<GameObject> storedDice = new List<GameObject>();

    public List<Skill> enemySkillList = new List<Skill>();
    public List<EquipementOwner> enemyEquipementOwner = new List<EquipementOwner>();
    public List<Skill> enemySkillWithCountdown = new List<Skill>();

    //altération
    public int numberOfShock = 0;

    private void Awake()
    {
        CreateSingleton(true);
    }

    public void InitEnemy()
    {
        health = maxHealth;
    }

    public void EnemyBattle()
    {
        int valueDice0 = storedDice[0].GetComponent<DiceBehaviour>().dice.value;
        int valueDice1 = storedDice[1].GetComponent<DiceBehaviour>().dice.value;

        int maxValue = 0;
        if (valueDice0 > valueDice1)
        {
            maxValue = valueDice0;
        }
        else
        {
            maxValue = valueDice1;
        }

        if (maxValue >=5 || (storedDice[0].GetComponent<DiceBehaviour>().dice.value == enemyEquipementOwner[1].equipementOwn.currentCountdown || storedDice[1].GetComponent<DiceBehaviour>().dice.value == enemyEquipementOwner[1].equipementOwn.currentCountdown))
        {
            Debug.Log("Methode1");
            StartCoroutine(FocusFirstSkill(maxValue));
        }
        else
        {
            Debug.Log("Methode2");
            StartCoroutine(FocusSecondSkill(maxValue));
        }
    }

    private IEnumerator FocusFirstSkill(int maxValue)
    {
        yield return new WaitForSeconds(1f);

        if (storedDice[0].GetComponent<DiceBehaviour>().dice.value == enemyEquipementOwner[1].equipementOwn.currentCountdown || storedDice[1].GetComponent<DiceBehaviour>().dice.value == enemyEquipementOwner[1].equipementOwn.currentCountdown)
        {
            if(storedDice[0].GetComponent<DiceBehaviour>().dice.value == enemyEquipementOwner[1].equipementOwn.currentCountdown)
            {
                StartCoroutine(Dice0Equipement1());
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Dice1Equipement0());
                yield return new WaitForSeconds(1.5f);
                Manager.Instance.EndTurn();
                yield break;
            }
            else
            {
                StartCoroutine(Dice1Equipement1());
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Dice0Equipement0());
                yield return new WaitForSeconds(1.5f);
                Manager.Instance.EndTurn();
                yield break;

            }

        }
        else
        {
            if (maxValue == storedDice[0].GetComponent<DiceBehaviour>().valueDice)
            {
                StartCoroutine(Dice0Equipement1());
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Dice1Equipement0());
                yield return new WaitForSeconds(1.5f);
                Manager.Instance.EndTurn();
                yield break;
            }
            else
            {
                StartCoroutine(Dice1Equipement1());
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(Dice0Equipement0());
                yield return new WaitForSeconds(1.5f);
                Manager.Instance.EndTurn();
                yield break;
            }
        }
    }

    private IEnumerator FocusSecondSkill(int value)
    {
        yield return new WaitForSeconds(1f);
 
        if(value == storedDice[0].GetComponent<DiceBehaviour>().valueDice)
        {
            StartCoroutine(Dice0Equipement0());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Dice1Equipement1());
            yield return new WaitForSeconds(1.5f);
            Manager.Instance.EndTurn();
            yield break;
        }
        else
        {
            StartCoroutine(Dice1Equipement1());
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Dice0Equipement0());
            yield return new WaitForSeconds(1.5f);
            Manager.Instance.EndTurn();
            yield break;
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

    public IEnumerator Dice0Equipement0()
    {
        storedDice[0].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[0].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.75f);
        enemyEquipementOwner[0].diceOwn = storedDice[0].GetComponent<DiceBehaviour>();
        RemoveChocInit(0);
    }

    public IEnumerator Dice1Equipement1()
    {
        storedDice[1].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[1].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.75f);
        enemyEquipementOwner[1].diceOwn = storedDice[1].GetComponent<DiceBehaviour>();
        RemoveChocInit(1);
    }

    public IEnumerator Dice0Equipement1()
    {
        storedDice[0].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[1].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.75f);
        enemyEquipementOwner[1].diceOwn = storedDice[0].GetComponent<DiceBehaviour>();
        RemoveChocInit(1);

    }

    public IEnumerator Dice1Equipement0()
    {
        storedDice[1].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[0].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.75f);
        enemyEquipementOwner[0].diceOwn = storedDice[1].GetComponent<DiceBehaviour>();
        RemoveChocInit(0);
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

    public void TakeDamages(int damages)
    {
        health -= damages;
        if(health <= 0)
        {
            health = 0;
            Debug.Log("dead");
        }
        Manager.Instance.canvasManager.UpdateHealth();
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
        Debug.Log(numberOfShock);
        for (int i = 0; i < numberOfShock; i++)
        {
            Debug.Log(i + "InitShock");
            int indexChoose = Random.Range(0, enemySkillList.Count - 1);

            enemySkillList[indexChoose].isShock = true;
        }
        numberOfShock = 0; 
    }
}

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

        if (maxValue >=5)
        {
            Debug.Log("Methode1");
            StartCoroutine(FocusFirstSkill());
        }
        else
        {
            Debug.Log("Methode2");
            StartCoroutine(FocusSecondSkill());
        }
    }

    private IEnumerator FocusFirstSkill()
    {
        storedDice[0].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[0].dicePosition.transform.position,1f,Easings.Ease.SmoothStep,true);
        yield return new WaitForSeconds(1f);
        enemyEquipementOwner[0].diceOwn = storedDice[0].GetComponent<DiceBehaviour>();
        enemyEquipementOwner[0].equipementOwn.TestValue();
        yield return new WaitForSeconds(0.2f);
        storedDice[1].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[1].dicePosition.transform.position, 1f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(1f);
        enemyEquipementOwner[1].diceOwn = storedDice[1].GetComponent<DiceBehaviour>();
        enemyEquipementOwner[1].equipementOwn.TestValue();
        yield return new WaitForSeconds(0.2f);
        Manager.Instance.EndTurn();
    }

    private IEnumerator FocusSecondSkill()
    {
        storedDice[1].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[1].dicePosition.transform.position, 1f, Easings.Ease.SmootherStep, true);
        yield return new WaitForSeconds(1f);
        enemyEquipementOwner[1].diceOwn = storedDice[1].GetComponent<DiceBehaviour>();
        enemyEquipementOwner[1].equipementOwn.TestValue();
        yield return new WaitForSeconds(0.2f);
        storedDice[0].GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[0].dicePosition.transform.position, 1f, Easings.Ease.SmootherStep, true);
        yield return new WaitForSeconds(1f);
        enemyEquipementOwner[0].diceOwn = storedDice[0].GetComponent<DiceBehaviour>();
        enemyEquipementOwner[0].equipementOwn.TestValue();
        yield return new WaitForSeconds(0.2f);
        Manager.Instance.EndTurn();
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

    public void InitShock(int shockTime)
    {
        for (int i = 0; i < shockTime; i++)
        {
            int indexChoose = Random.Range(0, enemySkillList.Count - 1);

            enemySkillList[indexChoose].isShock = true;
        }
    }
}

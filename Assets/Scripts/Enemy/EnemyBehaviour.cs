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

    public List<Skill> enemyActualEquipement = new List<Skill>();
    public List<Skill> enemySkillList = new List<Skill>();
    public List<Skill> enemybreakSkillList = new List<Skill>();
    public List<EquipementOwner> enemyEquipementOwner = new List<EquipementOwner>();
    public List<Skill> enemySkillWithCountdown = new List<Skill>();

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

    public void Comp1Shoked()
    {
        bool thereIsA6 = false;
        GameObject stored6 = null;
        GameObject otherDice0 = null;
        GameObject otherDice1 = null;
        int maxValue = 0;

        int valueDice0 = storedDice[0].GetComponent<DiceBehaviour>().dice.value;
        int valueDice1 = storedDice[1].GetComponent<DiceBehaviour>().dice.value;

        if (valueDice0 > valueDice1)
        {
            maxValue = valueDice0;
        }
        else
        {
            maxValue = valueDice1;
        }


        //Check si ya un 6
        for (int i = 0; i < storedDice.Count; i++)
        {
            if (storedDice[i].GetComponent<DiceBehaviour>().valueDice == 6)
            {
                if (stored6 != null)
                {
                    stored6 = storedDice[i];
                }
                else
                {
                    otherDice0 = storedDice[i];
                }

                thereIsA6 = true;
            }
            else
            {
                if (otherDice0 != null)
                {
                    otherDice0 = storedDice[i];
                }
                else
                {
                    otherDice1 = storedDice[i];
                }
            }
        }

        if (thereIsA6)
        {
            StartCoroutine(DiceToEquipement(stored6, 0, 1));
            StartCoroutine(DiceToEquipement(otherDice0, 1, 2));
            return;
        }
        else
        {

            if (maxValue == storedDice[0].GetComponent<DiceBehaviour>().valueDice)
            {
                StartCoroutine(DiceToEquipement(storedDice[1], 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[0], 1, 2));
            }
            else
            {
                StartCoroutine(DiceToEquipement(storedDice[0], 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[1], 1, 2));
            }
        }
    }

    public void Comp0Shoked()
    {
        int maxValue = 0;

        int valueDice0 = storedDice[0].GetComponent<DiceBehaviour>().dice.value;
        int valueDice1 = storedDice[1].GetComponent<DiceBehaviour>().dice.value;

        if (valueDice0 > valueDice1)
        {
            maxValue = valueDice0;
           
        }
        else
        {
            maxValue = valueDice1;
        }

        if(maxValue == 6)
        {
            //Cpt0 plus worth
            if(valueDice0 == maxValue)
            {
                StartCoroutine(DiceToEquipement(storedDice[0], 0, 1));
                StartCoroutine(DiceToEquipement(storedDice[1], 1, 2));
            }
            else
            {
                StartCoroutine(DiceToEquipement(storedDice[1], 0, 1));
                StartCoroutine(DiceToEquipement(storedDice[0], 1, 2));
            }
        }
        else //Cpt 1 plus worth
        {
            if(valueDice0 == maxValue)
            {
                StartCoroutine(DiceToEquipement(storedDice[1], 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[0], 1, 2));
            }
            else
            {
                StartCoroutine(DiceToEquipement(storedDice[0], 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[1], 1, 2));
            }
        }
        
    }

    public void NoShock()
    {
        #region Test
        int maxValue = 0;

        int valueDice0 = storedDice[0].GetComponent<DiceBehaviour>().dice.value;
        int valueDice1 = storedDice[1].GetComponent<DiceBehaviour>().dice.value;

        if (valueDice0 > valueDice1)
        {
            maxValue = valueDice0;
        }
        else
        {
            maxValue = valueDice1;
        }
        #endregion

        if (valueDice0 >= enemySkillList[1].currentCountdown)
        {
            StartCoroutine(DiceToEquipement(storedDice[0], 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[1], 0, 2));
            Debug.Log("1");
            return;
        }
        else if (valueDice1 >= enemySkillList[1].currentCountdown)
        {
            StartCoroutine(DiceToEquipement(storedDice[1], 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[0], 0, 2));
            Debug.Log("2");
            return;
        }
        else
        {
            if(maxValue == 6)
            {
                if (maxValue == valueDice0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0], 0, 1));
                    StartCoroutine(DiceToEquipement(storedDice[1], 1, 2));
                    return;
                }
                else
                {
                    StartCoroutine(DiceToEquipement(storedDice[1], 0, 1));
                    StartCoroutine(DiceToEquipement(storedDice[0], 1, 2));
                    return;
                }
            }

            if(valueDice0 + valueDice1 >= enemySkillList[1].currentCountdown)
            {
                StartCoroutine(DiceToEquipement(storedDice[0], 1, 1));
                StartCoroutine(DiceToEquipement(storedDice[1], 1, 2));
                Debug.Log("3");
                return;
            }
            else
            {
                if (valueDice0 == maxValue)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0], 0, 1));
                    StartCoroutine(DiceToEquipement(storedDice[1], 1, 2));
                    Debug.Log("4");
                    return;
                }
                else
                {
                    StartCoroutine(DiceToEquipement(storedDice[1], 0, 1));
                    StartCoroutine(DiceToEquipement(storedDice[0], 1, 2));
                    Debug.Log("5");
                    return;
                }
            }
        }
    }
   
    //waiting time = 0 si c'est la première action.
    public IEnumerator DiceToEquipement(GameObject selectedDice, int skillIndex, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        selectedDice.GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[skillIndex].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
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

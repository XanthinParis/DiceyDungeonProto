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

    public bool enemyShock = false;

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

        for (int i = 0; i < enemySkillList.Count; i++)
        {
            if (enemySkillList[i].isShock)
            {
                enemyShock = true;
            }
        }

        //Si l'ennemy a une compétence en Choc, il faut voir ce qui est le plus rentable pour lui de faire.  
        if (enemyShock)
        {
            if (enemySkillList[1].isShock)   // Si c'est la compétence 1 qui est choc, il faut que check si un des valeurs des deux dés est suffisantes pour que ca soit worth.
            {
                Comp1Shoked();
            }
            else //Dans le cas ou la compétence 0 est sous Choc
            {
                Comp0Shoked();
            }
        }
        else
        {
            NoShock();
        }
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

        if (valueDice0 == enemySkillList[1].currentCountdown)
        {
            StartCoroutine(DiceToEquipement(storedDice[0], 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[1], 0, 2));
        }
        else if (valueDice1 == enemySkillList[1].currentCountdown)
        {
            StartCoroutine(DiceToEquipement(storedDice[1], 1, 1));
            StartCoroutine(DiceToEquipement(storedDice[0], 0, 2));
        }
        else
        {
            if (valueDice0 == maxValue)
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
    }
   
    //waiting time = 0 si c'est la première action.
    public IEnumerator DiceToEquipement(GameObject selectedDice, int skillIndex, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        selectedDice.GetComponent<Tweener>().TweenPositionTo(enemyEquipementOwner[skillIndex].dicePosition.transform.position, 0.75f, Easings.Ease.SmoothStep, true);
        yield return new WaitForSeconds(0.75f);
        enemyEquipementOwner[0].diceOwn = selectedDice.GetComponent<DiceBehaviour>();
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
            int indexChoose = Random.Range(0, enemySkillList.Count);

            enemySkillList[indexChoose].isShock = true;
        }
        numberOfShock = 0; 
    }
}

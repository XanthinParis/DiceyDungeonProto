using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : Singleton<PlayerManager>
{
    public int initialDiceCount;
    public int numberOfDice;

    public float health;
    public float maxHealth;

    public int LimitBreakPV;
    public int currentLimitBreakPV;
    public bool limitBreakAvailable = false;

    public List<GameObject> storedDice = new List<GameObject>();

    public List<Skill> playerSkills = new List<Skill>();
    public List<EquipementOwner> playerEquipementOwner = new List<EquipementOwner>();
    public List<Skill> playerSkillsWithCountDown = new List<Skill>();

    public bool isEsquive = false;
    public int directRepetition = 0;

    public int numberOfBurn = 0;

    private void Awake()
    {
        CreateSingleton(true);
    }

    public void InitPlayer()
    {
        health = maxHealth;
        directRepetition = 0;
    }

    public void InitFireAlt(int number)
    {
        numberOfBurn += number;
    }

    public void TakeDamages(int damages)
    {
        health -= damages;
        currentLimitBreakPV -= damages;

        if (currentLimitBreakPV < 0)
        {
            Manager.Instance.canvasManager.UpdateLimitBreakVisuel();
            limitBreakAvailable = true;
            currentLimitBreakPV = LimitBreakPV;
        }

        if (health <= 0)
        {
            health = 0;
        }
        Manager.Instance.canvasManager.UpdateHealth();
    }

    //S'active quand le joueur appuie sur le bouton;
    public void LimitBreak()
    {
        if (limitBreakAvailable)
        {
            limitBreakAvailable = false;
            Manager.Instance.canvasManager.UpdateLimitBreakVisuel();

            for (int i = 0; i < playerSkillsWithCountDown.Count; i++)
            {
                if (playerSkillsWithCountDown[i].currentlyOnField == true)
                {
                    StartCoroutine(DelayCountdown(3, playerSkillsWithCountDown[i].equipementOwner));
                }
            }
        }
    }

    //Delay le compteur visuel pour un feedback sympa;
    public IEnumerator DelayCountdown(int value, EquipementOwner equipementOwner)
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
                StopCoroutine(DelayCountdown(value,equipementOwner));
                break;
            }
            
        }

       
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Values")]
    public int initialDiceCount;
    public int numberOfDice;
    public float health;
    public float maxHealth;

    [Header("Limit Break")]
    public int LimitBreakPV;
    public int currentLimitBreakPV;
    public bool limitBreakAvailable = false;

    [Header("Dices")]
    public List<GameObject> storedDice = new List<GameObject>();

    [Header("Skills")]
    public List<Skill> playerSkills = new List<Skill>();
    public List<EquipementOwner> playerEquipementOwner = new List<EquipementOwner>();
    public List<Skill> playerSkillsWithCountDown = new List<Skill>();

    [Header("SkillsStuff")]
    public bool isEsquive = false;
    public int directRepetition = 0;
    public int numberOfBurn = 0;
    public bool comesFromLimitBreak = false;

    [Header("Materials")]
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Image imageRend;

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
        if (isEsquive)
        {
            isEsquive = false;
            Manager.Instance.canvasManager.UpdateHealth();
        }
        else
        {
            health -= damages;
            currentLimitBreakPV -= damages;

            if (currentLimitBreakPV <= 0)
            {
                limitBreakAvailable = true;
                currentLimitBreakPV = LimitBreakPV;
                Manager.Instance.canvasManager.UpdateLimitBreakVisuel();
            }

            if (health <= 0)
            {
                health = 0;
            }
            Manager.Instance.canvasManager.UpdateHealth();

            //Lancer une fonction pour Afficher l'int.
            StartCoroutine(SwapColor(3));
        }
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
                    comesFromLimitBreak = true;
                    StartCoroutine(DelayCountdown(3, playerSkillsWithCountDown[i].equipementOwner));
                }
            }
        }
    }

    //Delay le compteur visuel pour un feedback sympa;
    public IEnumerator DelayCountdown(int value, EquipementOwner equipementOwner)
    {
        if (!comesFromLimitBreak)
        {
            equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
            equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
            equipementOwner.diceOwn.canMove = false;

            equipementOwner.diceOwn.gameObject.SetActive(false);
        }


        for (int i = 0; i < value; i++)
        {
            if(equipementOwner.equipementOwn.countdownUsed == false)
            {
                equipementOwner.equipementOwn.currentCountdown--;
                equipementOwner.diceValue.text = equipementOwner.equipementOwn.currentCountdown.ToString();
            }

            if (equipementOwner.equipementOwn.currentCountdown <= 0 && equipementOwner.equipementOwn.countdownUsed == false)
            {
                equipementOwner.equipementOwn.countdownUsed = true;
                equipementOwner.equipementOwn.currentCountdown = 0;
                comesFromLimitBreak = false;
                
                equipementOwner.equipementOwn.Use();
                StopCoroutine(DelayCountdown(value, equipementOwner));
            }
            yield return new WaitForSeconds(0.1f);
        }

        if (comesFromLimitBreak)
        {
            comesFromLimitBreak = false;
        }

        
    }

    private IEnumerator SwapColor(int time)
    {
        for (int i = 0; i < time; i++)
        {
            yield return new WaitForSeconds(0.05f);
            imageRend.material = baseMaterial;
            yield return new WaitForSeconds(0.05f);
            imageRend.material = whiteMaterial;
        }

        yield return new WaitForSeconds(0.05f);
        imageRend.material = baseMaterial;
    }
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class EnemyBehaviour : Singleton<EnemyBehaviour>
{
    [Header("Values")]
    public int health;
    public int maxHealth;
    public int initialDiceCount;
    public int numberOfDice;

    [Header("Dices")]
    public List<DiceBehaviour> storedDice = new List<DiceBehaviour>();
    public List<DiceBehaviour> storedDiceSecurity = new List<DiceBehaviour>();

    [Header("Equipements")]
    public List<EquipementOwner> enemyActualEquipement = new List<EquipementOwner>();
    public List<EquipementOwner> enemyEquipementOwner = new List<EquipementOwner>();
    public List<Skill> enemySkillList = new List<Skill>();
    public List<Skill> enemybreakSkillList = new List<Skill>();
    public List<Skill> enemySkillWithCountdown = new List<Skill>();

    //Change le fonctionnement de l'ennemi en fonction de qui il est.
    public EnemyBehaviourAlt enemyAlt;

    [Header("Alétérations")]
    public int numberOfShock = 0;
    public int numberOfBreak = 0;
    public int intBreak = 3;
    public int intShock = 3;

    public int armor = 0;

    public bool enemyShock = false;
    public bool enemyBreak = false;


    [Header("Materials")]
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private Image imageRend;

    [SerializeField] private GameObject damageFeedbackEnemy;
    private void Awake()
    {
        CreateSingleton(true);
    }

    private void Start()
    {
        enemyAlt = Manager.Instance.currentEnemy.GetComponent<EnemyBehaviourAlt>();
    }

    //Initialisation de l'ennemi;
    public void InitEnemy()
    {
        numberOfShock = 0;
        armor = 0;
        health = maxHealth;

        enemyShock = false;
        enemyBreak = false;
    }

    public void EnemyBattle()
    {
        gameObject.GetComponent<EnemyBehaviourAlt>().EnemyBattleAlt();
    }

    //Lance l'IA selon le type de l'ennemi : Ennemi HOTHEAD;
    #region HotHead

    //Compétence1 Choc.
    public void Comp1Shoked()
    {
        //Trier les dés selon leur valeurs.
        Manager.Instance.enemyBehaviour.storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        Debug.Log("Comp1Shoked");
        Debug.Log(Manager.Instance.enemyBehaviour.storedDice[1]);
        Debug.Log(Manager.Instance.enemyBehaviour.storedDice[0]);
    }

    //Compétence0 Choc.
    public void Comp0Shoked()
    {
        //Trier les dés selon leur valeurs.
        Manager.Instance.enemyBehaviour.storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        Debug.Log("Comp0Shoked");
        Debug.Log(Manager.Instance.enemyBehaviour.storedDice[1]);
        Debug.Log(Manager.Instance.enemyBehaviour.storedDice[0]);
    }

    //Pas de Compétence Choc.
    public void NoShock()
    {
        Manager.Instance.enemyBehaviour.storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        Debug.Log("NoShocked");
        Debug.Log(Manager.Instance.enemyBehaviour.storedDice[1]);
        Debug.Log(Manager.Instance.enemyBehaviour.storedDice[0]);
    }
    #endregion

    //Lance l'IA selon le type de l'ennemi : Ennemi AOIFE;
    #region Aoife

    //Remove la compétence Choc
    public void RemoveShockAlt(int shockComp)
    {
        switch (shockComp)
        {
            case 0:
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 0, 1));
                enemyAlt.ContinueAnalyse(false);
                intShock = 4;
                Debug.Log("RemoveChoc0");
                break;

            case 1:
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 1, 1));
                enemyAlt.ContinueAnalyse(false);
                Debug.Log("RemoveChoc1");
                intShock = 4;
                break;

            case 2:
                StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 1));
                enemyAlt.ContinueAnalyse(false);
                Debug.Log("RemoveChoc2");
                intShock = 4;
                break;

            default:
                break;
        }
    }

    public void BreakSkill0()
    {
        Debug.Log("BreakSkill0");

        //Le meilleur dice va dans la compétence 0;
        StartCoroutine(DiceToEquipement(storedDice[storedDice.Count-1].gameObject, 0, 2)); //Il reste 3 Dés ou 4 si y'a pas eut de choc;
        storedDice.Remove(storedDice[storedDice.Count - 1]);

        //ReTrier 
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        //Les deux suivants si ils sont pas égales à 6 ils vont dans la comp 1;

        bool ChosseOne = false;
        for (int i = storedDice.Count-1; i >= 0; i--)
        {
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 3));   //Il reste entre 2 et 3 Dés si y'a pas eut de choc & si les dés ne sont pas des 6; 
                                                                                    
                storedDice.Remove(storedDice[i]);
            }

        }

        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        ChosseOne = false;

        for (int i = storedDice.Count - 1; i >= 0; i--)
        {
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 4)); //Il reste entre 2 et 1 Dés si y'a pas eut de choc & si les dés ne sont pas des 6; 
                storedDice.Remove(storedDice[i]);
            }
        }

        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        ChosseOne = false;

        //switch pour la dernière compétence.
        switch (storedDice.Count)
        {

            case 3:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[2].gameObject, 2, 5));
                    storedDice.Remove(storedDice[2]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 2:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 1:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 6));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            default:
                break;
        }

    }

    public void BreakSkill1()
    {
        Debug.Log("BreakSkill1");

        //Le meilleur dice va dans la compétence 0;
        StartCoroutine(DiceToEquipement(storedDice[storedDice.Count - 1].gameObject, 0, 2)); //Il reste 3 Dés;
        storedDice.Remove(storedDice[storedDice.Count - 1]);
        //ReTrier 
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        bool ChosseOne = false;
        for (int i = storedDice.Count - 1; i >= 0; i--)
        {
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 3));   //Il reste 2 Dés;
                storedDice.Remove(storedDice[i]);
            }

        }
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        //switch pour la dernière compétence.
        switch (storedDice.Count)
        {

            case 3:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[2].gameObject, 2, 5));
                    storedDice.Remove(storedDice[2]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 2:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 1:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }
                break;
            default:
                break;
        }
    }

    public void BreakSkill2()
    {
        Debug.Log("BreakSkill2");
        //Le meilleur dice va dans la compétence 0;
        StartCoroutine(DiceToEquipement(storedDice[storedDice.Count - 1].gameObject, 0, 2)); //Il reste 3/4 Dés;

        //ReTrier 
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        //Les deux suivants si ils sont pas égales à 6 ils vont dans la comp 1;

        bool ChosseOne = false;
        for (int i = storedDice.Count - 1; i >= 0; i--)
        {
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 3));   //Il reste 2/3 Dés;
                storedDice.Remove(storedDice[i]);
            }

        }
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        ChosseOne = false;

        for (int i = storedDice.Count - 1; i >= 0; i--)
        {
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 4)); //Il reste 1/2 Dés;
                storedDice.Remove(storedDice[i]);
            }
        }
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        ChosseOne = false;

        //switch pour la dernière compétence.
        switch (storedDice.Count)
        {

            case 3:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[2].gameObject, 2, 5));
                    storedDice.Remove(storedDice[2]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 2:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 1:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }
                break;
            default:
                break;
        }


    }

    public void NoBreak()
    {
        Debug.Log("NoBreak");
        //Le meilleur dice va dans la compétence 0;
        StartCoroutine(DiceToEquipement(storedDice[storedDice.Count - 1].gameObject, 0, 2)); 
        storedDice.Remove(storedDice[storedDice.Count - 1]);

        //ReTrier 
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        //Les deux suivants si ils sont pas égales à 6 ils vont dans la comp 1;

        bool ChosseOne = false;
        for (int i = storedDice.Count - 1; i >= 0; i--)
        {
            //Debug.Log(i);
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 3));   
                storedDice.Remove(storedDice[i]);

            }
        }
        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        ChosseOne = false;

        for (int i = storedDice.Count - 1; i >= 0; i--)
        {
            if (storedDice[i].valueDice != 6 && !ChosseOne)
            {
                ChosseOne = true;
                StartCoroutine(DiceToEquipement(storedDice[i].gameObject, 1, 4)); 
                storedDice.Remove(storedDice[i]);
            }
        }

        storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();

        ChosseOne = false;

        //switch pour la dernière compétence.
        switch (storedDice.Count)
        {

            case 3:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[2].gameObject, 2, 5));
                    storedDice.Remove(storedDice[2]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 2:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[1].gameObject, 2, 6));
                    storedDice.Remove(storedDice[1]);
                }

                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 7));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            case 1:
                Debug.Log("switch");
                if (enemyActualEquipement[2].equipementOwn.currentCountdown != 0)
                {
                    StartCoroutine(DiceToEquipement(storedDice[0].gameObject, 2, 6));
                    storedDice.Remove(storedDice[0]);
                }
                break;
            default:
                break;
        }
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

    // A lancer pour activer la compétence ou remove l'altération Choc.
    public void RemoveChocInit(int index)
    {
        if (enemyActualEquipement[index].isChoc)
        {
            enemyActualEquipement[index].chocBehaviour.StartCoroutine(enemyEquipementOwner[index].chocBehaviour.RemoveChoc());
            enemyActualEquipement[index].isChoc = false;
        }
        else
        {
            enemyActualEquipement[index].equipementOwn.TestValue();
        }
    }

    //A activer pour réduire le coundown d'un équipement
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

    //A Appeler lorsque l'enemy prend des dégats.
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
            StartCoroutine(SwapColor(2));
            InstantiateDamageFeedback(damages);
        }

       
    }


    //Faire qu'une compétence soit affaiblie.
    public void InitBreak()
    {
        for (int i = 0; i < numberOfBreak; i++)
        {
            int indexChoose = Random.Range(0, 2);
            Debug.Log("indexChoose" + indexChoose);
            intBreak = indexChoose;
            enemyBreak = true;
            
        }
        numberOfBreak = 0;
        
    }

    //Faire qu'une compétence soit Choc (demande un dé pour être réactiver).
    public void InitShock()
    {
        for (int i = 0; i < numberOfShock; i++)
        {
            int indexChoose = Random.Range(0, 2);
            Debug.Log("indexChoose"+indexChoose);
            intShock = indexChoose; 
            enemyShock = true;
        }
        numberOfShock = 0;
    }

    public void InstantiateDamageFeedback(int damage)
    {
        GameObject feedback =  Instantiate(damageFeedbackEnemy, Manager.Instance.canvasManager.enemyDamageFeedbackPos.transform.position, Quaternion.identity);
        feedback.transform.SetParent(Manager.Instance.canvasManager.enemyDamageFeedbackPos.transform);
        feedback.GetComponent<FeedBackDamagePlayer>().damageText.text = "-" + damage;
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

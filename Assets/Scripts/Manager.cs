using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script gère la globalité des références entre les scripts.
/// </summary>

public class Manager : Singleton<Manager>
{
    public bool blockAction = false;

    public bool firstTurn = true;

    [Header("Gameplay")]
    public DiceManager diceManager;

    [Header("UI")]
    public GameCanvasManager canvasManager;

    [Header("Character")]
    public PlayerManager playerManager;
    public GameObject currentEnemy;
    public EnemyBehaviour enemyBehaviour;
    
    [Header("Cursor")]    
    public CursorBehaviour cursorBehaviour;
    public GameObject cursor;

    [Header("Skill Template")]
    [SerializeField] private GameObject smallSkill;
    [SerializeField] private GameObject bigSkill;

    public enum GameTurn { Player,Enemy};
    public GameTurn turn;

    #region SkillPosition
    [Header("SkillPosition")]
    [SerializeField] private GameObject smallSkillParentPositionPlayer;
    public List<Transform> smallSkillPositionPlayer = new List<Transform>();
    [SerializeField] private GameObject bigSkillParentPositionPlayer;
    public List <Transform> bigSkillPositionPlayer = new List<Transform>();
    [SerializeField] private GameObject goAwayParentPlayer;
    public List<Transform> goAwayPositionPlayer = new List<Transform>();

    [Header("SkillPosition")]
    [SerializeField] private GameObject smallSkillParentPositionEnemy;
    public List<Transform> smallSkillPositionEnemy = new List<Transform>();
    [SerializeField] private GameObject bigSkillParentPositionEnemy;
    public List<Transform> bigSkillPositionEnemy = new List<Transform>();
    [SerializeField] private GameObject goAwayParentEnemy;
    public List<Transform> goAwayPositionEnemy = new List<Transform>();
    #endregion

    private void Awake()
    {
        enemyBehaviour = currentEnemy.GetComponent<EnemyBehaviour>();
    }

    private void Start()
    {
        StartCoroutine(StartEnum());
    }

    private IEnumerator StartEnum()
    {
        yield return new WaitForSeconds(0.1f);
        #region PlayerPositionref
        for (int i = 0; i < smallSkillParentPositionPlayer.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            smallSkillPositionPlayer.Add(smallSkillParentPositionPlayer.GetComponent<GetPositions>().waypointsPosition[i]);
        }

        for (int i = 0; i < bigSkillParentPositionPlayer.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            bigSkillPositionPlayer.Add(bigSkillParentPositionPlayer.GetComponent<GetPositions>().waypointsPosition[i]);
        }

        for (int i = 0; i < goAwayParentPlayer.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            goAwayPositionPlayer.Add(goAwayParentPlayer.GetComponent<GetPositions>().waypointsPosition[i]);
        }
        #endregion

        #region EnemyPositionRef
        for (int i = 0; i < smallSkillParentPositionEnemy.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            smallSkillPositionEnemy.Add(smallSkillParentPositionEnemy.GetComponent<GetPositions>().waypointsPosition[i]);
        }

        for (int i = 0; i < bigSkillParentPositionEnemy.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            bigSkillPositionEnemy.Add(bigSkillParentPositionEnemy.GetComponent<GetPositions>().waypointsPosition[i]);
        }

        for (int i = 0; i < goAwayParentEnemy.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            goAwayPositionEnemy.Add(goAwayParentEnemy.GetComponent<GetPositions>().waypointsPosition[i]);
        }
        #endregion

        InitialiseCombat();
    }

    private void InitialiseCombat()
    {
        playerManager.InitPlayer();
        enemyBehaviour.InitEnemy();
        InitPlayerSkill();
        InitEnemySkill();
        diceManager.GenerateDicePlayer();
    }

    public void InitPlayerSkill()
    {
        int numberOfBig = 0;

        ///Le Gadget est placé dans le tableau de skill du player.

        //Instanciate big skill before small one in Order to put then in the proper place.
        for (int i = 0; i < playerManager.playerSkills.Count; i++)
        {
            if (playerManager.playerSkills[i].isBig)
            {
                GameObject InitSkill = Instantiate(bigSkill, bigSkillPositionPlayer[numberOfBig].position, Quaternion.identity);
                

                EquipementOwner equipOwner = InitSkill.GetComponent<EquipementOwner>();
                playerManager.playerEquipementOwner.Add(equipOwner);

                equipOwner.equipementOwn = playerManager.playerSkills[i];
                equipOwner.UpdateVisuel();
                equipOwner.equipementOwn.currentlyOnField = true;

                equipOwner.equipementOwn.initSkillValue();

                equipOwner.position = i;
                
                numberOfBig++;
            }
        }

        int smallCount = 0;
        //Instancier les petits skills après les grands.
        for (int i = 0; i < playerManager.playerSkills.Count; i++)
        {
            if (playerManager.playerSkills[i].isBig == false)
            {
                GameObject InitSkill = Instantiate(smallSkill, smallSkillPositionPlayer[i+ numberOfBig].position, Quaternion.identity);
                EquipementOwner equipOwner = InitSkill.GetComponent<EquipementOwner>();
                playerManager.playerEquipementOwner.Add(equipOwner);

                equipOwner.equipementOwn = playerManager.playerSkills[i];
                equipOwner.UpdateVisuel();
                equipOwner.equipementOwn.currentlyOnField = true;
                equipOwner.equipementOwn.initSkillValue();

                equipOwner.position = numberOfBig;
                smallCount++;
                if (smallCount ==2)
                {
                    smallCount = 0;
                    numberOfBig++;
                }
                
            }
        }

        for (int i = 0; i < playerManager.playerSkills.Count; i++)
        {
            if (playerManager.playerSkills[i].conditions == Skill.conditionType.countdown)
            {
                playerManager.playerSkillsWithCountDown.Add(playerManager.playerSkills[i]);
            }
        }

        Manager.Instance.firstTurn = false;
        numberOfBig = 0;
        smallCount = 0;
    }

    public void InitEnemySkill()
    {
        int numberOfBig = 0;

        ///Le Gadget est placé dans le tableau de skill du player.

        //Instanciate big skill before small one in Order to put then in the proper place.
        for (int i = 0; i < enemyBehaviour.enemySkillList.Count; i++)
        {
            if (enemyBehaviour.enemySkillList[i].isBig)
            {
                GameObject InitSkill = Instantiate(bigSkill, bigSkillPositionEnemy[numberOfBig].position, Quaternion.identity);
                EquipementOwner equipOwner = InitSkill.GetComponent<EquipementOwner>();
                enemyBehaviour.enemyEquipementOwner.Add(equipOwner);
                equipOwner.equipementOwn = enemyBehaviour.enemySkillList[i];
                equipOwner.UpdateVisuel();
                equipOwner.equipementOwn.currentlyOnField = true;
                equipOwner.equipementOwn.initSkillValue();
                equipOwner.position = i;
                numberOfBig++;
            }
        }

        int smallCount = 0;
        //Instancier les petits skills après les grands.
        for (int i = 0; i < enemyBehaviour.enemySkillList.Count; i++)
        {
            if (!enemyBehaviour.enemySkillList[i].isBig)
            {
                GameObject InitSkill = Instantiate(smallSkill, smallSkillPositionEnemy[i + numberOfBig].position, Quaternion.identity);
                EquipementOwner equipOwner = InitSkill.GetComponent<EquipementOwner>();
                enemyBehaviour.enemyEquipementOwner.Add(equipOwner);
                equipOwner.equipementOwn = enemyBehaviour.enemySkillList[i];
                equipOwner.UpdateVisuel();
                equipOwner.equipementOwn.currentlyOnField = true;
                equipOwner.equipementOwn.initSkillValue();

                equipOwner.position = numberOfBig;
                smallCount++;
                if (smallCount == 2)
                {
                    smallCount = 0;
                    numberOfBig++;
                }

            }
        }

        for (int i = 0; i < enemyBehaviour.enemySkillList.Count; i++)
        {
            if (enemyBehaviour.enemySkillList[i].conditions == Skill.conditionType.countdown)
            {
                enemyBehaviour.enemySkillWithCountdown.Add(enemyBehaviour.enemySkillList[i]);
            }
        }

        numberOfBig = 0;
        smallCount = 0;
    }

    public void EndTurn()
    {
        StartCoroutine(EndTurnEnum());
    }

    private IEnumerator EndTurnEnum()
    {
        if (turn == GameTurn.Player) // Fin du tour du player
        {
            turn = GameTurn.Enemy;
            //Reset competences
            playerManager.playerSkillsWithCountDown.Clear();
            for (int i = 0; i < playerManager.playerEquipementOwner.Count; i++)
            {
                playerManager.playerEquipementOwner[i].AnimationUse();
            }

            
            diceManager.ResetDicePlayer();

            canvasManager.SwitchCamera();
            diceManager.GenerateDiceEnemy();
            canvasManager.upCross.SetActive(false);
            canvasManager.downCross.SetActive(false);
            enemyBehaviour.EnemyBattle();
        }
        else                        // Fin du tour de l'enemy.
        {
            enemyBehaviour.enemySkillWithCountdown.Clear();
            for (int i = 0; i < enemyBehaviour.enemyEquipementOwner.Count; i++)
            {
                enemyBehaviour.enemyEquipementOwner[i].AnimationUse();
            }

            diceManager.ResetDiceEnemy();
            canvasManager.SwitchCamera();
            diceManager.GenerateDicePlayer();
            InitPlayerSkill();
            turn = GameTurn.Player;
            yield return new WaitForSeconds(1f);
            InitEnemySkill();
            canvasManager.upCross.SetActive(true);
        }
    }
}

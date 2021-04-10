﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ce script gère la globalité des références entre les scripts.
/// </summary>

public class Manager : Singleton<Manager>
{
    [Header("Gameplay")]
    public DiceManager diceManager;
    
    [Header("Character")]
    public PlayerManager playerManager;
    public EnemyBehaviour enemyBehaviour;
    
    [Header("Cursor")]    
    public CursorBehaviour cursorBehaviour;
    public GameObject cursor;

    [Header("Skill Template")]
    [SerializeField] private GameObject smallSkill;
    [SerializeField] private GameObject bigSkill;

    [Header("SkillPosition")]
    [SerializeField] private GameObject smallSkillParentPosition;
    public List<Transform> smallSkillPosition = new List<Transform>();
    [SerializeField] private GameObject bigSkillParentPosition;
    public List <Transform> bigSkillPosition = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < smallSkillParentPosition.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            smallSkillPosition.Add(smallSkillParentPosition.GetComponent<GetPositions>().waypointsPosition[i]);
        }

        for (int i = 0; i < bigSkillParentPosition.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            bigSkillPosition.Add(bigSkillParentPosition.GetComponent<GetPositions>().waypointsPosition[i]);
        }

        InitialiseCombat();
    }

    private void InitialiseCombat()
    {
        playerManager.InitPlayer();
        enemyBehaviour.InitEnemy();
        InitSkill();
    }

    public void InitSkill()
    {
        int numberOfBig = 0;

        ///Le Gadget est placé dans le tableau de skill du player.

        //Instanciate big skill before small one in Order to put then in the proper place.
        for (int i = 0; i < playerManager.playerSkills.Count; i++)
        {
            if (playerManager.playerSkills[i].isBig)
            {
                GameObject InitSkill = Instantiate(bigSkill, bigSkillPosition[numberOfBig].position, Quaternion.identity);
                EquipementOwner equipOwner = InitSkill.GetComponent<EquipementOwner>();

                equipOwner.equipementOwn = playerManager.playerSkills[i];
                equipOwner.UpdateVisuel();
                equipOwner.equipementOwn.currentlyOnField = true;
                numberOfBig++;
            }
        }

        //Instancier les petits skills après les grands.
        for (int i = 0; i < playerManager.playerSkills.Count; i++)
        {
            if (playerManager.playerSkills[i].isBig == false)
            {
                GameObject InitSkill = Instantiate(smallSkill, smallSkillPosition[i+ numberOfBig].position, Quaternion.identity);
                EquipementOwner equipOwner = InitSkill.GetComponent<EquipementOwner>();

                equipOwner.equipementOwn = playerManager.playerSkills[i];
                equipOwner.UpdateVisuel();
                equipOwner.equipementOwn.currentlyOnField = true;
            }
        }

        for (int i = 0; i < playerManager.playerSkills.Count; i++)
        {
            if (playerManager.playerSkills[i].conditions == Skill.conditionType.countdown)
            {
                playerManager.playerSkillsWithCountDown.Add(playerManager.playerSkills[i]);
            }
        }

        numberOfBig = 0;
    }
}

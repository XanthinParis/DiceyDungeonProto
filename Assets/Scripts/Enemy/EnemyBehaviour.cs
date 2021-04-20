using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Singleton<EnemyBehaviour>
{
    public int health;
    public int maxHealth;

    public List<Skill> enemySkillList = new List<Skill>();
    public List<Skill> enemySkillWithCountdown = new List<Skill>();

    private void Awake()
    {
        CreateSingleton(true);
    }

    public void InitEnemy()
    {
        health = maxHealth;
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

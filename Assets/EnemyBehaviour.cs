using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public void InitEnemy()
    {
        health = maxHealth;
    }
}

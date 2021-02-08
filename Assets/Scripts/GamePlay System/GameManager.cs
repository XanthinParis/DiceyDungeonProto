using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


namespace Management
{
    public class GameManager : Singleton<GameManager>
    {
        public PlayerManager playerManager;
        public DiceManager diceManager;
        public GameObject cursor;

        public GameObject enemy;
        public EnemyBehaviour enemyBehaviour;

        void Start()
        {
            enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
        }
    }
}


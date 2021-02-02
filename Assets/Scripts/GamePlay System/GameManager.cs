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
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class PlayerManager : MonoBehaviour
{
        public int initialDiceCount;
        public int numberOfDice;

        public float health;
        public float maxHealth;

        public int LimitBreakPV;

        public List<GameObject> storedDice = new List<GameObject>();

        public List<Skill> playerSkills = new List<Skill>();


        public void InitPlayer()
        {
            health = maxHealth;
        }

    }




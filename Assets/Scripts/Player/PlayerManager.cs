using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public int initialDiceCount;
        public int numberOfDice;

        public float health;
        public float maxHealth;

        public int LimitBreakPV;

        public List<GameObject> storedDice = new List<GameObject>();

        private void Start()
        {
            maxHealth = 100f;
            health = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            numberOfDice = storedDice.Count;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Management.GameManager.Instance.diceManager.GenerateDice();
            }
        }

    }

}


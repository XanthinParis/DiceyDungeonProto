using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class DiceManager : MonoBehaviour
{
    public  List<GameObject> diceInitialPosition = new List<GameObject>();

    [SerializeField] private GameObject[] dices;

   public void GenerateDice()
   {
        for (int i = 0; i < GameManager.Instance.playerManager.storedDice.Count; i++)
        {
                Destroy(GameManager.Instance.playerManager.storedDice[i]);
        }

        GameManager.Instance.playerManager.storedDice.Clear();

        for (int i = 0; i < GameManager.Instance.playerManager.initialDiceCount; i++)
        {
            int spawnValue =  Random.Range(0, 6);
            GameObject spawnedDice =  Instantiate(dices[spawnValue],diceInitialPosition[i].transform.position,Quaternion.identity);
            GameManager.Instance.playerManager.storedDice.Add(spawnedDice);
            GameManager.Instance.playerManager.numberOfDice +=1;
        }
    }
}

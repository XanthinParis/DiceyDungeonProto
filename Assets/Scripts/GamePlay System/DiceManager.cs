using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //Permet de placer les dés selon des positions;
    public List<GameObject> diceInitialPosition = new List<GameObject>();

    //On stock les prefabs des dés afin de pouvoir les instancier.
    [SerializeField] private GameObject[] dices;

    void Update()
    {
        //Le faire au début de tour;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Manager.Instance.diceManager.GenerateDice();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Manager.Instance.diceManager.addDice(1);
        }
    }

    public void GenerateDice()
    {
        //Reset tous les dices.
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            Destroy(Manager.Instance.playerManager.storedDice[i]);
        }

        //Clear du Tableau.
        Manager.Instance.playerManager.storedDice.Clear();

        //Instanciation des dés.
        for (int i = 0; i < Manager.Instance.playerManager.initialDiceCount; i++)
        {
            //1 - Générer un nombre pour simuler de l'aléatoire.
            int spawnValue = Random.Range(0, 6);

            //2 - En fonction de du nombre choisi, j'instancie le dé à la bonne position.
            GameObject spawnedDice = Instantiate(dices[spawnValue], diceInitialPosition[i].transform.position, Quaternion.identity);
            Manager.Instance.playerManager.storedDice.Add(spawnedDice);
        }

        Manager.Instance.playerManager.numberOfDice = Manager.Instance.playerManager.storedDice.Count;
    }

    public void addDice(int moreDice)
    {
        for (int i = 0; i < moreDice; i++)
        {
            int spawnValue = Random.Range(0, 6);
            GameObject spawnedDice = Instantiate(dices[spawnValue], diceInitialPosition[Manager.Instance.playerManager.storedDice.Count].transform.position, Quaternion.identity);
            Manager.Instance.playerManager.storedDice.Add(spawnedDice);
        }

        Manager.Instance.playerManager.numberOfDice = Manager.Instance.playerManager.storedDice.Count;
    }
}

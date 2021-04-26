using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DiceManager : MonoBehaviour
{

    [SerializeField] private GameObject waypointsParentPlayer;
    //Permet de placer les dés selon des positions;
    public List<Transform> diceInitialPositionPlayer = new List<Transform>();

    [SerializeField] private GameObject waypointsParentEnemy;
    //Permet de placer les dés selon des positions;
    public List<Transform> diceInitialPositionEnemy = new List<Transform>();

    //On stock les prefabs des dés afin de pouvoir les instancier.
    [SerializeField] private GameObject[] dices;

    private void Start()
    {
        for (int i = 0; i < waypointsParentEnemy.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            diceInitialPositionEnemy.Add(waypointsParentEnemy.GetComponent<GetPositions>().waypointsPosition[i]);        
        }

        for (int i = 0; i < waypointsParentPlayer.GetComponent<GetPositions>().waypointsPosition.Count; i++)
        {
            diceInitialPositionPlayer.Add(waypointsParentPlayer.GetComponent<GetPositions>().waypointsPosition[i]);
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddDicePlayer(1);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddDiceEnemy(1);
        }
    }

    public void ResetDicePlayer()
    {
        //Reset tous les dices.
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            Destroy(Manager.Instance.playerManager.storedDice[i]);
        }

        //Clear du Tableau.
        Manager.Instance.playerManager.storedDice.Clear();
    }

    public void ResetDiceEnemy()
    {
        //Reset tous les dices.
        for (int i = 0; i < Manager.Instance.enemyBehaviour.storedDice.Count; i++)
        {
            Destroy(Manager.Instance.enemyBehaviour.storedDice[i]);
        }

        //Clear du Tableau.
        Manager.Instance.enemyBehaviour.storedDice.Clear();
    }

    public void GenerateDicePlayer()
    {
        //Instanciation des dés.
        for (int i = 0; i < Manager.Instance.playerManager.initialDiceCount; i++)
        {
            

            //1 - Générer un nombre pour simuler de l'aléatoire.
            int spawnValue = Random.Range(0, 6);
            //2 - En fonction de du nombre choisi, j'instancie le dé à la bonne position.
            GameObject spawnedDice = Instantiate(dices[spawnValue], diceInitialPositionPlayer[i].transform.position, Quaternion.identity);
            if (Manager.Instance.playerManager.numberOfBurn > 0)
            {
                Manager.Instance.playerManager.numberOfBurn--;
                spawnedDice.GetComponent<DiceBehaviour>().isBurn = true;
                spawnedDice.GetComponent<SpriteRenderer>().color = Color.red;
            }
            Manager.Instance.playerManager.storedDice.Add(spawnedDice);
        }

        Manager.Instance.playerManager.numberOfDice = Manager.Instance.playerManager.storedDice.Count;
    }

    public void GenerateDiceEnemy()
    {
        //Reset tous les dices.
        for (int i = 0; i < Manager.Instance.enemyBehaviour.storedDice.Count; i++)
        {
            Destroy(Manager.Instance.enemyBehaviour.storedDice[i]);
        }

        //Clear du Tableau.
        Manager.Instance.enemyBehaviour.storedDice.Clear();

        //Instanciation des dés.
        for (int i = 0; i < Manager.Instance.enemyBehaviour.initialDiceCount; i++)
        {
            //1 - Générer un nombre pour simuler de l'aléatoire.
            int spawnValue = Random.Range(0, 6);

            //2 - En fonction de du nombre choisi, j'instancie le dé à la bonne position.
            GameObject spawnedDice = Instantiate(dices[spawnValue], diceInitialPositionEnemy[i].transform.position, Quaternion.identity);
            spawnedDice.transform.SetParent(diceInitialPositionEnemy[i].transform);
            spawnedDice.transform.localPosition = new Vector3(0,0,1);
            spawnedDice.transform.SetParent(null);
            spawnedDice.GetComponent<BoxCollider2D>().enabled = false;

            Manager.Instance.enemyBehaviour.storedDice.Add(spawnedDice.GetComponent<DiceBehaviour>());
        }

        Manager.Instance.enemyBehaviour.storedDice = Manager.Instance.enemyBehaviour.storedDice.OrderBy(e => e.GetComponent<DiceBehaviour>().valueDice).ToList();
    }

    public void AddDiceEnemy(int moreDice)
    {
        for (int i = 0; i < moreDice; i++)
        {
            int spawnValue = Random.Range(0, 6);
            GameObject spawnedDice = Instantiate(dices[spawnValue], diceInitialPositionEnemy[Manager.Instance.enemyBehaviour.storedDice.Count].transform.position, Quaternion.identity);
            Manager.Instance.enemyBehaviour.storedDice.Add(spawnedDice.GetComponent<DiceBehaviour>());
        }

        Manager.Instance.enemyBehaviour.numberOfDice = Manager.Instance.enemyBehaviour.storedDice.Count;

    }

    public void AddDicePlayer(int moreDice)
    {
        for (int i = 0; i < moreDice; i++)
        {
            int spawnValue = Random.Range(0, 6);
            GameObject spawnedDice = Instantiate(dices[spawnValue], diceInitialPositionPlayer[Manager.Instance.playerManager.storedDice.Count].transform.position, Quaternion.identity);
            Manager.Instance.playerManager.storedDice.Add(spawnedDice);
        }

        Manager.Instance.playerManager.numberOfDice = Manager.Instance.playerManager.storedDice.Count;
    }
}

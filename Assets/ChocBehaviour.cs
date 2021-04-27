using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocBehaviour : MonoBehaviour
{
    public EquipementOwner equipementOwner;

    public DiceBehaviour diceOwn;

    private bool diceHere = false;

    private void Update()
    {
        if (diceHere && Input.GetMouseButtonUp(0))
        {
            StartCoroutine(RemoveChoc());
        }
    }

    public IEnumerator RemoveChoc()
    {
        BlockDice();
        Debug.Log("RemoveChoc");
        equipementOwner.isChoc = false;
        Manager.Instance.enemyBehaviour.enemyShock = false;
        equipementOwner.equipementOwn.isShock = false;
        equipementOwner.chocItem.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        equipementOwner.GetComponent<BoxCollider2D>().enabled = true;
        equipementOwner.diceOwn = null;
    }

    public void BlockDice()
    {
        //Bloquer les joueurs sur la position;
        equipementOwner.diceOwn.transform.SetParent(equipementOwner.dicePosition.transform);
        equipementOwner.diceOwn.transform.localPosition = Vector3.zero;
        equipementOwner.diceOwn.canMove = false;

        equipementOwner.diceOwn.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            if (collision.gameObject == Manager.Instance.playerManager.storedDice[i])
            {
                diceOwn = collision.gameObject.GetComponent<DiceBehaviour>();
                //Debug.Log("Collide");
                diceHere = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < Manager.Instance.playerManager.storedDice.Count; i++)
        {
            if (collision.gameObject == Manager.Instance.playerManager.storedDice[i])
            {
                diceHere = false;
                diceOwn = null;
                //Debug.Log("ExitCollide");
            }
        }
    }
}

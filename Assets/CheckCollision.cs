using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public bool isTrigger;
    public bool diceHere;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dice" )
        {
            gameObject.GetComponentInParent<EquipementBehaviour>().storedDice = collision.gameObject;
            diceHere = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="Dice")
        {
            diceHere = false;
        }
    }

    private void Update()
    {
        if (diceHere && Input.GetMouseButtonUp(0) /* ET CHECK LA CONDITION DU DE */) {
            isTrigger = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;

        }
    }



}

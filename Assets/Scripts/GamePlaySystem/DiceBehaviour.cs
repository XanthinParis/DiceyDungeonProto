using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceBehaviour : MonoBehaviour
{
    public Dice dice;

    public int valueDice;

    [SerializeField] private bool cursorHere;

    [SerializeField] public bool canMove = true;

    public bool isBurn = false;

    [SerializeField] private TextMeshProUGUI diceBurnAlerte;

    // Start is called before the first frame update
    void Start()
    {
        valueDice = dice.value;
        diceBurnAlerte.enabled = false;
        GetComponent<SpriteRenderer>().sprite = dice.asset;
    }

    private void Update()
    {
        if(cursorHere && Input.GetMouseButtonDown(0) && Manager.Instance.cursorBehaviour.currentSelected == gameObject && canMove)
        {
            if (isBurn)
            {
                isBurn = false;
                diceBurnAlerte.enabled = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                Manager.Instance.playerManager.TakeDamages(2);
            }

            gameObject.transform.parent = Manager.Instance.cursor.transform;
        }
        if (cursorHere && Input.GetMouseButtonUp(0))
        {
            gameObject.transform.parent = null;
            Manager.Instance.cursorBehaviour.currentSelected = null;
        }

        if (cursorHere)
        {
            Manager.Instance.cursorBehaviour.currentSelected = gameObject;
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Manager.Instance.cursor && Manager.Instance.cursorBehaviour.currentSelected ==null)
        {
            cursorHere = true;

            if (isBurn)
            {
                //Activer text;
                diceBurnAlerte.enabled = true;
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collisions)
    {
        if (collisions.gameObject == Manager.Instance.cursor)
        {
            cursorHere = false;
            Manager.Instance.cursorBehaviour.currentSelected = null;

            if (isBurn)
            {
                diceBurnAlerte.enabled = false;
            }
        }
    }

}

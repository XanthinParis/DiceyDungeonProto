using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehaviour : MonoBehaviour
{
    public Dice dice;

    public int valueDice;

    [SerializeField] private bool cursorHere;

    // Start is called before the first frame update
    void Start()
    {
        valueDice = dice.value;

        GetComponent<SpriteRenderer>().sprite = dice.asset;
    }

    private void Update()
    {
        if(cursorHere && Input.GetMouseButtonDown(0) && Manager.Instance.cursorBehaviour.currentSelected == gameObject)
        {
            gameObject.transform.parent = Manager.Instance.cursor.transform;
        }
        if (cursorHere && Input.GetMouseButtonUp(0))
        {
            gameObject.transform.parent = null;
            Manager.Instance.cursorBehaviour.currentSelected = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Manager.Instance.cursor && Manager.Instance.cursorBehaviour.currentSelected ==null)
        {
            cursorHere = true;
            //Debug.Log("cursorIn");
            Manager.Instance.cursorBehaviour.currentSelected = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collisions)
    {
        if (collisions.gameObject == Manager.Instance.cursor)
        {
            cursorHere = false;
            //Debug.Log("cursorOut");
            Manager.Instance.cursorBehaviour.currentSelected = null;
        }
    }

}

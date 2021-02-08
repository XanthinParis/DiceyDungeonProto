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
        if(cursorHere && Input.GetMouseButtonDown(0))
        {
            gameObject.transform.parent = Management.GameManager.Instance.cursor.transform;
        }
        if (cursorHere && Input.GetMouseButtonUp(0))
        {
            gameObject.transform.parent = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Management.GameManager.Instance.cursor)
        {
            cursorHere = true;
            Debug.Log("cursorIn");
            Management.GameManager.Instance.cursor.GetComponent<CursorBehaviour>().currentSelected = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collisions)
    {
        if (collisions.gameObject == Management.GameManager.Instance.cursor)
        {
            cursorHere = false;
            Debug.Log("cursorOut");
        }
    }

}

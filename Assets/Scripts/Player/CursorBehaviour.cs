using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ce script permet de mettre un curseur sous la sourie du joueur.
/// </summary>

public class CursorBehaviour : MonoBehaviour
{
    public GameObject currentSelected;

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        gameObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }
}

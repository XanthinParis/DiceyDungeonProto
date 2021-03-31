using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{

    public GameObject currentSelected;

   

    // Update is called once per frame
    void Update()
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

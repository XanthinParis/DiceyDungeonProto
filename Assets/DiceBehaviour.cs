using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBehaviour : MonoBehaviour
{
    public Dice dice;

    public int valueDice;

    // Start is called before the first frame update
    void Start()
    {
        valueDice = dice.value;

        GetComponent<SpriteRenderer>().sprite = dice.asset;
    }

}

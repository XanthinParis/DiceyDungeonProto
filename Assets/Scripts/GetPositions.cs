using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPositions : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            Manager.Instance.diceManager.diceInitialPosition.Add(child.gameObject);
        }
    }
}

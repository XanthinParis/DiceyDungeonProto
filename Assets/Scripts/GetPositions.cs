using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPositions : MonoBehaviour
{
    public List<Transform> waypointsPosition = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            waypointsPosition.Add(child);
        }
    }
}

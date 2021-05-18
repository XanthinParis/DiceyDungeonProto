using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedBackDamagePlayer : MonoBehaviour
{
    public TextMeshProUGUI damageText;

    public void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }
}

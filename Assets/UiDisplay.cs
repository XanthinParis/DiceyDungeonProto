using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Management;

public class UiDisplay : MonoBehaviour
{
    [SerializeField] private Image playerHealthBar;

    [SerializeField] private Image enemyHealthBar;

    // Update is called once per frame
    void Update()
    {
        playerHealthBar.fillAmount = GameManager.Instance.playerManager.health / GameManager.Instance.playerManager.maxHealth;

        enemyHealthBar.fillAmount = GameManager.Instance.enemyBehaviour.enemyHealth / GameManager.Instance.enemyBehaviour.enemyMaxHealth;
    }
}

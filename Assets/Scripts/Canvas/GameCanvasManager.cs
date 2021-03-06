﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvasManager : MonoBehaviour
{

    [Header("Limit Break")]
    [SerializeField] private Button limitBreakButton;
    [SerializeField] private Color limitbreakColorDisable;
    [SerializeField] private Color limitbreakColorAble;

    [Header("PlayerUI")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private TextMeshProUGUI esquiveText;

    [Header("EnemyUI")]
    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI diceText;
    
    [Header("Ecran De Fin")]
    [SerializeField] private GameObject result;

    [SerializeField] private Transform[] cameraPos; //0Player //1Enemy;
    public GameObject upCross;
    public GameObject downCross;

    private bool playerView = true;

    [Header("FeedbackPosition")]
    public GameObject playerDamageFeedbackPos;
    public GameObject enemyDamageFeedbackPos;

    // Start is called before the first frame update
    void Start()
    {
        downCross.SetActive(false);
        UpdateLimitBreakVisuel();
        UpdateHealth();
        result.SetActive(false);
        diceText.text = "D : " + Manager.Instance.enemyBehaviour.initialDiceCount.ToString();
    }

    private void Update()
    {
        if (Manager.Instance.turn == Manager.GameTurn.Enemy)
        {
            upCross.SetActive(false);
            downCross.SetActive(false);
        }
    }

    //Permet d'activer le button de la limit break une fois les conditions remplies.
    public void UpdateLimitBreakVisuel()
    {
        if (Manager.Instance.playerManager.limitBreakAvailable == true)
        {
            limitBreakButton.enabled = true;
            Manager.Instance.canvasManager.limitBreakButton.GetComponent<Image>().color = Manager.Instance.canvasManager.limitbreakColorAble;
        }
        else
        {
            Manager.Instance.canvasManager.limitBreakButton.GetComponent<Image>().color = Manager.Instance.canvasManager.limitbreakColorDisable;
            limitBreakButton.enabled = false; 
        }
    }

    //Update la Vie du joueur et de l'ennemi (appelé lorsqu'ils prennent des dégats).
    public void UpdateHealth()
    {
        if (PlayerManager.Instance.isEsquive)
        {
            esquiveText.enabled = true;
        }
        else
        {
            esquiveText.enabled = false;
        }

        playerHealthText.text = PlayerManager.Instance.health.ToString() + " / " + PlayerManager.Instance.maxHealth;
        enemyHealthText.text = EnemyBehaviour.Instance.health.ToString() + " / " + EnemyBehaviour.Instance.maxHealth;
        armorText.text = "Armor : " + EnemyBehaviour.Instance.armor.ToString();

        if(PlayerManager.Instance.health <= 0)
        {
            result.SetActive(true);
            result.GetComponent<TextMeshProUGUI>().text = "Défaite.";
            Manager.Instance.blockAction = true;
        }

        if (Manager.Instance.currentEnemy.GetComponent<EnemyBehaviour>().health <= 0)
        {
            result.SetActive(true);
            result.GetComponent<TextMeshProUGUI>().text = "Victoire.";
            Manager.Instance.blockAction = true;
        }
    }

    public void SwitchCamera()
    {
        StartCoroutine(SwitchCameraEnum());
    }

    //Animation de la Caméra via tweening;
    private IEnumerator SwitchCameraEnum()
    {
        if(!Manager.Instance.blockAction)
        {
            //Debug.Log("Move");
            Manager.Instance.blockAction = true;
            if (playerView)
            {
                upCross.SetActive(false);
                Camera.main.GetComponent<Tweener>().TweenPositionTo(cameraPos[1].position, 1f, Easings.Ease.SmootherStep, true);
                yield return new WaitForSeconds(1f);
                downCross.SetActive(true);
                Manager.Instance.blockAction = false;
                playerView = false;
            }
            else
            {
                downCross.SetActive(false);
                Camera.main.GetComponent<Tweener>().TweenPositionTo(cameraPos[0].position, 1f, Easings.Ease.SmootherStep, true);
                yield return new WaitForSeconds(1f);
                upCross.SetActive(true);
                Manager.Instance.blockAction = false;
                playerView = true;
            }
        }   
    }

    public void Quitter()
    {
        Application.Quit();
    }
}


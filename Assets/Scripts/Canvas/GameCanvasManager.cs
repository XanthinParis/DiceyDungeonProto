﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] private Button limitBreakButton;

    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI playerHealthText;

    [SerializeField] private Transform[] cameraPos; //0Player //1Enemy;
    [SerializeField] private GameObject upCross;
    [SerializeField] private GameObject downCross;

    private bool playerView = true;

    // Start is called before the first frame update
    void Start()
    {
        downCross.SetActive(false);
        UpdateLimitBreakVisuel();
    }

    // Update is called once per frame
    public void UpdateLimitBreakVisuel()
    {
        if (Manager.Instance.playerManager.limitBreakAvailable == true)
        {
            limitBreakButton.enabled = true;
            //Ajouter un coté shiny au Bouton;
        }
        else
        {
            limitBreakButton.enabled = false; ;
        }
    }

    public void UpdateHealth()
    {
        playerHealthText.text = PlayerManager.Instance.health.ToString() + " / " + PlayerManager.Instance.maxHealth;
        enemyHealthText.text = EnemyBehaviour.Instance.health.ToString() + " / " + EnemyBehaviour.Instance.maxHealth;
    }

    public void SwitchCamera()
    {
        StartCoroutine(SwitchCameraEnum());
    }

    private IEnumerator SwitchCameraEnum()
    {
        if(!Manager.Instance.blockAction)
        {
            Debug.Log("Move");
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
}

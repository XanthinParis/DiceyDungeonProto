using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] private Button limitBreakButton;
    [SerializeField] private Color limitbreakColorDisable;
    [SerializeField] private Color limitbreakColorAble;


    [SerializeField] private TextMeshProUGUI enemyHealthText;
    [SerializeField] private TextMeshProUGUI playerHealthText;

    [SerializeField] private TextMeshProUGUI result;

    [SerializeField] private Transform[] cameraPos; //0Player //1Enemy;
    public GameObject upCross;
    public GameObject downCross;

    private bool playerView = true;

    // Start is called before the first frame update
    void Start()
    {
        downCross.SetActive(false);
        UpdateLimitBreakVisuel();
        UpdateHealth();
        result.enabled = false;
    }

    private void Update()
    {
        if (Manager.Instance.turn == Manager.GameTurn.Enemy)
        {
            upCross.SetActive(false);
            downCross.SetActive(false);
        }
    }

    // Update is called once per frame
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

    public void UpdateHealth()
    {
        playerHealthText.text = PlayerManager.Instance.health.ToString() + " / " + PlayerManager.Instance.maxHealth;
        enemyHealthText.text = EnemyBehaviour.Instance.health.ToString() + " / " + EnemyBehaviour.Instance.maxHealth;

        if(PlayerManager.Instance.health <= 0)
        {
            result.enabled = true;
            result.text = "Défaite.";
            Manager.Instance.blockAction = true;
        }

        if (Manager.Instance.currentEnemy.GetComponent<EnemyBehaviour>().health <= 0)
        {
            result.enabled = true;
            result.text = "Victoire.";
            Manager.Instance.blockAction = true;
        }
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


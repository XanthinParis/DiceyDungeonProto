using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager : MonoBehaviour
{
    [SerializeField] private Button limitBreakButton;

    // Start is called before the first frame update
    void Start()
    {
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
}

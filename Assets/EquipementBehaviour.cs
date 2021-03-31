using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquipementBehaviour : MonoBehaviour
{
    //Template Things
    [SerializeField] private EquipementToken template;
    [SerializeField] private string equipementName;
    [SerializeField] private bool isBig;

    [SerializeField] private string effect;
    [SerializeField] private bool reusable;
    [SerializeField] private int reusableTime;
    [SerializeField] private enum conditionType { compteur, valeurX, valeurMin, valeurMax }
    [SerializeField] private conditionType condition;
    [SerializeField] private Sprite assetCondition;

    //Affichage Things
    [SerializeField] private TextMeshProUGUI nametext;
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private TextMeshProUGUI conditionNumber;
    [SerializeField] private Image diceContent;

    public GameObject storedDice;

    private void Update()
    {
        if (diceContent.GetComponent<CheckCollision>().isTrigger)
        {
            Management.GameManager.Instance.cursor.GetComponent<CursorBehaviour>().currentSelected.transform.SetParent(diceContent.rectTransform);
            storedDice.transform.position = new Vector3 (0f,-0.085f,0f);
            
        }
    }





}

using UnityEngine;

public class LimitBreakBehaviour : MonoBehaviour
{
    public bool cursorHere = false;

    public GameObject showDetails;

    private void Awake()
    {
        showDetails.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Manager.Instance.cursor && Manager.Instance.cursorBehaviour.currentSelected == null)
        {
            showDetails.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collisions)
    {
        if (collisions.gameObject == Manager.Instance.cursor)
        {
            showDetails.SetActive(false);
        }
    }
}

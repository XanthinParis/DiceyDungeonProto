using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuNavigation : MonoBehaviour
{
    public void StartFirstCombat()
    {
        SceneManager.LoadScene(1);
    }

    public void StartBossCombat()
    {
        SceneManager.LoadScene(2);
    }
}

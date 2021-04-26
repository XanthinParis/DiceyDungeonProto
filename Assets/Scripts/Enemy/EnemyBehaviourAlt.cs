using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourAlt : MonoBehaviour
{
    public enum monster { Hothead, Aoife }

    public monster monsterName;

    public EnemyBehaviour enemy;

    private void Start()
    {
        enemy = Manager.Instance.enemyBehaviour;
    }

    public void EnemyBattleAlt()
   {
        

        switch (monsterName)
        {
            case monster.Hothead:
                #region HotHead
                int valueDice0 = enemy.storedDice[0].GetComponent<DiceBehaviour>().dice.value;
                int valueDice1 = enemy.storedDice[1].GetComponent<DiceBehaviour>().dice.value;

                int maxValue = 0;

                if (valueDice0 > valueDice1)
                {
                    maxValue = valueDice0;
                }
                else
                {
                    maxValue = valueDice1;
                }

                for (int i = 0; i < enemy.enemySkillList.Count; i++)
                {
                    if (enemy.enemySkillList[i].isShock)
                    {
                        enemy.enemyShock = true;
                    }
                }

                //Si l'ennemy a une compétence en Choc, il faut voir ce qui est le plus rentable pour lui de faire.  
                if (enemy.enemyShock)
                {
                    if (enemy.enemySkillList[1].isShock)   // Si c'est la compétence 1 qui est choc, il faut que check si un des valeurs des deux dés est suffisantes pour que ca soit worth.
                    {
                        enemy.Comp1Shoked();
                    }
                    else //Dans le cas ou la compétence 0 est sous Choc
                    {
                        enemy.Comp0Shoked();
                    }
                }
                else
                {
                    enemy.NoShock();
                }
                #endregion
                break;

            case monster.Aoife:
                #region Aoife

                //IA;

                #endregion
                break;
            default:
                break;
        }
    }
}

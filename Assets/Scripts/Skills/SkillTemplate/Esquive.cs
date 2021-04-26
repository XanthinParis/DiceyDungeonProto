using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equipements/Esquive")]
public class Esquive : Skill
{
    public override void initSkillValue()
    {
        realInitSkillValue();
    }

    public override void TestValue()
    {
        RealTestValue();
    }

    public override void Use()
    {
        //Bloquer les joueurs sur la position;
        BlockDice();

        Manager.Instance.playerManager.isEsquive = true;
    }

}

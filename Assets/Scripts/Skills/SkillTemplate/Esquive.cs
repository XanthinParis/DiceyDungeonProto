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

        Manager.Instance.playerManager.isEsquive = true;
        equipementOwner.AnimationUse();
    }

}

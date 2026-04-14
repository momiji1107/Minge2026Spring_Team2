using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackUpgrade", menuName = "ScriptableObjects/Upgrade/AttackUpgrade")]
public class AttackUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("攻撃力の増加量")] private int attack;

    private void OnEnable()
    {
        titleName = "こうげき力UP";
        infoSentence = $"こうげき力が\n{attack} ふえる";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager?.Model.AttackUp(attack);
    }
}

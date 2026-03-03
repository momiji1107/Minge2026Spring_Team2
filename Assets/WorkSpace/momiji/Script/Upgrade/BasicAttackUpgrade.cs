using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttackUpgrade", menuName = "ScriptableObjects/Upgrade/BasicAttackUpgrade")]
public class BasicAttackUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("アップグレード前に所持している必要があるEquipment")] private EquipmentBase preBasicAttack;
    [SerializeField,Tooltip("アップグレード後のEquipment")] private EquipmentBase newBasicAttack;

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(equipmentManager.GetBasicAttack(preBasicAttack) != null) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.UpgradeBasicAttack(newBasicAttack);
    }
}

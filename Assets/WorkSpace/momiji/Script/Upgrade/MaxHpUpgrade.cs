using UnityEngine;

[CreateAssetMenu(fileName = "MaxHpUpgrade", menuName = "ScriptableObjects/Upgrade/MaxHpUpgrade")]
public class MaxHpUpgrade : UpgradeBase
{
    [SerializeField] private int hp;
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if (equipmentManager.Model.MaxHp < equipmentManager.Model.HPLIMIT) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.MaxHpUp(hp);
    }
}

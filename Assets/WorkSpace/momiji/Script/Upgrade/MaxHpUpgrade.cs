using UnityEngine;

[CreateAssetMenu(fileName = "MaxHpUpgrade", menuName = "ScriptableObjects/Upgrade/MaxHpUpgrade")]
public class MaxHpUpgrade : UpgradeBase
{
    [SerializeField] private int hp;
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.MaxHpUp(hp);
    }
}

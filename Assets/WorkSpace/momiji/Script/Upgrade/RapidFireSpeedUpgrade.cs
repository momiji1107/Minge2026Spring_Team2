using UnityEngine;

[CreateAssetMenu(fileName = "RapidFireSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/RapidFireSpeedUpgrade")]
public class RapidFireSpeedUpgrade : UpgradeBase
{
    [SerializeField] private float rapidFireSpeed;
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(equipmentManager.Model.RapidFireSpeed >= rapidFireSpeed) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.RapidFireSpeedUp(rapidFireSpeed);
    }
}

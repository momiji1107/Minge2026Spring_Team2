using UnityEngine;

[CreateAssetMenu(fileName = "ShootSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/ShootSpeedUpgrade")]
public class ShootSpeedUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("弾速の増加量")] private float shootSpeed;

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.ShootSpeedUp(shootSpeed);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeBase", menuName = "ScriptableObjects/Upgrade/MoveSpeedUpgrade")]
public class MoveSpeedUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("増加する移動速度の値")] private float moveSpeed;
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.MoveSpeedUp(moveSpeed);
    }
}

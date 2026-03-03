using UnityEngine;

[CreateAssetMenu(fileName = "SkillUpgrade", menuName = "ScriptableObjects/Upgrade/SkillUpgrade")]
public class SkillUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("アップグレード前に所持している必要があるEquipment")] private EquipmentBase preSkill;
    [SerializeField,Tooltip("アップグレード後のEquipment")] private EquipmentBase newSkill;

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(equipmentManager.GetSkill(preSkill) != null) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.UpgradeSkill(preSkill, newSkill);
    }
}

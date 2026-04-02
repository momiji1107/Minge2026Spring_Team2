using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillGetUpgrade", menuName = "ScriptableObjects/Upgrade/NewSkillGetUpgrade")]
public class NewSkillGetUpgrade : UpgradeBase
{
    [SerializeField] private EquipmentBase newSkill;
    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(equipmentManager.GetSkill(newSkill) == null && equipmentManager.SkillNum < equipmentManager.MaxSkillnum) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.AddSkill(newSkill, icon);
    }
}

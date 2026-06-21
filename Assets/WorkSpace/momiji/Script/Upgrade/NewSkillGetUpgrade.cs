using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillGetUpgrade", menuName = "ScriptableObjects/Upgrade/NewSkillGetUpgrade")]
public class NewSkillGetUpgrade : UpgradeBase
{
    [SerializeField,Tooltip("このスキルの強化版スキル")] private List<EquipmentBase> upgradeSkills;
    [SerializeField] private EquipmentBase newSkill;

    private void OnEnable()
    {
        titleName = "新スキルGET";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        //強化版スキルを持っていたら表示しない
        foreach (var upgrade in upgradeSkills)
        {
            if (equipmentManager.GetSkill(upgrade) != null) return false;
        }
        //このスキルを持っていない　かつ　スキルスロットが空いていたら表示する
        if(equipmentManager.GetSkill(newSkill) == null && equipmentManager.SkillNum < equipmentManager.MaxSkillnum) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.AddSkill(newSkill, icon);
    }
}

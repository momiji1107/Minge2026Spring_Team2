using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private PlayerAttackController attackController;
    [SerializeField] private int maxSkillnum = 3; //最大スキル所持数
    
    public PlayerModel Model => model;
    
    //スキルを追加する
    public void AddSkill(EquipmentBase newSkill)
    {
        if (attackController.Skills.Count >= maxSkillnum) return;
        attackController.Skills.Add(newSkill);
    }

    //スキルを検索して所持していればそのスキルを返す
    public EquipmentBase GetSkill(EquipmentBase requireSkill)
    {
        EquipmentBase skill = attackController.Skills.Find(s => s.name == requireSkill.name);
        return skill;
    }

    //通常攻撃をアップグレードする
    public void UpgradeBasicAttack(EquipmentBase newBasicAttack)
    {
        attackController.BasicAttack = newBasicAttack;
    }
    
    //スキルをアップグレードする
    public void UpgradeSkill(EquipmentBase preSkill, EquipmentBase newSkill)
    {
        int idx = attackController.Skills.FindIndex(s => s.name == preSkill.name);
        if (idx < 0) return;
        attackController.Skills[idx] = newSkill;
    }
}

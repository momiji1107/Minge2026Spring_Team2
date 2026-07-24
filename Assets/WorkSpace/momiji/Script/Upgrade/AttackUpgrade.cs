using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "AttackUpgrade", menuName = "ScriptableObjects/Upgrade/AttackUpgrade")]
public class AttackUpgrade : UpgradeBase
{
    [SerializeField, Tooltip("攻撃力の増加量と重み")]
    private AmountData[] data;
    private int attack;

    private void OnEnable()
    {
        titleName = "こうげき力UP";
        infoSentence = $"こうげき力が\n{(int)data[0].amount}〜{(int)data[data.Length - 1].amount} ふえる";
    }

    public override void UpdateUpgrade()
    {
        //重みつき計算
        int weightSum = 0;
        
        data = data
            .OrderBy(u => Guid.NewGuid())
            .ToArray();
            
        int displayBoader = Random.Range(0, data.Sum(x => x.weight)); 
        for (int i = 0; i < data.Length; i++)
        {
            weightSum += data[i].weight;
            if (displayBoader <= weightSum)
            {
                attack = (int)data[i].amount;
                break;
            }
        }
        
        infoSentence = $"こうげき力が\n{attack} ふえる";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(PlayerSelection.selectedCharacter == CharacterName.PLAYER_ONE && attack >= 10) return false;
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager?.Model.AttackUp(attack);
    }
}

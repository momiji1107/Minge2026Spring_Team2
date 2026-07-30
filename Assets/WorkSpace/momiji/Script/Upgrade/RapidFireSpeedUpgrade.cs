using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "RapidFireSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/RapidFireSpeedUpgrade")]
public class RapidFireSpeedUpgrade : UpgradeBase
{
    [SerializeField, Tooltip("攻撃力の増加量と重み")]
    private AmountData[] data;
    private float rapidFireSpeed;
    
    void OnEnable()
    {
        titleName = "こうげき速度UP";
        infoSentence = $"こうげきの\nクールタイムが\n{data[0].amount}〜{data[data.Length - 1].amount}秒 早くなる";
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
                rapidFireSpeed = data[i].amount;
                break;
            }
        }
        
        infoSentence = $"こうげきの\nクールタイムが\n{rapidFireSpeed}秒 早くなる";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(PlayerSelection.selectedCharacter == CharacterName.PLAYER_TWO) return false;
        else if(equipmentManager.Model.RapidFireSpeed > 0.5f) return true;
        return false;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.RapidFireSpeedUp(rapidFireSpeed);
    }
}

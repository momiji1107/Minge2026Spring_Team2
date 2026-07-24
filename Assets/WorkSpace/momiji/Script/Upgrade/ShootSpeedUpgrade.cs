using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ShootSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/ShootSpeedUpgrade")]
public class ShootSpeedUpgrade : UpgradeBase
{
    [SerializeField, Tooltip("攻撃力の増加量と重み")]
    private AmountData[] data;
    private float shootSpeed;

    void OnEnable()
    {
        titleName = "だんそくUP";
        infoSentence = $"だんそくが\n{data[0].amount}〜{data[data.Length - 1].amount} 上がる";
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
                shootSpeed = data[i].amount;
                break;
            }
        }
        infoSentence = $"だんそくが\n{shootSpeed} 上がる";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        if(PlayerSelection.selectedCharacter == CharacterName.PLAYER_ONE) return false;
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager.Model.ShootSpeedUp(shootSpeed);
    }
}

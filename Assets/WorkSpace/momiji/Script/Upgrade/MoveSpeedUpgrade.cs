using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "MoveSpeedUpgrade", menuName = "ScriptableObjects/Upgrade/MoveSpeedUpgrade")]
public class MoveSpeedUpgrade : UpgradeBase
{
    [SerializeField, Tooltip("攻撃力の増加量と重み")]
    private AmountData[] data;
    private float moveSpeed;

    void OnEnable()
    {
        titleName = "いどう速度UP";
        infoSentence = $"いどう速度が\n{data[0].amount}〜{data[data.Length - 1].amount} 上がる";
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
                moveSpeed = data[i].amount;
                break;
            }
        }
        infoSentence = $"いどう速度が\n{moveSpeed} 上がる";
    }

    public override bool CanAppear(PlayerEquipmentManager equipmentManager)
    {
        return true;
    }

    public override void Apply(PlayerEquipmentManager equipmentManager)
    {
        equipmentManager?.Model.MoveSpeedUp(moveSpeed);
    }
}

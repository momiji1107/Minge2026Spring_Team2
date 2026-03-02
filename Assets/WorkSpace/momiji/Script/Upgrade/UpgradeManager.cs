using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerEquipmentManager equipmentManager;
    [SerializeField] private List<UpgradeBase> upgrades;
    //[SerializeField] private Canvas upgradeCanvas;
    [SerializeField,Tooltip("表示するアップグレードの数")] private int diplayUpgradesNum;
    private List<UpgradeBase> displayUpgrades; //選択肢に表示するアップグレード
    
    //アップグレードをランダムに表示する
    public void DisplayRandomUpgrades()
    {
        //表示可能なものを抽出、要素をシャッフルし、前からdisplayUpgradesNumの数だけ取り出す
        displayUpgrades = upgrades.Where(u => u.CanAppear(equipmentManager))
            .OrderBy(u => Guid.NewGuid())
            .Take(diplayUpgradesNum)
            .ToList();
        
        //アップグレード画面を表示（後ほど追加）
        //upgradeCanvas.gameObject.SetActive(true);
        
        //選ばれた３つの選択肢確認用
        //Debug.Log("selection: " + displayUpgrades[0].name + "\n" + displayUpgrades[1].name + "\n" + displayUpgrades[2].name);
    }
    
    //アップグレードを選択し、反映する
    //select => 選択肢のうち選んだ番号、一番左がゼロ
    public void SelectUpgrade(int select)
    {
        displayUpgrades[select].Apply(equipmentManager);
    }
}

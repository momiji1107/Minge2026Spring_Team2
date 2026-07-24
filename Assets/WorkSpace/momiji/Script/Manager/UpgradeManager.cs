using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerEquipmentManager equipmentManager;
    [SerializeField] private PlayerModel model;
    [SerializeField] private AudioManager audioManager;
    
    [Header("アップグレード関係")]
    [SerializeField] private List<UpgradeBase> upgrades;
    [SerializeField,Tooltip("表示するアップグレードの数")] private int diplayUpgradesNum;
    private List<UpgradeBase> displayUpgrades; //選択肢に表示するアップグレード
    private int selectNumber; //選択中のアップグレードを示す
    
    [Header("パネルUI関係")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private RectTransform[] panelRects;
    [SerializeField] private TextMeshProUGUI[] nameTexts;
    [SerializeField] private TextMeshProUGUI[] infoTexts;
    [SerializeField] private Image[] images;
    private float atractSize = 1.2f; //選択中のパネルの拡大したサイズ

    void Start()
    {
        upgradePanel.gameObject.SetActive(false);
        displayUpgrades = new List<UpgradeBase>();
        selectNumber = 0;
    }
    
    //アップグレードをランダムに表示する
    public void DisplayRandomUpgrades()
    {
        //時間を止める＆状態の切り替え
        Time.timeScale = 0f;
        GameManagement.GameState = GAMESTATE.ISUPGRADE;
        
        displayUpgrades = new List<UpgradeBase>();
        
        //アップグレード内容を更新
        foreach (UpgradeBase upgrade in upgrades) upgrade.UpdateUpgrade();
        
        //表示可能なものを抽出し、要素をシャッフル
        List<UpgradeBase> canAppears = upgrades
            .Where(u => u.CanAppear(equipmentManager))
            .ToList();
        
        //重みつき計算
        for (int i = 0; i < diplayUpgradesNum; i++)
        {
            int weightSum = 0;
            //配列シャッフル
            canAppears = canAppears
                .OrderBy(u => Guid.NewGuid())
                .ToList();
            
            int displayBoader = UnityEngine.Random.Range(0, canAppears.Sum(upgrade => upgrade.rarity));
            for (int j = 0; j < canAppears.Count; j++)
            {
                weightSum += canAppears[j].rarity;
                if (displayBoader <= weightSum)
                {
                    //Debug.Log("add: " + canAppears[j].name);
                    displayUpgrades.Add(canAppears[j]);
                    canAppears.RemoveAt(j);
                    break;
                }
            }
        }
        
        
        
        //選ばれた３つの選択肢確認用
        //Debug.Log("selection: " + displayUpgrades[0].titleName + ", " + displayUpgrades[1].titleName + ", " + displayUpgrades[2].titleName);
        
        //パネルの表示にアップグレードの内容を反映させる
        for (int i = 0; i < displayUpgrades.Count; i++)
        {
            nameTexts[i].text = displayUpgrades[i].titleName;
            infoTexts[i].text = displayUpgrades[i].infoSentence;
            images[i].sprite = displayUpgrades[i].icon;
        }
        
        //アップグレード画面を表示
        upgradePanel.gameObject.SetActive(true);
    }
    
    //アップグレードを選択し、反映する
    //select => 選択肢のうち選んだ番号、一番左がゼロ
    public void SelectUpgrade(int select)
    {
        displayUpgrades[select].Apply(equipmentManager);
        upgradePanel.gameObject.SetActive(false);
        
        //時間を動かす＆状態の切り替え
        Time.timeScale = 1f;
        GameManagement.GameState = GAMESTATE.INGAME;
    }
    
    //アップグレード中は左右矢印キーまたはADキーで選択肢を変更する、Enterキーで決定
    public void UpgradeInput()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && selectNumber < displayUpgrades.Count - 1)
        {
            selectNumber++;
            audioManager.Select();
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && selectNumber > 0)
        {
            selectNumber--;
            audioManager.Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectUpgrade(selectNumber);
            audioManager.Confirm();
        }

        for (int i = 0; i < panelRects.Length; i++)
        {
            panelRects[i].localScale = (i == selectNumber) ? Vector3.one * atractSize : Vector3.one;
        }
    }
}

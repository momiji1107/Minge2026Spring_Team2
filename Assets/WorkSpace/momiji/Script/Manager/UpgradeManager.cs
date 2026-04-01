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
    [SerializeField] private List<UpgradeBase> upgrades;
    
    [Header("パネルUI関係")]
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private RectTransform[] panelRects;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private Image[] images;
    private float atractSize = 1.2f; //選択中のパネルの拡大したサイズ
    
    [Header("アップグレード関係")]
    [SerializeField,Tooltip("表示するアップグレードの数")] private int diplayUpgradesNum;
    private List<UpgradeBase> displayUpgrades; //選択肢に表示するアップグレード
    private int selectNumber; //選択中のアップグレードを示す

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
        
        //表示可能なものを抽出、要素をシャッフルし、先頭からdisplayUpgradesNumの数だけ取り出す
        displayUpgrades = upgrades.Where(u => u.CanAppear(equipmentManager))
            .OrderBy(u => Guid.NewGuid())
            .Take(diplayUpgradesNum)
            .ToList();
        
        //パネルの表示にアップグレードの内容を反映させる
        for (int i = 0; i < displayUpgrades.Count; i++)
        {
            texts[i].text = displayUpgrades[i].name;
            images[i].sprite = displayUpgrades[i].icon;
        }
        
        //アップグレード画面を表示
        upgradePanel.gameObject.SetActive(true);
        
        //選ばれた３つの選択肢確認用
        Debug.Log("selection: " + displayUpgrades[0].name + ", " + displayUpgrades[1].name + ", " + displayUpgrades[2].name);
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
    
    //アップグレード中は左右矢印キーで選択肢を変更する、Enterキーで決定
    public void UpgradeInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && selectNumber < displayUpgrades.Count - 1)
        {
            selectNumber++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && selectNumber > 0)
        {
            selectNumber--;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectUpgrade(selectNumber);
        }

        for (int i = 0; i < panelRects.Length; i++)
        {
            panelRects[i].localScale = (i == selectNumber) ? Vector3.one * atractSize : Vector3.one;
        }
    }
}

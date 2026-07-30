using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagePanelSetting : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [Header("Data表示UI")]
    [SerializeField] private Image[] difficultyStar;
    [SerializeField] private Image[] enemiySprite;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI stageTitleText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int star = stageData.Difficulty;
        for (int i = 0; i < difficultyStar.Length; i++)
        {
            difficultyStar[i].enabled = i < star;
        }
        int enemyNum = stageData.Enemies.Length;
        for (int i = 0; i < enemiySprite.Length; i++)
        {
            if(i < enemyNum) enemiySprite[i].sprite = stageData.Enemies[i];
            enemiySprite[i].enabled = i < enemyNum;
        }
        infoText.text = stageData.InfoText;
        stageTitleText.text = stageData.StageTitleText;
    }
    
}
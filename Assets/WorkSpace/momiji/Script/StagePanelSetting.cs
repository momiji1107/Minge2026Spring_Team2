using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StagePanelSetting : MonoBehaviour
{
    [SerializeField] private StageData stageData;
    [Header("Data表示UI")]
    [SerializeField] private Image[] enemiySprite;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI stageTitleText;
    [SerializeField] private TextMeshProUGUI difficultyText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int enemyNum = stageData.Enemies.Length;
        for (int i = 0; i < enemiySprite.Length; i++)
        {
            if(i < enemyNum) enemiySprite[i].sprite = stageData.Enemies[i];
            enemiySprite[i].enabled = i < enemyNum;
        }
        
        infoText.text = stageData.InfoText;
        stageTitleText.text = stageData.StageTitleText;
        
        switch (stageData.Difficulty)
        {
            case DifficultyData.EASY:
                difficultyText.text = "かんたん";
                break;
            case DifficultyData.NORMAL:
                difficultyText.text = "ふつう";
                break;
            case DifficultyData.HARD:
                difficultyText.text = "むずかしい";
                break;
            default:
                break;
        }
    }
    
}
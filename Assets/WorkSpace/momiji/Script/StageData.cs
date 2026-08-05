using UnityEngine;

public enum DifficultyData
{
    EASY,
    NORMAL,
    HARD
};

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    [Header("ステージ情報")]
    [SerializeField] private DifficultyData difficulty;
    [SerializeField] private Sprite[] enemies;
    [SerializeField] private string infoText;
    [SerializeField] private string stageTitleText;

    //getter
    public DifficultyData Difficulty => difficulty;
    public Sprite[] Enemies => enemies;
    public string InfoText => infoText;
    public string StageTitleText => stageTitleText;
}
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    [Header("ステージ情報")]
    [SerializeField] private int difficulty;
    [SerializeField] private Sprite[] enemies;
    [SerializeField] private string infoText;
    [SerializeField] private string stageTitleText;

    //getter
    public int Difficulty => difficulty;
    public Sprite[] Enemies => enemies;
    public string InfoText => infoText;
    public string StageTitleText => stageTitleText;
}
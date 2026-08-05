using UnityEngine;

public static class StageSelection
{
    public static SceneName selectedStage;
}

public class StageSelect : MonoBehaviour
{
    public SceneName stageName;
    public void ChangeStage()
    {
        StageSelection.selectedStage = stageName;
        Debug.Log("selectedStage is "+ StageSelection.selectedStage);
    }
}
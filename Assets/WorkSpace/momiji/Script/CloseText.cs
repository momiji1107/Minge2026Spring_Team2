using UnityEngine;

public class CloseText : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scoreManager.CloseScore();
        }
    }
}

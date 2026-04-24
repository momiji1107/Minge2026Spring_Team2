using System.Collections;
using UnityEngine;
using TMPro;

public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private RectTransform _canvasRect;
    private float moveSpeed = 1.0f;
    private float displayTime = 1.0f;

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
        _canvasRect = gameObject.GetComponent<RectTransform>();
        StartCoroutine(ScorePopupMove());
    }

    private IEnumerator ScorePopupMove()
    {
        Vector3 pos = _canvasRect.anchoredPosition;
        float t = 0;
        while (t <= displayTime)
        {
            t += Time.deltaTime;
            
            float move = Mathf.Sin(t * Mathf.PI) / 2.0f; //イージングの計算
            pos += Vector3.up * moveSpeed * move * Time.deltaTime;
            _canvasRect.anchoredPosition = pos;
            
            yield return null;
        }
        Destroy(gameObject);
    }
}

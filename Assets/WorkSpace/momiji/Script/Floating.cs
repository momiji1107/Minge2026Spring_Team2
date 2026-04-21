using System.Collections;
using UnityEngine;

public class Floating : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float moveSize = 1f;
    private RectTransform rect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rect = GetComponent<RectTransform>();
        Vector2 startPos = rect.position;
        StartCoroutine(FloatingObj(startPos));
    }

    /// <summary>
    /// オブジェクトをぷかぷかさせる
    /// </summary>
    /// <returns>１フレーム待機</returns>
    private IEnumerator FloatingObj(Vector2 startPos)
    {
        float t = 0;
        while (true)
        {
            t += Common.OneFrameTime;
            //移動場所の計算
            Vector2 pos = new Vector2(startPos.x ,startPos.y + Mathf.Sin(t * moveSpeed) * moveSize);
            rect.position = pos;
            yield return new WaitForSecondsRealtime(Common.OneFrameTime);
        }
    }
}

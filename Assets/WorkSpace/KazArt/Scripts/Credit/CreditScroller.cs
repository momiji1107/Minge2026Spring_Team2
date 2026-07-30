using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class CreditScrollViewer : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private float scrollSpeed;

    private ScrollView scrollView;
    private bool isFinished;

    public bool IsFinished => isFinished;

    void OnEnable()
    {
        if (uiDocument == null)
        {
            uiDocument = GetComponent<UIDocument>();
        }

        var root = uiDocument.rootVisualElement;

        scrollView = root.Q<ScrollView>();

        if (scrollView == null)
        {
            Debug.Log("ScrollViewが見つかりませんでした");
            return;
        }

        root.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
    }

    private void OnGeometryChanged(GeometryChangedEvent evt)
    {
        var element = (VisualElement)evt.target;
        element.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);

        StartCoroutine(ScrollRoutine());
    }

    private IEnumerator ScrollRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        VisualElement container = scrollView.contentContainer;

        if (container == null)
        {
            Debug.Log("クレジットのコンテナが取得できませんでした。");
            yield break;
        }

        float contentHeight = container.resolvedStyle.height;
        float viewportHeight = scrollView.resolvedStyle.height;


        float maxScrollY = Mathf.Max(0, contentHeight - viewportHeight);

        float currentScrollY = 0f;
        float speedMultiplier = 5.0f;

        while (currentScrollY < maxScrollY)
        {
            bool isHighSpeed = Input.GetKey(KeyCode.Space);
            bool isSkipped = Input.GetKey(KeyCode.Escape);

            float currentSpeed = scrollSpeed * (isHighSpeed ? speedMultiplier : 1.0f);


            currentScrollY += currentSpeed * Time.deltaTime;
            if (isSkipped) currentScrollY = maxScrollY;

            float targetY = Mathf.Min(currentScrollY, maxScrollY);

            scrollView.scrollOffset = new Vector2(0, targetY);

            yield return null;
        }

        isFinished = true;

        Debug.Log("クレジットが最後まで流れ切りました");
        Debug.Log("isFinished " + isFinished);
    }
}
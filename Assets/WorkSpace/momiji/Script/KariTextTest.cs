using TMPro;
using UnityEditor;
using UnityEngine;

public class KariTextTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [Multiline] public string infoText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = infoText;
    }
}

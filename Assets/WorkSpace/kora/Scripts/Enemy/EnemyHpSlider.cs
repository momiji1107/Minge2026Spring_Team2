using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class EnemyHpSlider : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject fillArea;
    [SerializeField] private Canvas canvas;

    private float _hp;
    private float _maxHp;
    private EnemyCore _core;

    void Start()
    {
        _core = enemy.GetComponent<EnemyCore>();
        _maxHp = _core.GetMaxHp();
        _hp = _maxHp;
        
        slider.maxValue = _maxHp;
        slider.minValue = 0;
        slider.value = _hp;
        
        canvas.worldCamera = Camera.main;
    }
    
    void Update()
    {
        _hp = _core.GetHp();
        
        slider.value = _hp;
        if (_hp <= 0) fillArea.SetActive(false);
    }
}

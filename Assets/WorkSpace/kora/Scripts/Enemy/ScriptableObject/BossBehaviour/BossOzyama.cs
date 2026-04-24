using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossOzyama", menuName = "ScriptableObjects/Enemy/BossBehaviour/BossOzyama")]
public class BossOzyama : BossBehaviourBaseSO
{
    [SerializeField] private GameObject panelPrefab;
    
    [SerializeField] private bool isOnce;
    [SerializeField] private float startTime = 1f;
    [SerializeField] private float interval = 5f;
    [SerializeField] private int number = 3;
    [SerializeField] private bool isCheckViewPort = true;
    [SerializeField] private Vector2 minViewPort = new Vector2(0,1);
    [SerializeField] private Vector2 maxViewPort = new Vector2(0,1);
    [SerializeField] private Vector2 projDistance = new Vector2(0.1f,0.1f);
    
    private float _timer;
    private bool _isFire;
    private List<Vector3> _currentViewPort;
    private Vector2 _minViewPort;
    private Vector2 _maxViewPort;
    
    protected override void OnInit()
    {
        _isFire = false;
        _timer = interval - startTime;
        _currentViewPort = new List<Vector3>();
        _minViewPort = minViewPort;
        _maxViewPort = maxViewPort;
    }

    public override void Tick(float dt)
    {
        if (_isFire) return;
        _timer += dt;

        if (_timer <= interval) return;

        for (int i = 0; i < number; i++)
        {
            Vector3 viewPort = new Vector3
            {
                x = UnityEngine.Random.Range(_minViewPort.x, _maxViewPort.x),
                y = UnityEngine.Random.Range(_minViewPort.y, _maxViewPort.y)
            };
            
            //viewPortが既に発射した場所の近くかどうかを確認する
            

            if (CheckRandomViewPort(viewPort))
            {
                //retry
                //Debug.Log("Can not cast 1 Ozyama." + viewPort);
                continue;
            }
            
            //panelを生成する
            _currentViewPort.Add(viewPort);
            var point = SceneContext.Instance.canvas;
            var pos = Camera.main.ViewportToWorldPoint(viewPort);
            pos.z = point.transform.position.z;

            var panel = Context.Instantiate(panelPrefab, pos);
            var scale = panel.transform.lossyScale;
            panel.transform.SetParent(point.transform);
            panel.transform.position = pos;
            panel.transform.localScale = scale;
        }
        
        _timer = 0f;
        _currentViewPort.Clear();
        
        if (isOnce) _isFire = true;
    }

    private bool CheckRandomViewPort(Vector3 viewPort)
    {
        if (!isCheckViewPort) return false;
        bool isContainBox = false;
            
        foreach (var port in _currentViewPort)
        {
            if ((port.x - projDistance.x < viewPort.x && viewPort.x < port.x + projDistance.x)
                && (port.y - projDistance.y < viewPort.y && viewPort.y < port.y + projDistance.y))
            {
                isContainBox = true;
                break;
            }
        }
        
        return isContainBox;
    }

    protected override void OnSetIsRight()
    {
        _minViewPort.x = 1-_minViewPort.x;
        _maxViewPort.x = 1-_maxViewPort.x;
        
        (_minViewPort.x, _maxViewPort.x) = (_maxViewPort.x, _minViewPort.x);
    }
}

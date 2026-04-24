using System.Collections;
using UnityEngine;

public class ForecastAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField, Range(0f,1f)] private float maxAlpha = 1f;
    [SerializeField, Range(0f,1f)] private float minAlpha = 0f;
    [SerializeField] private int num = 2;

    [SerializeField] private bool isStartSelf;
    
    private float _duration = 0f;
    private float _interval;

    private float _timer = 0f;
    private float _fadeDuration = 0f;
    private bool _isFadeIn = false;

    void Start()
    {
        if(isStartSelf) Init(5f);
    }
    
    public void Init(float duration)
    {
        _duration = duration;
        num = 2;
        _interval = _duration / num;
        
        _fadeDuration = _interval/2;

        _isFadeIn = true;
        
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, minAlpha);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        
        if (_timer < _duration)
        {
            _timer += dt;
            Fade(dt);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Fade(float dt)
    {
        float alpha = sr.color.a;
        float deltaAlpha = ((maxAlpha - minAlpha) / _fadeDuration) * dt;
        deltaAlpha = _isFadeIn ?  deltaAlpha : -deltaAlpha;

        alpha += deltaAlpha;
        //Debug.Log("FadeAlpha:" +  alpha);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        if (alpha >= maxAlpha || alpha <= minAlpha)
        {
            alpha = _isFadeIn ?  maxAlpha : minAlpha;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
            _isFadeIn = !_isFadeIn;
            //Debug.Log("alpha:" + alpha  + "\ntimer:" + _timer);
        }
    }
}

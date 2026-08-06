using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public interface IEnemyStatusPanel
{
    public void OnHitPoison();
    public void ShowSlow(float duration){}
    public void ShowStun(float duration){}
    public void DisActivePanel(){}
}
public class EnemyStatusPanel : MonoBehaviour, IEnemyStatusPanel
{
    [SerializeField] private Image image;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject backGround;
    [Header("毒")][SerializeField] private Sprite poisonImages;
    [Header("スロウ")][SerializeField] private Sprite slowImages;
    [Header("スタン")][SerializeField] private Sprite stunImages;

    [SerializeField] private float poisonHitInterval;

    private float _poisonTimer = 0f;
    private bool _isActive = false;
    private bool _isActivePosion = false;

    private void Start()
    {
        //backGround?.SetActive(false);
        image.gameObject.SetActive(false);
        image.sprite = null;
    }
    
    private void Update()
    {
        if (_isActivePosion)
        {
            _poisonTimer += Time.deltaTime;
            if (_poisonTimer <= poisonHitInterval) return;

            _poisonTimer = 0f;
            DisActivePanel();
        }
    }

    public void OnHitPoison()
    {
        if (!_isActivePosion)
        {
            _isActivePosion = true;
            ShowPoison();
        }
        
        // reset timer
        _poisonTimer = 0f;
    }
    
    public void ShowPoison()
    {
        CheckActive();
        image.sprite = poisonImages;
    }

    public void ShowSlow(float duration)
    {
        StartCoroutine(StartShowIcon(slowImages, duration));
    }

    public void ShowStun(float duration)
    {
        StartCoroutine(StartShowIcon(stunImages, duration));
    }

    public void DisActivePanel()
    {
        _isActive = false;
        image.sprite = null;
        image.gameObject.SetActive(false);
        //backGround?.SetActive(false);
    }

    private IEnumerator StartShowIcon(Sprite sprite, float duration)
    {
        CheckActive();
        image.sprite = sprite;
        yield return new WaitForSeconds(duration);
        DisActivePanel();
    }

    private void CheckActive()
    {
        if (_isActive) return;
        
        _isActive = true;
        image.gameObject.SetActive(true);
        //backGround?.SetActive(true);
    }
}
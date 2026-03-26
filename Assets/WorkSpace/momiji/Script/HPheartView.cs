using System.Collections;
using UnityEngine;

public class HPheartView : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    
    [Header("HPハート")]
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject[] baseHearts;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        HPView();
    }
    
    public void HPView()
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < model.Hp);
        }
        
        for(int i = 0; i < baseHearts.Length; i++)
        {
            baseHearts[i].SetActive(i < model.MaxHp);
        }
    }
}

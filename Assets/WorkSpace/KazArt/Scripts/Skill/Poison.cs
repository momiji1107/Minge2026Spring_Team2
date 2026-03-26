using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Poison", menuName = "ScriptableObjects/Skill/Poison")]
public class Poison : EquipmentBase
{
    [Header("毒持続ダメージ時間")]
    [SerializeField] private int poisonDuration;
    [SerializeField] private int poisonInterval;

    [Header("当たり判定サイズ")]
    [SerializeField] private float boxWidth;
    [SerializeField] private float boxHeight;
    [SerializeField] private float offSet;
    
    public override void Activate(PlayerModel model)
    {
        Debug.Log("発動");

        Vector2 ownerPos = model.transform.position;
        Vector2 hitPos = ownerPos + (Vector2)model.transform.right * offSet;
        Vector2 hitSize = new Vector2(boxWidth, boxHeight);

        Collider2D[] targetEnemy = Physics2D.OverlapBoxAll(hitPos,hitSize, 0);

        PoisonDetection(model,targetEnemy);
    }

    private void PoisonDetection(PlayerModel model, Collider2D[] targetEnemy)
    {
        foreach (var hit in targetEnemy)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                model.StartCoroutine(PoisonAttack(model, hit));
            }
        }
    }

    private IEnumerator PoisonAttack(PlayerModel model,Collider2D targetEnemy)
    {
        int poisonTime = 0;
        
        while (poisonTime < poisonDuration)
        {
            if (targetEnemy == null) break;

            targetEnemy.gameObject.GetComponent<EnemyCore>()?.TakeDamage(model.Attack);
            yield return new WaitForSeconds(poisonInterval);
            poisonTime += poisonInterval;
        }
    }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Slow", menuName = "ScriptableObjects/Skill/Slow")]
public class Slow : EquipmentBase
{
    [Header("�X���[���ԂƑ��x")]
    [SerializeField] private int slowDuration;
    [SerializeField] private int speedDown;

    [Header("�����蔻��T�C�Y")]
    [SerializeField] private float boxWidth;
    [SerializeField] private float boxHeight;
    [SerializeField] private float offSet;
    
    [Header("Audio関係")]
    [SerializeField] private AudioClip slowClip;
    private AudioSource audioSource;

    public override void Activate(PlayerModel model)
    {
        Debug.Log("����");
        audioSource = model.GetComponent<AudioSource>();
        audioSource.PlayOneShot(slowClip);

        Vector2 ownerPos = model.transform.position;
        Vector2 hitPos = ownerPos + (Vector2)model.transform.right * offSet;
        Vector2 hitSize = new Vector2(boxWidth, boxHeight);

        Collider2D[] targetEnemy = Physics2D.OverlapBoxAll(hitPos, hitSize, 0);

        SlowSkill(targetEnemy);
    }

    private void SlowSkill(Collider2D[] targetEnemy)
    {
        foreach (var hit in targetEnemy)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyCore>()?.Slow(slowDuration, speedDown);
            }
        }
    }
}

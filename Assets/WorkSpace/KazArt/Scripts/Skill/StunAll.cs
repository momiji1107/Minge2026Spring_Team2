using UnityEngine;

[CreateAssetMenu(fileName = "StunAll", menuName = "ScriptableObjects/Skill/StunAll")]
public class StunAll : EquipmentBase
{
    [Header("�X�^����������")]
    [SerializeField] private float duration;

    public override void Activate(PlayerModel model)
    {
        Debug.Log("����");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if(enemy.GetComponent<Renderer>().isVisible)
            {
                if (enemy.gameObject.TryGetComponent<IEnemy>(out var core))
                {
                    core.Stun(duration);
                }
            }
        }
    }
}

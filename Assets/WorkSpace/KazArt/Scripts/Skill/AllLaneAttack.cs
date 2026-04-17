using UnityEngine;

[CreateAssetMenu(fileName = "AllLaneAttack", menuName = "ScriptableObjects/Skill/AllLaneAttack")]
public class AllLaneAttack : EquipmentBase
{
    [SerializeField] private GameObject bulletPrefab;

    [Header("ÉåÅ[Éìç¿ïWí≤êÆ")]
    [SerializeField] private float offSet;

    public override void Activate(PlayerModel model)
    {
        foreach(var lane in SceneContext.Instance.lanes)
        {
            float laneY = lane.transform.position.y;

            Vector2 spawnPos = new Vector2(
                model.transform.position.x,
                laneY + offSet
            );

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
            bullet.GetComponent<BulletBase>()?.SetDamage(model.Attack * level);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * model.ShootSpeed * level;
        }
    }
}

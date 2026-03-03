using UnityEngine;

[CreateAssetMenu(fileName = "MultipleShoot", menuName = "ScriptableObjects/Skill/MultipleShoot")]
public class MultipleShoot : EquipmentBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField,Tooltip("弾の高さ(プレイヤーからの距離)")] private Vector3[] offsets =
    {
        new Vector3(0, 2.3f, 0),
        new Vector3(0, 0, 0),
        new Vector3(0, -1.7f, 0)
    };
    public MultipleShoot()
    {
        name = "MultipleShoot";
    }
    
    public override void Activate(PlayerModel model)
    {
        foreach (var offset in offsets)
        {
            GameObject bullet = Instantiate(bulletPrefab, model.Player.transform.position + offset , Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * model.ShootSpeed * level;
            Destroy(bullet,2f);
        }
    }
}

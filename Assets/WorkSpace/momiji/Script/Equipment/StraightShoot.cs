using UnityEngine;

[CreateAssetMenu(fileName = "StraightShoot", menuName = "ScriptableObjects/BasicAttack/StraightShoot")]
public class StraightShoot : EquipmentBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField,Tooltip("弾速")] private float bulletSpeed;

    public StraightShoot()
    {
        name = "StraightShoot";
    }
    
    public override void Activate(PlayerModel model)
    {
        GameObject bullet = Instantiate(bulletPrefab, model.Player.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * bulletSpeed;
        
        Destroy(bullet,2f);
    }
}

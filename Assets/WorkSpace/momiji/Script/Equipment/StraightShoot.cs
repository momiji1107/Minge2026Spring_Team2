using UnityEngine;

[CreateAssetMenu(fileName = "StraightShoot", menuName = "ScriptableObjects/BasicAttack/StraightShoot")]
public class StraightShoot : EquipmentBase
{
    [SerializeField] private GameObject bulletPrefab;

    public StraightShoot()
    {
        name = "StraightShoot";
    }
    
    public override void Activate(PlayerModel model)
    {
        GameObject bullet = Instantiate(bulletPrefab, model.Player.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * model.ShootSpeed;
        
        Destroy(bullet,2f);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "StraightShoot", menuName = "ScriptableObjects/BasicAttack/StraightShoot")]
public class StraightShoot : EquipmentBase
{
    [SerializeField] private GameObject bulletPrefab;
    public bool flyEndless = false;

    public StraightShoot()
    {
        name = "StraightShoot";
    }
    
    public override void Activate(PlayerModel model)
    {
        GameObject bullet = Instantiate(bulletPrefab, model.Player.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletBase>()?.SetDamage(model.Attack);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * model.ShootSpeed;
        
        //flyEndless = falseだったら2秒後にこのオブジェクトを削除
        if(!flyEndless) Destroy(bullet,2f);
    }
}

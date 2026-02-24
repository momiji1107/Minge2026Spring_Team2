using UnityEngine;

[CreateAssetMenu(fileName = "MultipleShoot", menuName = "ScriptableObjects/Skill/MultipleShoot")]
public class MultipleShoot : EquipmentBase
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField, Tooltip("弾速")] private float bulletSpeed;

    public MultipleShoot()
    {
        name = "MultipleShoot";
    }
    
    public override void Activate(PlayerModel model)
    {
        GameObject bullet1 = Instantiate(bulletPrefab, model.Player.transform.position + new Vector3(0, 2.3f, 0) , Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPrefab, model.Player.transform.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(bulletPrefab, model.Player.transform.position + new Vector3(0, -1.7f, 0), Quaternion.identity);
        
        bullet1.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * bulletSpeed;
        bullet2.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * bulletSpeed;
        bullet3.GetComponent<Rigidbody2D>().linearVelocity = model.Player.transform.right * bulletSpeed;
        
        Destroy(bullet1,2f);
        Destroy(bullet2,2f);
        Destroy(bullet3,2f);
    }
}

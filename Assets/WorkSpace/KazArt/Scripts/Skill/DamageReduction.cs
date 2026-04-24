using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageReduction", menuName = "ScriptableObjects/Skill/DamageReduction")]
public class DamageReduction : EquipmentBase
{
    [Header("�_���[�W�y�����ʎ���")]
    [SerializeField] private int duration;
    [SerializeField] private bool isActive;
    [SerializeField] private GameObject effect;

    public bool IsActive => isActive;
    
    public override void Activate(PlayerModel model)
    {
        //init effect
        var obj = Instantiate(effect, model.transform.position, Quaternion.identity);
        if (!model.GetDirection)
        {
            var l = obj.transform.localScale;
            obj.transform.localScale = new Vector3(l.x * -1, l.y, l.z);
        }
        obj.transform.SetParent(model.transform);
        
        model.StartCoroutine(ActiveDamageReduce());
    }

    private IEnumerator ActiveDamageReduce()
    {
        isActive = true;
        yield return new WaitForSeconds(duration);
        isActive = false;
    }
}

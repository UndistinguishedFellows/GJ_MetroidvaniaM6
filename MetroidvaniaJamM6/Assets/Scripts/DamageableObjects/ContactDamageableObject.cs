using UnityEngine;

public class ContactDamageableObject : DamageableObject
{
    protected void OnTriggerEnter2D(Collider2D otherCollider)
    {
        DamageObject damageData = otherCollider.gameObject.GetComponent<DamageObject>();
        if (damageData == null)
        {
            Debug.LogWarning($"ContactDamageableObject trigger enter could not find DamageObject on collider: {otherCollider.gameObject.name}");
            return;
        }

        Damage(damageData.Damage);
    }
}

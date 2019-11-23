using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public virtual float Damage => m_baseDamage;
    
    [SerializeField] protected float m_baseDamage = 0f;

    public virtual void SetUp(float baseDamage)
    {
        m_baseDamage = baseDamage;
    }
}

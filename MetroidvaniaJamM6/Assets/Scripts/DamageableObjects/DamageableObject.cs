using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour
{
	[SerializeField] protected float m_maxHealth = 10f;
	[SerializeField] protected float m_initialHealth = 10f;

	[Tooltip("Arg0: Health modification, Arg1: New health percentage, Arg2: Is damage")]
	[SerializeField] protected HealthChangedEvent m_onHealthChanged = null;
	[SerializeField] protected DeathEvent m_onDeath = null;

	protected float m_currentHealth = 0f;

	public void Damage(float damageAmount)
	{
		ModifyHealth(-damageAmount);
	}

	public void Heal(float healthAmount)
	{
		ModifyHealth(healthAmount);
	}
	
	public void ModifyHealth(float amount)
	{
		float lastHealth = m_currentHealth;
		m_currentHealth += amount;
		m_onHealthChanged?.Invoke(m_currentHealth, m_currentHealth / m_maxHealth, m_currentHealth < lastHealth);
		
		if (m_currentHealth <= 0f)
		{
			m_onDeath?.Invoke();
		}
	}

	protected void Awake()
	{
		m_currentHealth = m_initialHealth;
	}

	protected void Start()
	{
		m_onHealthChanged?.Invoke(m_currentHealth, m_currentHealth / m_maxHealth, false);
	}

	[System.Serializable] protected class HealthChangedEvent : UnityEvent<float, float, bool> { }
	[System.Serializable] protected class DeathEvent : UnityEvent { }
}

using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : MonoBehaviour
{
	public float CurrentHealth => m_currentHealth;
	public float MaxHealth => m_maxHealth;
	public float CurrentHealthPercentage => m_currentHealth / m_maxHealth;
	
	[SerializeField] protected float m_maxHealth = 10f;
	[SerializeField] protected float m_initialHealth = 10f;

	[Tooltip("Arg0: New health, Arg1: New health percentage, Arg2: Change cause")]
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
		m_onHealthChanged?.Invoke(m_currentHealth, CurrentHealthPercentage, m_currentHealth < lastHealth ? HealthChangeCause.Damage : HealthChangeCause.Heal);
		
		if (m_currentHealth <= 0f)
		{
			m_onDeath?.Invoke();
		}
	}

	public void Set(float maxHealth, float currentHealth, bool notify = true)
	{
		m_maxHealth = maxHealth;
		m_currentHealth = currentHealth;
		if (notify)
		{
			m_onHealthChanged?.Invoke(m_currentHealth, CurrentHealthPercentage, HealthChangeCause.None);
		}
	}

	protected void Awake()
	{
		m_currentHealth = m_initialHealth;
	}

	protected void Start()
	{
		m_onHealthChanged?.Invoke(m_currentHealth, m_currentHealth / m_maxHealth, HealthChangeCause.None);
	}

	public enum HealthChangeCause
	{
		None,
		Damage,
		Heal
	};
	
	[System.Serializable] protected class HealthChangedEvent : UnityEvent<float, float, HealthChangeCause> { }
	[System.Serializable] protected class DeathEvent : UnityEvent { }
}

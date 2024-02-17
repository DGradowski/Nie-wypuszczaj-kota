using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
	[SerializeField] Transform barFill;
	[SerializeField] TMP_Text maxHealthText;
	[SerializeField] TMP_Text currentHealthText;
	private float maxHealth;
	public float currentHealth;
    
	private void UpdateHealhtBar()
	{
		barFill.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
		UpdateText();
	}

	private void UpdateText()
	{
		maxHealthText.text = maxHealth.ToString();
		currentHealthText.text = currentHealth.ToString();
	}

	public void ApplyDamage(float damage)
	{
		currentHealth -= damage;
		if (currentHealth < 0) currentHealth = 0;
		UpdateHealhtBar();
	}

	public void SetHealth(float health)
	{
		maxHealth = health;
		currentHealth = health;
		UpdateHealhtBar();
	}
}

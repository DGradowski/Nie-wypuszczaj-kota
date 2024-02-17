using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class CombatManager : MonoBehaviour
{
	[Header("Fighters")]
	public GameObject player;
	[SerializeField] HealthBar playerHealthBar;
	public GameObject enemy;
	[SerializeField] HealthBar enemyHealthBar;

	[Header("Player Stats")]
	[SerializeField] float health;
	[SerializeField] float attackDamage;
	[SerializeField] float attackRange;

	[Header("Enemy Stats")]
	[SerializeField] float enemyHealth;
	[SerializeField] float enemyAttackDamage;
	[SerializeField] float enemyAttackRange;


	[Header("Other")]
	[SerializeField] bool playerTurn;
	[SerializeField] float waitTime;
	[SerializeField] float waitTimer;
	[SerializeField] GameObject playerPanel;
	[SerializeField] TMP_Text actionInfoText;
	[SerializeField] Animator playerAnimator;

	private float dealtDamage;

	private void Start()
	{
		playerHealthBar.SetHealth(health);
		enemyHealthBar.SetHealth(enemyHealth);
	}

	private void SetTimer(float time = 2f)
	{
		waitTime = time;
		waitTimer = 0f;
	}

	private float CalculateDamage(float damage, float range)
	{
		dealtDamage = MathF.Ceiling(damage + UnityEngine.Random.Range(-range, range));
		return dealtDamage;
			}

	public void PlayerAttack()
	{
		enemyHealthBar.ApplyDamage(CalculateDamage(attackDamage, attackRange));
		ShowActionInfo("Kot", 0);
		playerPanel.SetActive(false);
		playerAnimator.SetTrigger("Attack");
		SetTimer();
		ChangeTurn();
	}

	public void PlayerSneer()
	{
		ShowActionInfo("Kot", 2);
		playerPanel.SetActive(false);
		playerAnimator.SetTrigger("Sneer");
		SetTimer();
		ChangeTurn();
	}

	public void PlayerWait()
	{
		ShowActionInfo("Kot", 1);
		playerPanel.SetActive(false);
		SetTimer();
		ChangeTurn();
	}

	public void EnemyTurn()
	{
		if (enemyHealthBar.currentHealth < 100)
		{
			dealtDamage = 9999;
			playerHealthBar.ApplyDamage(dealtDamage);
			ShowActionInfo("Sczuras", 0);
		}
		else
		{
			ShowActionInfo("Sczuras", 1);
		}
		SetTimer();
		ChangeTurn();
	}

	private void MakeTurn()
	{
		if (playerTurn)
		{
			playerPanel.SetActive(true);
		}
		else
		{
			EnemyTurn();
		}
	}

	private void ChangeTurn()
	{
		if (playerTurn) playerTurn = false;
		else playerTurn = true;
	}

	private void ShowActionInfo(string fighterName, int actionId)
	{
		string actionInfo = "";
		actionInfo += fighterName;
		switch (actionId)
		{
			case 0:
				actionInfo += " uderza za " + dealtDamage.ToString() + " obra¿en.";
				break;
			case 1:
				actionInfo += " nic nie robi.";
				break;
			case 2:
				actionInfo += " drwi z przeciwnika.";
				break;
		}
		actionInfoText.text = actionInfo;
	}



	void Update()
	{
		if (waitTimer < waitTime) waitTimer += Time.deltaTime;
		else MakeTurn();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] bool showOnHeadHealthBar = true;
    [SerializeField] StatsBar onHeadHealthBar;
    protected override void OnEnable()
    {
        health = maxHealth;

        if (showOnHeadHealthBar)
        {
            ShowOnHeadHealthBar();
        }
        else
        {
            HideOnHeadHealthBar();
        }
    }
    public void ShowOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(true);
        onHeadHealthBar.Initialize(health, maxHealth);
    }
    public void HideOnHeadHealthBar()
    {
        onHeadHealthBar.gameObject.SetActive(false);
    }
    public override void TakeDamage(float damage)
    {
        health -= damage;
        if (showOnHeadHealthBar && gameObject.activeSelf)
        {
            onHeadHealthBar.UpdateStats(health, maxHealth);
        }
        if (health <= 0f)
        {
            Die();
        }
    }
    public override  void RestoreHealth(float value)
    {
        if (health == maxHealth) return;

        health = Mathf.Clamp(health + value, 0f, maxHealth);
        if (showOnHeadHealthBar)
        {
            onHeadHealthBar.UpdateStats(health, maxHealth);
        }
    }
    public override void Die()
    {
        EnemyManager.Instance.RemoveFromList(gameObject);
        base.Die();
    }
}

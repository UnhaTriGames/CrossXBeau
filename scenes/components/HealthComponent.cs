using Godot;
using System;

public partial class HealthComponent : Node2D
{
	[Export]
	private int maxHealth;
	private int currentHealth;

	[Signal]
	public delegate void HealthChangedEventHandler(int newHealth);
	[Signal]
	public delegate void HealthDepletedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentHealth = maxHealth;
	}

	public void SetMaxHealth(int newMaxHealth)
	{
		maxHealth = newMaxHealth;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	}

	public void SetCurrentHealth(int newCurrentHealth) 
	{
		currentHealth = Mathf.Clamp(newCurrentHealth, 0, maxHealth);
		EmitSignal(nameof(HealthChangedEventHandler), currentHealth);
		if (currentHealth <= 0) { EmitSignal(nameof(HealthDepletedEventHandler)); }
	}

	public void DamageHealth(int damageAmount)
	{
		SetCurrentHealth(currentHealth - damageAmount);
	}

	public void HealHealth(int healAmount)
	{
		SetCurrentHealth(currentHealth + healAmount);
	}

	public int GetCurrentHealth()
	{
		return currentHealth;
	}
}

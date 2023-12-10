using Godot;
using Godot.Collections;
using System;

public partial class VelocityComponent : Node
{
	[Export]
	private float maxSpeed = 100;

	[Export]
	private float accelerationCoefficient = 40;

	public Vector2 Velocity { get; set; }
	public float SpeedMultiplier {get; set;} = 1f;
	public float AcceleractionCoefficientMultiplier {get; set;} = 1f;
	public float AccelerationCoefficient => accelerationCoefficient;
	public float SpeedPercent => Mathf.Min(Velocity.Length() / (CalculatedMaxSpeed > 0f ? CalculatedMaxSpeed : 1f), 1f);
	public float CalculatedMaxSpeed => maxSpeed * SpeedMultiplier; // * (1f + speedPercentModifier)

	public void AccelerateToVelocity(Vector2 velocity)
	{
		float time = 1f - Mathf.Exp(-accelerationCoefficient * AcceleractionCoefficientMultiplier * (float)GetPhysicsProcessDeltaTime());
		Velocity = Velocity.Lerp(velocity, time);
	}
	
	public void AccelerateInDirection(Vector2 direction)
	{
		AccelerateToVelocity(direction * CalculatedMaxSpeed);
	}

	public Vector2 GetMaxVelocity(Vector2 direction)
	{
		return direction * CalculatedMaxSpeed;
	}

	public void MaximizeVelocity(Vector2 direction)
	{
		Velocity = GetMaxVelocity(direction);
	}

	public void Decelerate()
	{
		AccelerateToVelocity(Vector2.Zero);
	}

	public void Move(CharacterBody2D characterBody2D)
	{
		characterBody2D.Velocity = Velocity;
		characterBody2D.MoveAndSlide();
	}
	
	public void SetMaxSpeed(float newSpeed)
	{
		maxSpeed = newSpeed;
	}
}

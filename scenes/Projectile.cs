using Godot;
using System;

public partial class Projectile : CharacterBody2D
{
	[Export]
	private VelocityComponent velocityComponent;

	public Vector2 Direction {get; set;}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		velocityComponent.AccelerateInDirection(Direction);
		velocityComponent.Move(this);
	}
}

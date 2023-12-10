using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private VelocityComponent velocityComponent;
	[Export]
	private AnimatorComponent animatorComponent;
	//private HealthComponent healthComponent;
	//private HitboxComponent hitboxComponent;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 playerVelocity = Vector2.Zero;
		if (Input.IsActionPressed("moveRight")) playerVelocity.X += 1;
		if (Input.IsActionPressed("moveLeft")) playerVelocity.X -= 1;
		if (Input.IsActionPressed("moveUp")) playerVelocity.Y -= 1;
		if (Input.IsActionPressed("moveDown")) playerVelocity.Y += 1;
		animatorComponent.Animate(playerVelocity);

		// Running - maybe change this to a dash/roll
		velocityComponent.SpeedMultiplier = 1f;
		if (Input.IsActionPressed("pressRun")) velocityComponent.SpeedMultiplier = 1.5f;

		velocityComponent.AccelerateInDirection(playerVelocity.Normalized());
		velocityComponent.Move(this);
    }
}

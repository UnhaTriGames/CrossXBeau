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
	[Export]
	private PackedScene projectile;
	[Export]
	private Marker2D firePoint;
	[Export]
	private float firePointRadius;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 playerVelocity = Vector2.Zero;
		if (Input.IsActionPressed("moveRight")) { playerVelocity.X += 1; RotateFirePoint(playerVelocity); }
		if (Input.IsActionPressed("moveLeft")) { playerVelocity.X -= 1; RotateFirePoint(playerVelocity); }
		if (Input.IsActionPressed("moveUp")) { playerVelocity.Y -= 1; RotateFirePoint(playerVelocity); }
		if (Input.IsActionPressed("moveDown")) { playerVelocity.Y += 1; RotateFirePoint(playerVelocity); }
		animatorComponent.Animate(playerVelocity);

		// Running - maybe change this to a dash/roll
		velocityComponent.SpeedMultiplier = 1f;
		if (Input.IsActionPressed("pressRun")) velocityComponent.SpeedMultiplier = 1.5f;

		if (Input.IsActionJustPressed("pressInteract")) { Shoot(); }

		velocityComponent.AccelerateInDirection(playerVelocity.Normalized());
		velocityComponent.Move(this);
    }

	private void RotateFirePoint(Vector2 playerVelocity)
	{
		firePoint.Transform = new Transform2D(playerVelocity.Angle(), playerVelocity * firePointRadius);
	}

	// Shoot arrow
	public void Shoot()
	{
		Projectile instancedProjectile = (Projectile)projectile.Instantiate();
		Owner.AddChild(instancedProjectile);
		instancedProjectile.GlobalTransform = firePoint.GlobalTransform;
		instancedProjectile.Direction = firePoint.Position.Normalized();
	}
}

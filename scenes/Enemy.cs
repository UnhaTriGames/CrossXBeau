using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export]
	private VelocityComponent velocityComponent;
	[Export]
	private AnimatorComponent animatorComponent;

	public override void _PhysicsProcess(double delta)
	{
		
	}
}

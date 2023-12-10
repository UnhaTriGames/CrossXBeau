using Godot;
using System;

public partial class AnimatorComponent : Node
{
	[Export]
	public AnimatedSprite2D animatedSprite2D;
	
	public void Animate(Vector2 velocity)
	{
		if (velocity.Length() > 0) animatedSprite2D.Play();
		else animatedSprite2D.Stop();

		if (velocity.X > 0) animatedSprite2D.Animation = "right";
		else if (velocity.X < 0) animatedSprite2D.Animation = "left";
		else if (velocity.Y > 0) animatedSprite2D.Animation = "down";
		else if (velocity.Y < 0) animatedSprite2D.Animation = "up";
	}

	// actions? 
}

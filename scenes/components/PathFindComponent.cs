using Godot;
using System;

public partial class PathFindComponent : Node2D
{
	[Export]
	public NavigationAgent2D NavigationAgent2D {get; private set;}
	[Export]
	private Timer intervalTimer;
	[Export]
	private VelocityComponent velocityComponent;
	[Export]
	private bool debugDrawEnabled;
	public Node2D Target { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//NavigationAgent2D.PathDesiredDistance = 4;
		//NavigationAgent2D.TargetDesiredDistance = 4;
		intervalTimer.Start();
		//NavigationAgent2D.Connect("velocity_computed", new Callable(this, nameof(OnVelocityComputed)));
		SetProcess(OS.IsDebugBuild() && debugDrawEnabled);
	}

	public override void _Draw()
	{
		if (OS.IsDebugBuild() && debugDrawEnabled)
		{
			for(int i = 0; i< NavigationAgent2D.GetCurrentNavigationPath().Length; i++)
			{
				Vector2 point = NavigationAgent2D.GetCurrentNavigationPath()[i];
				DrawCircle(ToLocal(point), 3f, Colors.BlueViolet);
				if (i > 0)
				{
					Vector2 prevPoint = NavigationAgent2D.GetCurrentNavigationPath()[i-1];
					DrawLine(ToLocal(prevPoint), ToLocal(point), Colors.Blue, 2f);
				}
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		QueueRedraw();
		
	}

	public void OnTimerTimeout()
	{
		if (Target != null)
		{
			//GD.Print("[TIMEOUT]: Target == "+ Target.GlobalPosition);
			SetTargetPosition(Target.GlobalPosition);
		}
	}

	public void SetTargetPosition(Vector2 targetPosition)
	{
		//if (!intervalTimer.IsStopped()) { return;}
		//GD.Print("TIMER IS STOPPED");
		intervalTimer.Call("start");
		NavigationAgent2D.TargetPosition = targetPosition;
		//GD.Print("TargetPos Set");
	}

	public void ForceSetTargetPosition(Vector2 targetPosition)
	{
		NavigationAgent2D.TargetPosition = targetPosition;
		intervalTimer.Call("start");
	}

	public void FollowPath()
	{
		if (NavigationAgent2D.IsNavigationFinished())
		{
			//GD.Print("Navigation is Done");
			velocityComponent.Decelerate();
			return;
		}

		Vector2 direction = (NavigationAgent2D.GetNextPathPosition() - GlobalPosition).Normalized();
		velocityComponent.AccelerateInDirection(direction);
		NavigationAgent2D.SetVelocityForced(velocityComponent.Velocity);
	}

	private void OnVelocityComputed(Vector2 velocity)
	{
		Vector2 newDirection = velocity.Normalized();
		Vector2 currentDirection = velocityComponent.Velocity.Normalized();
		Vector2 halfwayDirection = newDirection.Lerp(currentDirection, 1f - Mathf.Exp(velocityComponent.AccelerationCoefficient * (float)GetPhysicsProcessDeltaTime()));
		velocityComponent.Velocity = halfwayDirection * velocityComponent.Velocity.Length();
	}
}

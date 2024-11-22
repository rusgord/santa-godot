using Godot;
using System;

public partial class Player : Area2D
{
	public AnimatedSprite2D animatedSprite2D;
	[Export]
	public int Speed {get; set; } = 400;
	public Vector2 ScreenSize;
	
	[Signal]
	public delegate void HitEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
{
	var velocity = Vector2.Zero; // The player's movement vector.

	if (Input.IsActionPressed("move_right"))
	{
		velocity.X += 1;
	}

	if (Input.IsActionPressed("move_left"))
	{
		velocity.X -= 1;
	}

	if (Input.IsActionPressed("move_down"))
	{
		velocity.Y += 1;
	}

	if (Input.IsActionPressed("move_up"))
	{
		velocity.Y -= 1;
	}

	

	if (velocity.Length() > 0)
	{
		velocity = velocity.Normalized() * Speed;
		animatedSprite2D.Play();
	}
	else
	{
		animatedSprite2D.Stop();
	}
	Position += velocity * (float)delta;
	Position = new Vector2(
		x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
		y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
	);
	if (velocity.X != 0)
	{
		animatedSprite2D.Animation = "walk";
		animatedSprite2D.FlipV = false;
		// See the note below about the following boolean assignment.
		animatedSprite2D.FlipH = velocity.X > 0;
	}
	}	
	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnBodyEntered(Node2D body)
	{
		Hide(); // Player disappears after being hit.
		EmitSignal(SignalName.Hit);
		// Must be deferred as we can't change physics properties on a physics callback.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);

	}
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}

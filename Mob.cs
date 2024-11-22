using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animatedSprite2D.Play();
	}

	public void Stop()
	{
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}

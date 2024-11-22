using Godot;
using System;

public partial class Menu : Control
{
	public Button StartButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartButton = GetNode<Button>("%StartButton");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://main.tscn");
	}
}

using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;

	public Button buttonRestart;

	public Button buttonMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
		buttonRestart = GetNode<Button>("%RestartButton");
		buttonMenu = GetNode<Button>("%MenuButton");
		buttonRestart.Hide();
		buttonMenu.Hide();
	}	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		foreach (var Enemy in GetTree().GetNodesInGroup("Enemy"))
		{
			if (Enemy is Mob)
			{
				Mob mob = Enemy as Mob;
				mob.LinearVelocity = new Vector2(0, 0);
				mob.Stop();
			}
		}

		buttonRestart.Show();
		//buttonMenu.Show();
	}

	public void NewGame()
	{
		
		_score = 0;

		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartedPosition");
		player.Start(startPosition.Position);

		GetNode<Timer>("StartTimer").Start();
		GD.Print("Timers restarted");
		}
	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnScoreTimerTimeout()
	{
		_score++;
	}

	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
	// We also specified this function name in PascalCase in the editor's connection window.
	private void OnMobTimerTimeout()
	{
		// Create a new instance of the Mob scene.
		Mob mob = MobScene.Instantiate<Mob>();

		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mob.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)150.0, 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}

	public void OnRestartButtonPressed()
	{
		GD.Print("Restart button pressed");
		buttonRestart.Hide();
		buttonMenu.Hide();
		NewGame();
		
	}

	public void OnMenuButtonPressed()
	{
		GD.Print("Menu button pressed");
		buttonRestart.Hide();
		buttonMenu.Hide();
		GetTree().ChangeSceneToFile("res://menu.tscn");
	}
}

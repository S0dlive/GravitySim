using Godot;
using System;
using System.Collections.Generic;
using GravitySim;

public partial class GravitySimulator : Node3D
{
	private const double G = 6.67430e-3; 
	[Export]
	public PackedScene CelesyBodyPrefab { get; set; }
	private List<CelestBody> bodies = new List<CelestBody>();

	public override void _Ready()
	{
		CreateRandomSystem();
	}

	private void CreateRandomSystem(int nbr = 20)
	{
		
		var sun = CelesyBodyPrefab.Instantiate<CelestBody>();

		sun.Mass = 500000;
		sun.Position = new Vector3(0, 0, 0);
		sun.Scale = new Vector3(3, 3, 3);
		AddChild(sun);
		bodies.Add(sun);
		GD.Print("sun spawn");
		
		for (var t = 0; t < nbr; t++)
		{
			Random random = new Random();
			var earth = CelesyBodyPrefab.Instantiate<CelestBody>();
			earth.Mass = random.Next(900, 1200);
			earth.Position = new Vector3(random.Next(-50, 50), random.Next(-50, 50), random.Next(-50, 50));
			earth.Velocity = new Vector3(0, 0, random.Next(0, 5)); 
			AddChild(earth);
			bodies.Add(earth);
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		foreach (var body in bodies)
		{
			Vector3 totalForce = Vector3.Zero;
			foreach (var other in bodies )
			{
				if (body == other) continue;
				
				Vector3 diff =  other.Position - body.Position;
				double dist = Math.Max(diff.Length(), 1);
				double force = G * (body.Mass * other.Mass) / (dist * dist);
				totalForce += diff.Normalized() * (float)force;
			}
			body.ApplyForce(totalForce, delta);
		}
	}
}

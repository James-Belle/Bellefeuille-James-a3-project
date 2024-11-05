using System;
using System.Numerics;


namespace Game10003
{
	public class Debris
	{
		public Vector2 debrisPosition;

		Vector2 speed;
		int size = 15;


		public Debris()
		{
			
            speed.X = Random.Float((float)-2, (float)-1);
            speed.Y = Random.Float((float)-1, (float)1);
            debrisPosition.X = 700;
			debrisPosition.Y = 300;
        }

		public void DebrisSpawn()
		{

		}

		public void DebrisMove()
		{
			debrisPosition += speed;
			if (debrisPosition.X < 200 || debrisPosition.Y < 0 || debrisPosition.Y > 600)
			{
				debrisPosition.X = 700;
				debrisPosition.Y = 300;
                speed.X = Random.Float((float)-2, (float)-1);
                speed.Y = Random.Float((float)-1, (float)1);
            }
			Draw.Circle(debrisPosition, size);
		}
		public bool DebrisCollide(Vector2 oldMousePosition, Vector2 newMousePosition, bool switcher)
		{
			
			if (Math.Abs(debrisPosition.Y-newMousePosition.Y) < size + 15 && Math.Abs(debrisPosition.X-newMousePosition.X) < size+25 && !switcher)
			{
				Console.WriteLine("colide");
				speed += (newMousePosition - oldMousePosition) /15;
				return (true);
            }
			return (switcher);
		}
	}
}
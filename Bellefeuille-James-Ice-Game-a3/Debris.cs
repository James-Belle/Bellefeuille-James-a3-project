using System;
using System.Numerics;


namespace Game10003
{
	public class Debris
	{
		public Vector2 debrisPosition;

		Vector2 speed;
		Vector2 size;


		public Debris()
		{
			float max = 25;
            speed.X = Random.Float((float)-2, (float)-1);
            //speed.Y = Random.Float((float)-1, (float)1);
            speed.Y = 0;
            debrisPosition.X = 700;
			debrisPosition.Y = 300;
			size = Random.Vector2(10, max, 10, max);
        }

		public void DebrisSpawn()
		{

		}

		public void DebrisMove()
		{
			debrisPosition += speed;


			if (debrisPosition.X < 200 || debrisPosition.Y < 0 || debrisPosition.Y > 600)
			{
				debrisPosition.X = 700; // temporary reuse of debris, eventually will just delete.
				debrisPosition.Y = 300;
                speed.X = Random.Float((float)-2, (float)-1);
				//speed.Y = Random.Float((float)-1, (float)1);
				speed.Y = 0;
            }

			Draw.FillColor = Color.Black;
			Draw.Ellipse(debrisPosition, size);
		}
		public bool DebrisCollide(Vector2 collisionPosition) // this function lags a bit, i may need to optimize. maybe call it every few frames.
		{
			//checks if mouse collides with debris
			if (Math.Abs(debrisPosition.Y- collisionPosition.Y) < size.Y + 5 && Math.Abs(debrisPosition.X- collisionPosition.X) < size.X+15)
			{
				return true;
            }
			return false;
		}
		public void MouseCollide(float collisionPosition)
		{
            if (collisionPosition < debrisPosition.Y)
            {
                // is the mouse is above the debris it moves it down
                speed.Y += (float)0.5;
            }
            else
            {  //if bellow it moves debris up
                speed.Y -= (float)0.5;
            }
        }
	}
}
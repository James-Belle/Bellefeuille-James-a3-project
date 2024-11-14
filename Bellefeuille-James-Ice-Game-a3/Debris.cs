using System;
using System.Numerics;


namespace Game10003
{
	public class Debris
	{
		public Vector2 debrisPosition;

		Vector2 speed;
		public Vector2 size;
        public float avgSize;
        float max = 20;
        public Debris()
		{
			max = 50;
            speed.X = -1;
            //speed.X = Random.Float((float)-2, (float)-1);
            //speed.Y = Random.Float((float)-1, (float)1);
            speed.Y = 0;
            debrisPosition.X = Random.Integer(600, 2000);
            debrisPosition.Y = Random.Integer(200, 400);
            size = Random.Vector2(10, max, 10, max);

			avgSize = size.X + size.Y / 2;
    }

		public void DebrisSpawn()
		{

		}

		public void DebrisMove(float speedIncrease)
		{
			speed.X = speedIncrease;
			debrisPosition += speed;
			if (Math.Abs(speed.Y) > 0)
			{ // might remove
				speed.Y *= (float)0.95;
			}
			// if the debis goes out of bounds it is deleted/repurposed
			if (debrisPosition.X < -size.X || debrisPosition.Y < 0 || debrisPosition.Y > 600)
			{
				DebrisDelete();
            }

			Draw.FillColor = Color.Black;
			Draw.Ellipse(debrisPosition, size);
		}
		public bool DebrisCollide(Vector2 collisionPosition,float objectSize) // this function lags a bit, i may need to optimize. maybe call it every few frames.
		{

			//checks if mouse collides with debris
			if (Math.Abs(debrisPosition.Y- collisionPosition.Y) < size.Y + objectSize && Math.Abs(debrisPosition.X- collisionPosition.X) < size.X + objectSize)
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
                speed.Y += (float)10/(avgSize); // this bigger the debris the less it moves
            }
            else
            {  //if bellow it moves debris up
                speed.Y -= (float)10/(avgSize);
            }
        }
		public void DebrisDelete()
		{
			max++;
			//this will reclocate the debris.
            debrisPosition.X = Random.Integer(800, 2000);
            debrisPosition.Y = Random.Integer(200, 400);
            size = Random.Vector2(10, max, 10, max);
            speed.X = -1;
            speed.Y = 0;

        }
	}
}
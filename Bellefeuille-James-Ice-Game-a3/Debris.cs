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
        public float max = 30;
		Color colDarkerGrey = new Color(50, 50, 50);
		public Debris()
		{
			max = 20; // max debris size
			speed.X = -1;
			speed.Y = 0;
			debrisPosition.X = Random.Integer(600, 2000); // randomizes position
			debrisPosition.Y = Random.Integer(200, 400);
			size = Random.Vector2(10, max, 10, max); //randomizes size
			avgSize = size.X + size.Y / 2;
		}

		public void DebrisMove(float speedIncrease)
		{ // moves debris
			speed.X = speedIncrease;
			debrisPosition += speed;
			if (Math.Abs(speed.Y) > 0)
			{ //slows, vertical speed
				speed.Y *= (float)0.95;
			}
			if (debrisPosition.X < -size.X || debrisPosition.Y < 0 || debrisPosition.Y > 600)
            {// if the debis goes out of bounds it is deleted/repurposed
                DebrisDelete();
            }
			Draw.FillColor = colDarkerGrey;
			Draw.LineColor = Color.DarkGray;
			Draw.Ellipse(debrisPosition, size);
			
        }
		public bool DebrisCollide(Vector2 collisionPosition,float objectSize) // this function lags a bit, i may need to optimize. maybe call it every few frames.
        { //checks if mouse collides with debris
            if (Math.Abs(debrisPosition.Y- collisionPosition.Y) < size.Y + objectSize && Math.Abs(debrisPosition.X- collisionPosition.X) < size.X + objectSize)
			{
				return true;
            }
			return false;
		}
		public void MouseCollide(float collisionPosition, int upgrade)
		{
            if (collisionPosition < debrisPosition.Y)
            { // is the mouse is above the debris it moves it down
                speed.Y += (1+ (float)upgrade*3)/(avgSize); // this bigger the debris the less it moves
            }
            else
            {  //if bellow it moves debris up
                speed.Y -= (1+(float)upgrade*3)/(avgSize);
            }
        }
		public void DebrisDelete()
		{
			max += 4; // this increases the potential size of the debris
            debrisPosition.X = Random.Integer(800, 2000); //this will reclocate the debris.
            debrisPosition.Y = Random.Integer(200, 400);
            size = Random.Vector2(10, max, 10, max); //changes debris size
            speed.X = -1;
            speed.Y = 0;
            avgSize = size.X + size.Y / 2;
        }
	}
}
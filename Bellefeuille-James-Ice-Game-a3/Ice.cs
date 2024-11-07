using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace Game10003
{
	public class Ice
	{
		public Vector2 position;
		
		public float size;
		public float iceSpeed;
		int frame = 0;
		public Ice()
		{
			// i love programming
			position = new Vector2(100, 300);
			size = 20;
			iceSpeed =(float) 0.01;
			
		}
		public float iceSlow(float totalSpeed)
		{
			totalSpeed += iceSpeed / size;
			size -=(float) 0.001;


			return (totalSpeed);
		}
		
		public void IceDraw(float speedTest, Color iceBlue)
		{
			frame++;
			float smallSize = size/8;
			Draw.FillColor = iceBlue;
			Draw.LineColor = iceBlue;
			Draw.Capsule(0, 300+smallSize*4, position.X+smallSize*2, position.Y+smallSize*4, smallSize*4);
			Draw.LineColor = Color.Black;
            if (frame <= 10)
			{
				Draw.Square(position, size);
			}
			else if (frame <= 20)
			{ // these frames let the cube spin
                Draw.Quad(position.X - smallSize, position.Y + smallSize * 5,
                    position.X + smallSize * 3, position.Y - smallSize,
                    position.X + smallSize * 9, position.Y + smallSize * 3,
                    position.X + smallSize * 5, position.Y + smallSize * 9);

            }
			else if (frame <= 30)
			{
                Draw.Quad(position.X - smallSize, position.Y + smallSize * 3,
                    position.X + smallSize * 5, position.Y - smallSize,
                    position.X + smallSize * 9, position.Y + smallSize * 5,
                    position.X + smallSize * 3, position.Y + smallSize * 9);
                
            }
			else
			{
                Draw.Square(position, size);
                frame = 0;
				Console.WriteLine(speedTest);
			}
		}
	}
}
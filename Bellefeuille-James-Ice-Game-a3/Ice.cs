using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace Game10003
{
	public class Ice
	{
		Vector2 position;
		float size;
		Vector2 speed;
		int frame = 0;
		public Ice()
		{
			// i love programming
			position = new Vector2(20, 20);
			size = 20;
			speed = new Vector2(0, 0);
			
		}
		public void IceDraw()
		{
			frame++;

			float smallSize = size/8;
            
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
			}
		}
	}
}
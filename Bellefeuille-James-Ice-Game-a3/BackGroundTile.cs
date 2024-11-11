﻿using System;
using System.Numerics;
namespace Game10003
{
	public class BackGroundTile
	{

		public Vector2 position;
		public Color colTileColour;
		public BackGroundTile()
		{
			float Brightness = Random.Float(4, 6);
			int red = (int)Math.Round(Random.Float(28, 31) * Brightness,0);

			int green = (int)Math.Round(Random.Float(13, 15) * Brightness, 0);

			int blue = (int)Math.Round(Random.Float(0, 10) * Brightness / 2,0);

            colTileColour = new Color(red, green, blue); // this creates a random brown color
			position = Vector2.Zero;
		}
		public void DrawTile()
		{
			Vector2 size = new Vector2(140, 40);
			Draw.FillColor = colTileColour;
			Draw.Rectangle(position, size);
		}

		public void OffScreen()
		{
			if(position.X < -140)
			{
				position.X += 140 * 10;
            }
			
			
		}
		public void Move(float speed)
		{
			position.X -= speed;
		}
	}
}
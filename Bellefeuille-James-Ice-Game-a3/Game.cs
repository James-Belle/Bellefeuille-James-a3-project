// Include code libraries you need below (use the namespace).
using Raylib_cs;
using System;
using System.Drawing;
using System.Numerics;
using System.Threading;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Vector2 mouseBrush = new Vector2(50, 10);
        Debris deb = new Debris(); // will probably change into an array
        Color colBrown = new Color(180, 100, 90);
        Ice ice = new Ice();
        Color colUiBack = new Color(50, 20, 100);
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            //drawes ui
            //Draw.FillColor = colUiBack;
            //Draw.Rectangle(0, 0, 200, 600);


            Vector2 mousePosition = Input.GetMousePosition();

            bool startGame = true;

            if (startGame)
            {

                ice.IceDraw();
                if (mousePosition.X > 200)
                { // this will draw a brush on the mouse
                    Draw.FillColor = colBrown;
                    Draw.Ellipse(mousePosition, mouseBrush);

                    // this will check if the brush is touching the debris
                    if (deb.DebrisCollide(mousePosition))
                    {
                        deb.MouseCollide(mousePosition.Y);
                    }
                }

                deb.DebrisMove();
            }

            
            
        }
    }
}

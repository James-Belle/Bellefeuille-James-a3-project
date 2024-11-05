// Include code libraries you need below (use the namespace).
using Raylib_cs;
using System;
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
        Debris deb = new Debris();
        Vector2 lastMousePos = new Vector2(0,0);
        int timer = 0;
        bool colide = false;


        Vector2 storedMousePos = new Vector2(0,0);
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
            Window.ClearBackground(Color.White);
            Draw.Rectangle(0, 0, 200, 600);

            Vector2 mousePosition = Input.GetMousePosition();


            //
            // collision check here
            colide = deb.DebrisCollide(lastMousePos, mousePosition, colide);
            if (!colide)
            {
                storedMousePos = lastMousePos;

            }
            else
            {
                Draw.LineColor = Color.Red;
                Draw.Capsule(lastMousePos, storedMousePos, 5); // this shows the trajectory of your mouse colliding with the debris
                Draw.LineColor = Color.Black;
                timer++;
            }


            //
            //
            if (timer > 20)
            {
                timer = 0;
                Console.WriteLine(mousePosition - lastMousePos);
                colide = false;
            }
            
            

            if (mousePosition.X > 200)
            {
                Draw.Ellipse(mousePosition, mouseBrush);
            }

            deb.DebrisMove();

            if (!colide)
            {
                lastMousePos = mousePosition;
            }
            
        }
    }
}

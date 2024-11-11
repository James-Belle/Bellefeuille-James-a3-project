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
        //Debris deb = new Debris(); // will probably change into an array
        Color colBrown = new Color(180, 100, 90);
        Ice ice = new Ice();
        Color colUiBack = new Color(50, 20, 100);
        Debris[] obstacles = new Debris[20];
        Vector2 mousePosition = Vector2.Zero;
        Color colIceBlue = new Color(179, 240, 245);
        BackGroundTile[] backGround = new BackGroundTile[150];
        bool mainMenu = true;
        bool startGame = false;
        float backgroundSpeed = -5;


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 600);
            for (int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i] = new Debris();
            }
            for (int yPos = 0; yPos < 15; yPos++)
            {
                for (int xPos = 0; xPos < 10; xPos++)
                {
                    backGround[xPos + yPos * 10] = new BackGroundTile();
                    int xOffset = 0;
                    if (yPos %2 == 0)
                    {
                        xOffset = -70;
                    }
                    backGround[xPos + yPos * 10].position = new Vector2(xPos * 140+xOffset, yPos * 40);

                }
            }

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


            mousePosition = Input.GetMousePosition();
            if (mainMenu)
            {
                Draw.FillColor = Color.OffWhite;
                
                Draw.Rectangle(300, 200, 200, 150);
                Text.Size = 25;
                Text.Draw("Start The Game!", 300, 250);
                if (Input.IsMouseButtonPressed(MouseInput.Left))
                {

                    if (mousePosition.X > 350 && mousePosition.Y > 250 && mousePosition.X < 500 && mousePosition.Y < 350)
                    {
                        startGame = true;
                        mainMenu = false;
                    }

                }
            }

            


            if (startGame)
            {
                // background tileing


                foreach (BackGroundTile tile in backGround)
                {

                    tile.DrawTile();
                    tile.OffScreen();
                    tile.Move(-backgroundSpeed);
                }


                ice.IceDraw(backgroundSpeed, colIceBlue);
                //if (mousePosition.X > 100)
                //{ // this will draw a brush on the mouse
                Draw.FillColor = colBrown;
                Draw.Ellipse(mousePosition, mouseBrush);

                    // this will check if the brush is touching the debris
                    
                //}
                foreach (Debris deb in obstacles)
                { // thank you forEach!!!
                    if (deb.DebrisCollide(mousePosition, mouseBrush.X/3)) // will need to be opomised later
                    {
                        deb.MouseCollide(mousePosition.Y);
                    }
                    if (deb.DebrisCollide(ice.position, ice.size))
                    {
                        Console.WriteLine("damage");
                        ice.size -= (float)0.005 * deb.avgSize;
                        deb.DebrisDelete();
                        
                    }
                    deb.DebrisMove(backgroundSpeed);
                }

                backgroundSpeed = ice.iceSlow(backgroundSpeed);



                if (ice.size < 2 || backgroundSpeed > -1)
                {
                    Console.WriteLine("You Lose!!");
                }

                // background floor to be done

            }

        }
    }
}
